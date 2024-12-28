// $Id: PipeCsvCS.cs 10786 2008-09-18 17:19:51Z juan $
// Simple Pipe API program
// Copyright 2008 Port25 Solutions, Inc
// All rights reserved.
//
// This example uses the open source Lumenworks CSV library, available from
// http://www.codeproject.com/cs/database/CsvReader.asp
//
// Compile with
// csc /optimize+ /warnaserror+ warn:4 /checked+ /unsafe- /t:exe /r PipeCsvCS.cs


using System;
using System.IO;
using LumenWorks.Framework.IO.Csv;


public class Pipe {
    // This example writes all records for the given virtual MTA to a file
    // and ignores all other records.
    public static void Main(String[] args) {
        // Get the desired virtual MTA.
        String vmta;
        if (args.Length != 1) {
            Console.WriteLine("Arguments: virtual MTA");
            return;
        }
        vmta = args[0];
        
        // Open the file for this VMTA.
        StreamWriter sw = new StreamWriter("vmta-" + vmta + ".csv");
    
        // Create and configure the CSV reader.
        bool hasHeaders = true;
        char delimiter = ',';
        char quote = '"';
        char escape = '"';
        char comment = '\0'; // comment not supported
        bool trimSpaces = false;
        
        CsvReader csv = new CsvReader(Console.In, hasHeaders,
                                      delimiter, quote, escape, comment, 
                                      trimSpaces);
        csv.SupportsMultiline = true;

        // Main loop keeps reading accounting data from the console
        // until PowerMTA stops writing (i.e., shuts down).
        while (csv.ReadNextRecord()) {
            // Here do whatever you want to do with the data; this example
            // program just filters on VMTA and ignores everything that
            // is not sent from a given VMTA.
            if (vmta == csv["vmta"]) {
                int i;
                for (i = 0; i < csv.FieldCount; i++) {
                    sw.Write(csv[i]);
                    if (i < csv.FieldCount - 1) {
                        sw.Write(",");
                    }
                }
                sw.WriteLine();
            }
        }
        sw.Close();
    }
}
