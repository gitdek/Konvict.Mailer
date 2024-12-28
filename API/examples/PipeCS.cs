// $Id: PipeCS.cs 10786 2008-09-18 17:19:51Z juan $
// Simple pipe program for the .NET API
// Copyright 2008 Port25 Solutions, Inc
// All rights reserved.

// Compile with
// csc /optimize+ /warnaserror+ warn:4 /checked+ /unsafe- /t:exe PipeCS.cs


using System;


public class Benchmark {
    public static void Main(String[] args) {
        int records = 0;
        // Read from the console until PowerMTA stops writing (i.e., 
        // PowerMTA shuts down.
        String line;
        while ((line = Console.ReadLine()) != null) {
            // Now work on the accounting record we just got.
            // This example just writes it to the Console.
            Console.WriteLine(line);
            records++;
        }
        Console.WriteLine("Saw " + records + " records.");
    }
}
