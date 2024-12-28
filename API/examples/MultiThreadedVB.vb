' $Id: MultiThreadedVB.vb 10786 2008-09-18 17:19:51Z juan $
' Example program showing how to user PowerMTA's .NET API in a 
' multi-threaded way for submitting messages.
' Copyright 2007, Port25 Solutions, Inc.
' All rights reserved.

Option Explicit On
Option Strict On

Imports System
Imports System.Collections
Imports System.Threading
Imports System.IO
Imports Microsoft.VisualBasic.ControlChars
Imports port25.pmta.api.submitter


Module MultiThreadedVB
    Sub Main()
        Dim coord As ThreadCoordinator = _
            New ThreadCoordinator("trip-reminder@example.port25.com")
        ' Add servers here, either by IP address or host name.
        ' Provide the port number where PowerMTA is listening.
        ' By default, this is 25.
        coord.AddServer(New Mailhost("127.0.0.1", 25))
        coord.AddServer(New Mailhost("localhost", 25))
        ' Use this instead to connect with user name and password,
        ' according to your PowerMTA configuration:
        ' coord.AddServer(New Mailhost("localhost", 25, "username", "password"))
        coord.ConnectionsPerServer = 3
        coord.ThreadsPerConnection = 5
        coord.RecipientProvider = New RecipientProvider()
        coord.RunThreads
   End Sub


   Class Mailhost
        Private _host As String 
        Private _port As Integer
        Private _username As String
        Private _password As String
        

        Public Sub New(host As String, port As Integer) 
            Me.New(host, port, Nothing, Nothing)
        End Sub


        Public Sub New(host As String, port As Integer, _
                    username As String, password As String)
            _host = host
            _port = port
            _username = username
            _password = password
        End Sub


        Public ReadOnly Property Host() As String
            Get
                Return _host
            End Get
        End Property


        Public ReadOnly Property Port() As Integer
            Get 
                Return _port
            End Get
        End Property


        Public ReadOnly Property Username() As String
            Get
                Return _username
            End Get
        End Property


        Public ReadOnly Property Password() As String
            Get
                Return _password
            End Get
        End Property
    End Class


    ' This is a sample recipient provider.
    ' In real life, such a class would read the recipients from your data base.
    Class RecipientProvider
        Private _left As Integer


        Public Sub New()
            _left = 100 ' how many dummy emails to generate
        End Sub


        Public Function GetNext() As Recipient
            SyncLock GetType(RecipientProvider)
                If _left > 0 Then
                    _left = _left - 1
                    ' Creates a fake recipient for demonstration purposes.
                    ' Change to use your email address so that you receive the
                    ' example messages.
                    ' Defines mail merge fields for the recipient to personalize
                    ' the message.
                    Dim r As Recipient = New Recipient("joe.doe@example.port25.com")
                    r("firstname") = "Joe"
                    r("lastname") = "Doe"
                    r("airportShort") = "BWI"
                    r("airportLong") = "Baltimore/Washington International Airport"
                    r("travelDate") = "next Tuesday"
                    r("travelTime") = "10:00 am"
                    r("*parts") = "1"
                    Return r
                End If
                Return Nothing
            End SyncLock
        End Function
    End Class


    Class ThreadCoordinator
        Private _servers As ArrayList
        Private _connectionsPerServer As Integer
        Private _threadsPerConnection As Integer
        Private _mailfrom As String
        Private _threads As ArrayList
        Private _connections As ArrayList
        Private _recipientProvider As RecipientProvider 


        Public Sub New(mailfrom As String)
            _mailfrom = mailfrom
            _servers = New ArrayList()
            _threads = New ArrayList()
            _connections = New ArrayList()
            _threadsPerConnection = 1
            _connectionsPerServer = 1
        End Sub


        Public Sub AddServer(host As Mailhost)
            _servers.Add(host)
        End Sub


        Public Property ConnectionsPerServer() As Integer
            Get 
                Return _connectionsPerServer
            End Get
            Set
                If value < 1 Then
                    Throw New ArgumentOutOfRangeException( _
                        "Must have at least one connection per server")
                End If
                _connectionsPerServer = value
            End Set
        End Property


        Public Property ThreadsPerConnection As Integer
            Get
                Return _threadsPerConnection
            End Get
            Set 
                If value < 1 Then
                    Throw New ArgumentOutOfRangeException( _
                        "Must have at least one thread per connection")
                End If
                _threadsPerConnection = value
            End Set
        End Property


        Public Property RecipientProvider As RecipientProvider
            Set
                _recipientProvider = value
            End Set
            Get
                Return _recipientProvider
            End Get
        End Property


        Public Sub RunThreads()
            Try
                StartThreads
            Finally 
                WaitForAllThreads
                CloseAllConnections
            End Try
        End Sub


        Private Sub StartThreads
            Dim m As MailHost
            For Each m In _servers
                Dim c As Integer
                For c = 1 To _connectionsPerServer
                    Dim con As Connection
                    con = New Connection(m.Host, m.Port, m.Username, m.Password)
                    _connections.Add(con)
                    Dim t As Integer
                    For t = 1 To _threadsPerConnection
                        Dim st As SendThread
                        st = New SendThread(_mailfrom, con, "vmta" & t, _
                            _recipientProvider)
                        Dim thread As Thread = New Thread(New ThreadStart(AddressOf st.Run))
                        _threads.Add(thread)
                        thread.Start
                    Next t
                Next c
            Next m
        End Sub


        Private Sub WaitForAllThreads
            Dim t As Thread
            For Each t In _threads
                t.Join
            Next
        End Sub


        Private Sub CloseAllConnections
            Dim c As Connection
            For Each c In _connections
                c.Close
            Next
        End Sub
    End Class


    Class SendThread
        Private _mailfrom As String
        Private _con As Connection
        Private _recipientProvider As RecipientProvider 
        Private _vmta As String

        
        Public Sub New(mailfrom As String, con As Connection, _
                vmta As String, provider As RecipientProvider)
            _mailfrom = mailfrom
            _con = con
            _recipientProvider = provider
            _vmta = vmta
        End Sub


        Public Sub Run()
            Dim r As Recipient = _recipientProvider.GetNext()
            While Not r Is Nothing
                Dim msg As Message = MakeMessage(r)
                _con.Submit(msg)
                r = _recipientProvider.GetNext()
            End While
        End Sub


        ' Composes a multi-part message with a plain text and an HTML part.
        ' The HTML part comes with an attached picture.
        ' Uses PowerMTA's mail merge feature to personalize the message.
        Private Function MakeMessage(r As Recipient) As Message
            Dim msg As Message = New Message(_mailfrom)
            msg.VirtualMTA = _vmta
            msg.AddRecipient(r)
            
            Dim outerBoundary As String = "outerboundary"
            Dim innerBoundary As String = "innerboundary"
            
            Dim headers As String = _
                "To: ""[firstname] [lastname]"" <[*to]>" & NewLine & _
                "From: ""Trip Reminder Service"" <" & _mailfrom & ">" & NewLine & _
                "Subject: Trip reminder" & NewLine & _
                "MIME-Version: 1.0" & NewLine & _
                "Content-Type: multipart/related; boundary=""" &  _
                outerBoundary & _
                """" & NewLine
            msg.AddMergeData(headers)
            msg.AddDateHeader
            
            ' Optionally add a preamble for old mail clients that don't know
            ' MIME:
            Dim preamble As String = _
                NewLine & _
                "This is a multi-part message in MIME format." & NewLine & _
                NewLine
            AddData(msg, preamble)

            Dim innerPart As String = _
                "--" & outerBoundary & NewLine & _
                "Content-Type: multipart/alternative; boundary=""" & _
                innerBoundary & """" & NewLine & _
                NewLine
            AddData(msg, innerPart)

            Dim plainTextBody As String = _
                "--" & innerBoundary & NewLine & _
                "Content-Type: text/plain; charset=utf-8" & NewLine & _
                NewLine & _
                "Dear [firstname]," & NewLine & NewLine & _
                "here is your trip reminder." & NewLine & _
                "You are leaving [airportLong] ([airportShort]) on " & _
                "[travelDate] at [travelTime]." & NewLine & _
                "Please make sure you arrive an hour early." & NewLine & _
                NewLine & _
                "Thanks for your business and have a good trip!" & NewLine
            msg.AddMergeData(plainTextBody)

            Dim logoId As String = "logo"
            Dim htmlBody As String = _
                "--" & innerBoundary & NewLine & _
                "Content-Type: text/html; charset=utf-8" & NewLine & _
                NewLine & _
                "<html>" & NewLine & _
                "<body>" & NewLine
                ' include an image on the web like this:
                ' "<img src='http:'www.port25.com/images/port25_LOGO.gif'" & _
                ' "   alt='logo' />"
                ' refer to an image sent with the email like this:
            htmlBody = htmlBody & _
                "<img src='cid:" & logoId & "' alt='logo' />" & NewLine & _
                "<p>Dear <b>[firstname]</b>," & NewLine & NewLine & _
                "<p>here is your trip reminder.</p>" & NewLine & _
                "<p>You are leaving [airportLong] ([airportShort]) on " & _
                "[travelDate] at [travelTime].</p>" & NewLine & _
                "<p>Please make sure you arrive an hour early.</p>" & NewLine & _
                NewLine & _
                "<p>Thanks for your business and have a good trip!</p>" & NewLine & _
                "</body>" & NewLine & _
                "</html>" & NewLine
            msg.AddMergeData(htmlBody)
            AddData(msg, NewLine & "--" & innerBoundary & "--" & NewLine)

            InlineImage(msg, logoId, "examples\Port25_LOGO.gif", outerBoundary)
            Return msg
        End Function


        Private sub AddData(msg as Message, data as String)
            ' PowerMTA encodes strings in UTF-8.  If you need a different encoding,
            ' use something like this:
            '
            '       msg.AddData(System.Text.Encoding.X.GetBytes(headers));
            '
            '
            ' where X is the desired encoding.  You must also set the body part's
            ' character set accordingly.
            msg.AddData(data)
        End Sub


        Private Sub InlineImage(msg As Message, id As String,  _
                path As String, boundary As String)
            Dim imageHeader As String = _
                NewLine & "--" & boundary & NewLine & _
                "Content-ID: <" & id & ">" & NewLine & _
                "Content-Type: image/gif" & NewLine & _
                "Content-Transfer-Encoding: base64" & NewLine & _
                "Content-Disposition: inline filename=" & id & NewLine & _
                NewLine
            AddData(msg, imageHeader)

            msg.Encoding = port25.pmta.api.submitter.Encoding.Base64
            Dim fs As FileStream = Nothing
            Try
                fs = New FileStream(path, FileMode.Open, FileAccess.Read, _
                                                         FileShare.Read)
                Dim imageData(CInt(fs.Length)) As byte
                fs.Read(imageData, 0, imageData.Length)
                msg.AddData(imageData)
            Finally 
                If Not fs Is Nothing Then
                    fs.Close
                End If
            End Try

            msg.Encoding = port25.pmta.api.submitter.Encoding.SevenBit
            AddData(msg, NewLine & "--" + boundary + "--" & NewLine)
        End Sub
    End Class
End Module
