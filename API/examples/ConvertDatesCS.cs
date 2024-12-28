// $Id: ConvertDatesCS.cs 10786 2008-09-18 17:19:51Z juan $
// Example for how to convert PowerMTA's timestamps to human-readable fields.
// Some spreadsheet programs or databases may also be more likely to import
// such dates.
//
// This example uses the open source Lumenworks CSV library, available from
// http://www.codeproject.com/cs/database/CsvReader.asp
//
// Copyright 2007, Port25 Solutions, Inc.
// All rights reserved.

using System;
using System.IO;
using LumenWorks.Framework.IO.Csv;


public class ConvertDates {
    public static void Main(String[] argv) {
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
        writeHeader(csv);
        while (csv.ReadNextRecord()) {
            for (int i = 0; i < csv.FieldCount; i++) {
                // Time fields may be emtpty, e.g., if no gm-imprinting is
                // used, the timeImprinted fields is empty.
                if (csv.GetFieldHeaders()[i].StartsWith("time") && 
                    csv[i] != "") {
                    // PowerMTA uses Unix time stamps (seconds since midnight
                    // on Jan 1st 1970)
                    DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0);
                    // A tick is 100 nano seconds; tick 0 was 12:00 AM on
                    // Jan 1st in the year 1.
                    dt = dt.AddSeconds(System.Convert.ToInt64(csv[i]));
                    dt = dt.Add(TimeZone.CurrentTimeZone.GetUtcOffset(dt));
                    Console.Write(dt.ToString());
                }
                else {
                    Console.Write(csv[i]);
                }
                writeComma(csv, i);
            }
            Console.WriteLine();
        }
    }
    
    
    private static void writeHeader(CsvReader csv) {
        String[] headers = csv.GetFieldHeaders();
        for (int i = 0; i < csv.FieldCount; i++) {
            Console.Write(headers[i]);
            writeComma(csv, i);
        }
        Console.WriteLine();
    }
    
    
    private static void writeComma(CsvReader csv, int i) {
        if (i < csv.FieldCount - 1) {
            Console.Write(',');
        }
    }
}
