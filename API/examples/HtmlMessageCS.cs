// $Id: HtmlMessageCS.cs 10786 2008-09-18 17:19:51Z juan $
// Example for sending a simple message via PowerMTA's .NET API.
// Copyright 2007, Port25 Solutions, Inc.
// All rights reserved.

using System;
using port25.pmta.api.submitter;

public class ExampleHtmlMessage {
    public static void Main() {
        // See RFC 2557 for details; http://tools.ietf.org/html/rfc2557
        String mailfrom = "me.here@some.example-domain.com";
        String recipient = mailfrom;

        Message msg = new Message(mailfrom);
        msg.AddDateHeader();
        String headers = 
            "From: Me Here <" + mailfrom + ">\n" +
            "To: Joe Doe <" + recipient + ">\n" +
            "Subject: C# HTML test email\n" +
            "Content-Type: text/html; charset=\"utf-8\"\n" +
            "\n"; // separate headers from body by an empty line
        msg.AddData(headers);

        String body = 
            "<html>" +
            "<body>" +
            "<p>Dear <b>Joe</b>,</p>" +
            "<p>This is sent using <a href='http://www.port25.com'>PowerMTA</a>'s" +
            "new .NET API!</p>" +
            "</body>" +
            "</html>";
        msg.AddData(body);

        Recipient rcpt = new Recipient(recipient);
        msg.AddRecipient(rcpt);
        
        Connection con = new Connection("127.0.0.1", 25);
        con.Submit(msg);
        con.Close();
    }
}
