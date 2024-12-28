' $Id: ConvertDatesVB.vb 10786 2008-09-18 17:19:51Z juan $
' Example for how to convert PowerMTA's timestamps to human-readable fields.
' Some spreadsheet programs or databases may also be more likely to import
' such dates.
'
' This example uses the open source Lumenworks CSV library, available from
' http://www.codeproject.com/cs/database/CsvReader.asp
'
' Copyright 2007, Port25 Solutions, Inc.
' All rights reserved.

Option Explicit On
Option Strict On

Imports System
Imports System.IO
Imports LumenWorks.Framework.IO.Csv


Module ConvertDates
    Sub Main
        Dim hasHeaders As Boolean = true
        Dim delimiter As Char = ","C
        Dim quote As Char = """"C
        Dim escape As Char = """"C
        Dim comment As Char = Nothing
        Dim trimSpaces As Boolean = false
        
        Dim csv As CsvReader = new CsvReader(Console.In, _
                                             hasHeaders, delimiter, quote, _
                                             escape, comment, trimSpaces)
        csv.SupportsMultiline = true
        writeHeader(csv)
        While (csv.ReadNextRecord()) 
            Dim i As Integer
            For i = 0 To csv.FieldCount - 1
                ' Time fields may be emtpty, e.g., if no gm-imprinting is
                ' used, the timeImprinted fields is empty.
                If (csv.GetFieldHeaders()(i).StartsWith("time") AND _
                    csv(i) <> "") Then
                    ' PowerMTA uses Unix time stamps (seconds since midnight
                    ' on Jan 1st 1970).
                    Dim dt As DateTime = new DateTime(1970, 1, 1, 0, 0, 0)
                    dt = dt.AddSeconds(System.Convert.ToInt64(csv(i)))
                    dt = dt.Add(TimeZone.CurrentTimeZone.GetUtcOffset(dt))
                    Console.Write(dt.ToString())
                Else 
                    Console.Write(csv(i))
                End If
                writeComma(csv, i)
            Next
            Console.WriteLine()
        End While
    End Sub
    
    
    Private Sub writeHeader(csv As CsvReader) 
        Dim headers As String() = csv.GetFieldHeaders()
        Dim i As Integer
        For i = 0 To csv.FieldCount - 1
            Console.Write(headers(i))
            writeComma(csv, i)
        Next
        Console.WriteLine()
    End Sub
    
    
    Private Sub writeComma(csv As CsvReader, i As Integer) 
        If (i < csv.FieldCount - 1) Then
            Console.Write(",")
        End If
    End Sub
End Module