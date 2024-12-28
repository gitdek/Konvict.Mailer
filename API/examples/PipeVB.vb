' $Id: PipeVB.vb 10786 2008-09-18 17:19:51Z juan $
' Simple pipe program for the .NET API
' Copyright 2008 Port25 Solutions, Inc
' All rights reserved.

' Compile with
' vbc /optimize+ /warnaserror+ warn:4 /checked+ /unsafe- /t:exe PipeVB.vb

Option Explicit On
Option Strict On

Imports System


Class Pipe
    Public Shared Sub Main(args As String()) 
        Dim records As Integer = 0
        ' Read from the console until PowerMTA stops writing (i.e., 
        ' PowerMTA shuts down.
        Dim line As String
        line = Console.ReadLine()
        While line <> Nothing
            ' Now work on the accounting record we just got.
            ' This example just writes it to the Console.
            Console.WriteLine(line)
            records = records + 1
            line = Console.ReadLine()
        End While
        Console.WriteLine("Saw " & records & " records.")
    End Sub
End Class
