// $Id: MessageCS.cs 10786 2008-09-18 17:19:51Z juan $
// Example for sending a simple message via PowerMTA's .NET API.
// Copyright 2007-2008, Port25 Solutions, Inc.
// All rights reserved.

using System;
using port25.pmta.api.submitter;

public class ExampleMessage {
    public static void Main() {
        // mailfrom is used in the SMTP mail-from
        String mailfrom = "me.here@some.example-domain.com";
        // sender appears in the email; can be the same as mailfrom
        String sender = "sender@some.example-domain.com";
        String recipient = mailfrom;

        Message msg = new Message(mailfrom);
        msg.AddDateHeader();
        String headers = 
            "From: Some Sender <" + sender + ">\n" +
            "To: Me Here <" + recipient + ">\n" +
            "Subject: C# test email\n" +
            "Content-Type: text/plain; charset=\"utf-8\"\n" +
            "\n"; // separate headers from body by an empty line
        msg.AddData(headers);

        // adjust as needed (or remove) these:
        msg.VirtualMTA = "vmta1";
        msg.JobID = "my-job";

        msg.AddData("This was sent from C# with PowerMTA's .NET API\n");

        Recipient rcpt = new Recipient(recipient);
        msg.AddRecipient(rcpt);
        
        Connection con = new Connection("127.0.0.1", 25);
        con.Submit(msg);
        con.Close();
    }
}
