' $Id: MessageVB.vb 10786 2008-09-18 17:19:51Z juan $
' Example for sending a simple message via PowerMTA's .NET API.
' Copyright 2007, Port25 Solutions, Inc.
' All rights reserved.

Option Explicit On
Option Strict On

Imports System
Imports port25.pmta.api.submitter

Module MessageVB
    ' mailfrom is used in the SMTP mail-from
    Const mailfrom As String = "me.here@some-example-domain.com"
    ' sender appears in the email; can be the same as mailfrom
    Const sender As String = "some.sender@some-example-domain.com"
    Const recipient As String = mailfrom

    Sub Main()
        Dim msg As New Message(mailfrom)
        msg.AddDateHeader()
        Dim headers As String = _
            "From: Some Sender <" & sender & ">" & Environment.Newline & _
            "To: Me Here <" & recipient & ">" & Environment.Newline & _
            "Subject: VB test email" & Environment.Newline & _
            "Content-Type: text/plain; charset=""utf-8""" & Environment.Newline & _
            Environment.Newline 'separate headers and body by an emty line
        msg.AddData(headers)
        msg.AddData("This was sent from Visual Basic with PowerMTA's " _
            & ".NET API" & Environment.Newline)
        
        ' set as needed (or remove) these:
        msg.VirtualMTA = "vmta1"
        msg.JobID = "my-job"
        
        Dim rcpt As New Recipient(recipient)
        msg.AddRecipient(rcpt)
        
        Dim con As new Connection("127.0.0.1", 25)
        con.Submit(msg)
        con.Close()
    End Sub
End Module
