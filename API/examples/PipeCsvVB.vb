' $Id: PipeCsvVB.vb 10786 2008-09-18 17:19:51Z juan $
' Simple Pipe API program
' Copyright 2008 Port25 Solutions, Inc
' All rights reserved.
'
' This example uses the open source Lumenworks CSV library, available from
' http://www.codeproject.com/cs/database/CsvReader.asp
'
' Compile with
' vbc /optimize+ /warnaserror+ warn:4 /checked+ /unsafe- /t:exe /r PipeCsvCS.cs

Option Explicit On
Option Strict On

Imports System
Imports System.IO
Imports LumenWorks.Framework.IO.Csv


Class Pipe
    ' This example writes all records for the given virtual MTA to a file
    ' and ignores all other records.
    Public Shared Sub Main(args As String())
        ' Get the desired virtual MTA.
        Dim vmta As String
        If args.Length <> 1 Then
            Console.WriteLine("Arguments: virtual MTA")
            return
        End If
        vmta = args(0)
        
        ' Open the file for this VMTA.
        Dim sw As StreamWriter = new StreamWriter("vmta-" & vmta & ".csv")
    
        ' Create and configure the CSV reader.
        Dim hasHeaders As Boolean = true
        Dim delimiter As Char = ","C
        Dim quote As Char = """"C
        Dim escape As Char = """"C
        Dim comment As Char = Nothing   ' comment not supported
        Dim trimSpaces As Boolean = false
        
        Dim csv As CsvReader = new CsvReader(Console.In, hasHeaders, _
                                      delimiter, quote, escape, comment, _
                                      trimSpaces)
        csv.SupportsMultiline = true

        ' Main loop keeps reading accounting data from the console
        ' until PowerMTA stops writing (i.e., shuts down).
        While (csv.ReadNextRecord())
            ' Here do whatever you want to do with the data; this example
            ' program just filters on VMTA and ignores everything that
            ' is not sent from a given VMTA.
            If vmta = csv("vmta") Then
                Dim i As Integer
                For i = 0 To csv.FieldCount
                    sw.Write(csv(i))
                    If i < csv.FieldCount - 1 Then
                        sw.Write(",")
                    End If
                Next i
                sw.WriteLine()
            End If
        End While
        sw.Close()
    End Sub
End Class
