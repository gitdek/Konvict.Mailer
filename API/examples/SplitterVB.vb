' $Id: SplitterVB.vb 10786 2008-09-18 17:19:51Z juan $
' Example for how to split PowerMTA's accounting files by a certain field,
' e.g., by job ID or envelope id.
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
Imports System.Collections
Imports LumenWorks.Framework.IO.Csv


Module Splitter
    Sub Main(argv As String()) 
        If argv.Length < 2 Then
            Console.WriteLine("Args: field file [file...]")
            Console.WriteLine("Where 'field' can be jobId, envId, vmta, etc.")
            Console.WriteLine("Check the PowerMTA User's Guide for field names.")
            Console.WriteLine("'field' is case-sensitive!")
            return
        End If
        Dim splitter As New Splitter(argv(0))
        Dim i As Integer
        For i = 1 To argv.Length - 1
            splitter.readFile(argv(i))
        Next
        splitter.closeFiles
    End Sub
    
    
    Class Splitter
        Private want As String
        Private openFiles As HashTable
    
    
        Public Sub New(wanted As String) 
            want = wanted
            openFiles = new Hashtable()
        End Sub
    
    
        Public Sub readFile(inFile As String) 
            Dim hasHeaders As Boolean = true
            Dim delimiter As Char = ","C
            Dim quote As Char = """"C
            Dim escape As Char = """"C
            Dim comment As Char = Nothing
            Dim trimSpaces As Boolean = false
            
            Dim csv As CsvReader = new CsvReader(new StreamReader(inFile), _
                                                 hasHeaders, delimiter, quote, _
                                                 escape, comment, trimSpaces)
            csv.SupportsMultiline = true
            while (csv.ReadNextRecord()) 
                Dim id As String = csv(want)
                ' CSV library throws on unknown fields
                Dim isNewFile As Boolean = Not openFiles.Contains(id)
                If isNewFile Then
                    Dim filename As String = "acct-" & want & "-" & id & ".csv"
                    openFiles(id) = new StreamWriter(filename)
                End If
                Dim sw As StreamWriter = DirectCast(openFiles(id), StreamWriter)
                If isNewFile Then
                    writeHeader(sw, csv)
                End If
                
                Dim i As Integer
                For i = 0 To csv.FieldCount - 1
                    sw.Write(csv(i))
                    writeComma(sw, csv, i)
                Next
                sw.WriteLine
            End While
        End Sub    
        
        
        Private Sub writeHeader(sw As StreamWriter, csv As CsvReader) 
            Dim headers As String() = csv.GetFieldHeaders()
            Dim i As Integer
            For i = 0 To csv.FieldCount - 1
                sw.Write(headers(i))
                writeComma(sw, csv, i)
            Next
            sw.WriteLine
        End Sub
        
        
        Private Sub writeComma(sw As StreamWriter, csv As CsvReader, i as Integer) 
            If i < csv.FieldCount - 1 Then
                sw.Write(",")
            End If
        End Sub
        
        
        Public Sub closeFiles
            Dim sw As StreamWriter
            For Each sw In openFiles.Values
                sw.Close
            Next
        End Sub    
    End Class
End Module
