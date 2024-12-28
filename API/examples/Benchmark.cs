// $Id: Benchmark.cs 10786 2008-09-18 17:19:51Z juan $
// Simple benchmarking program for the .NET API
// Copyright 2007-2008 Port25 Solutions, Inc
// All rights reserved.

// Adjust all spots marked "TODO"!

// Compile with
// csc /optimize+ /warnaserror+ warn:4 /checked+ /unsafe- /t:exe /reference:port25.pmta.api.submitter.dll Benchmark.cs


using System;
using port25.pmta.api.submitter;


public class Benchmark {
    public static void Main(String[] args) {
        DateTime start = DateTime.Now;
        // TODO: put in a test host on your system or configure discard
        // delivery for the destination host.

        if (args.Length < 5) {
            Console.WriteLine("Args: host, port, msgs, kbytes per message, recipients per message");
            return;
        }

        String host = args[0];
        int port = Int32.Parse(args[1]);        
        int messages = Int32.Parse(args[2]);
        int kbytes = Int32.Parse(args[3]);
        int recipients = Int32.Parse(args[4]);

        Console.WriteLine("C# Benchmark: sending " 
            + messages + " messages, " 
            + recipients + " recipients per message, " 
            + kbytes + " kbytes per message"
            + " to " +
            host + ":" + port);

        byte[] theKByte = new byte[1024];
        for (int i = 0; i < theKByte.Length; i++) {
            theKByte[i] = (byte) 'x';
        }     

        Connection con = new Connection(host, port);

        for (int m = 0; m < messages; m++) {
            // TODO: adjust recipient address
            Message msg = new Message("me.here@" + host);
            msg.AddDateHeader();
            String headers = 
                "From: Me Here <me.here@" + host + ">\n" +
                "To: You There <you.there@d" + host + ">\n" +
                "Subject: .NET benchmark email\n" +
                "\n";
            msg.AddData(System.Text.Encoding.ASCII.GetBytes(headers));
            for (int k = 0; k < kbytes; k++) {
                msg.AddData(theKByte);
            }
            for (int r = 0; r < recipients; r++) {
                Recipient rcpt = new Recipient("r"+ r + "@" + host);
                rcpt["*parts"] = "1";
                msg.AddRecipient(rcpt);
            }
            con.Submit(msg);
        }

        con.Close();

        DateTime end = DateTime.Now;
        System.TimeSpan diff = end.Subtract(start);
        Console.WriteLine("Took " + diff.ToString());
    }
}
