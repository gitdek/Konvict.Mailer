// $Id: SplitterCS.cs 10786 2008-09-18 17:19:51Z juan $
// Example for how to split PowerMTA's accounting files by a certain field,
// e.g., by job ID or envelope id.
//
// This example uses the open source Lumenworks CSV library, available from
// http://www.codeproject.com/cs/database/CsvReader.asp
//
// Copyright 2007, Port25 Solutions, Inc.
// All rights reserved.

using System;
using System.IO;
using System.Collections;
using LumenWorks.Framework.IO.Csv;


public class Splitter {
    public static void Main(String[] argv) {
        if (argv.Length < 2) {
            Console.WriteLine("Args: field file [file...]");
            Console.WriteLine("Where 'field' can be jobId, envId, vmta, etc.");
            Console.WriteLine("Check the PowerMTA User's Guide for field names.");
            Console.WriteLine("'field' is case-sensitive!");
            return;
        }
        Splitter splitter = new Splitter(argv[0]);
        for (int i = 1; i < argv.Length; i++) {
            splitter.readFile(argv[i]);
        }
        splitter.closeFiles();
    }
    
    
    private String want;
    private Hashtable openFiles;
    
    
    public Splitter(String wanted) {
        want = wanted;
        openFiles = new Hashtable();
    }
    
    
    public void readFile(String inFile) {
        bool hasHeaders = true;
        char delimiter = ',';
        char quote = '"';
        char escape = '"';
        char comment = '\0'; // comment not supported
        bool trimSpaces = false;
        
        CsvReader csv = new CsvReader(new StreamReader(inFile), hasHeaders,
                                      delimiter, quote, escape, comment, 
                                      trimSpaces);
        csv.SupportsMultiline = true;
        while (csv.ReadNextRecord()) {
            String id = csv[want]; // CSV library throws on unknown fields
            bool isNewFile = !openFiles.Contains(id);
            if (isNewFile) {
                String filename = "acct-" + want + "-" + id + ".csv";
                openFiles[id] = new StreamWriter(filename);
            }
            StreamWriter sw = (StreamWriter) openFiles[id];
            if (isNewFile) {
                writeHeader(sw, csv);
            }
            for (int i = 0; i < csv.FieldCount; i++) {
                sw.Write(csv[i]);
                writeComma(sw, csv, i);
            }
            sw.WriteLine();
        }
    }
    
    
    private void writeHeader(StreamWriter sw, CsvReader csv) {
        String[] headers = csv.GetFieldHeaders();
        for (int i = 0; i < csv.FieldCount; i++) {
            sw.Write(headers[i]);
            writeComma(sw, csv, i);
        }
        sw.WriteLine();
    }
    
    
    private void writeComma(StreamWriter sw, CsvReader csv, int i) {
        if (i < csv.FieldCount - 1) {
            sw.Write(',');
        }
    }
    
    
    public void closeFiles() {
        foreach (StreamWriter sw in openFiles.Values) {
            sw.Close();
        }
    }
}
