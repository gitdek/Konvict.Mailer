using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Threading;
using port25.pmta.api.submitter;

namespace TestInject
{
    class Program
    {

        static void Main(string[] args)
        {
            string host = "174.137.191.4";
            string user = "dek";
            string passwd = "05MtjRRB5phxmmLsi8mI";
            int port = 2525;
            
            Console.WriteLine("\n|-------------------------------------|");
            Console.WriteLine("| iamevil[@]gmail[dot]com             |");
            Console.WriteLine("| Injector Console v0.1 alpha         |");
            Console.WriteLine("|-------------------------------------|\n");


            ThreadCoordinator coord = new ThreadCoordinator("jector@cchls.net");

            coord.AddServer(new Mailhost(host, port, user, passwd));
            coord.ConnectionsPerServer = 1;
            coord.ThreadsPerConnection = 1;
            coord.RecipientProvider = new RecipientProvider();

            Console.WriteLine("Running. Threads: {0}\nConnections: {1}\n",coord.ThreadsPerConnection,coord.ConnectionsPerServer);
            coord.RunThreads();

            Console.ReadKey(true);
        }
    }


    internal class Mailhost
    {
        private String _host;
        private int _port;
        private String _username;
        private String _password;


        public Mailhost(String host, int port) : this(host, port, null, null) { }


        public Mailhost(String host, int port, String username, String password)
        {
            _host = host;
            _port = port;
            _username = username;
            _password = password;
        }


        public String Host
        {
            get
            {
                return _host;
            }
        }


        public int Port
        {
            get
            {
                return _port;
            }
        }


        public String Username
        {
            get
            {
                return _username;
            }
        }


        public String Password
        {
            get
            {
                return _password;
            }
        }
    }


    // This is a sample recipient provider.
    // In real life, such a class would read the recipients from your data base.
    internal class RecipientProvider
    {
        private int _left;


        public RecipientProvider()
        {
            _left = 10; // how many dummy emails to generate
        }


        public Recipient GetNext()
        {
            lock (this)
            {
                if (_left-- > 0)
                {
                    // Creates a fake recipient for demonstration purposes.
                    // Change to use your email address, so that you receive the
                    // example messages.
                    // Defines mail merge fields for the recipient to personalize
                    // the message.
                    Recipient r = new Recipient("justadropbox@aol.com");
                    r["firstname"] = "Jacob";
                    r["lastname"] = "Doe";
                    r["airportShort"] = "BWI";
                    r["airportLong"] = "Baltimore/Washington International Airport";
                    r["travelDate"] = "next Tuesday";
                    r["travelTime"] = "10:00 am";
                    r["*parts"] = "1,2";
                    
                    return r;
                }
                return null;
            }
        }
    }


    internal class ThreadCoordinator
    {

        private ArrayList _servers;
        private int _connectionsPerServer;
        private int _threadsPerConnection;
        private String _mailfrom;
        private ArrayList _threads;
        private ArrayList _connections;
        private RecipientProvider _recipientProvider;


        public ThreadCoordinator(String mailfrom)
        {
            _mailfrom = mailfrom;
            _servers = new ArrayList();
            _threads = new ArrayList();
            _connections = new ArrayList();
            _threadsPerConnection = 1;
            _connectionsPerServer = 1;
            
        }


        public void AddServer(Mailhost host)
        {
            _servers.Add(host);
        }


        public int ConnectionsPerServer
        {
            get
            {
                return _connectionsPerServer;
            }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException(
                        "Must have at least one connection per server");
                }
                _connectionsPerServer = value;
            }
        }


        public int ThreadsPerConnection
        {
            get
            {
                return _threadsPerConnection;
            }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException(
                        "Must have at least one thread per connection");
                }
                _threadsPerConnection = value;
            }
        }


        public RecipientProvider RecipientProvider
        {
            set
            {
                _recipientProvider = value;
            }
            get
            {
                return _recipientProvider;
            }
        }


        public void RunThreads()
        {
            try
            {
                StartThreads();
            }
            finally
            {
                WaitForAllThreads();
                CloseAllConnections();
                Console.WriteLine("Finished.");
            }
        }


        private void StartThreads()
        {
            foreach (Mailhost m in _servers)
            {
                for (int c = 0; c < _connectionsPerServer; c++)
                {
                    Connection con = new Connection(m.Host, m.Port, m.Username, m.Password);
                    _connections.Add(con);
                    Console.WriteLine("Connected!");
                    
                    for (int t = 0; t < _threadsPerConnection; t++)
                    {
                        SendThread st = new SendThread(_mailfrom, con, "v" + t,
                            _recipientProvider);
                        Thread thread = new Thread(new ThreadStart(st.Run));
                        _threads.Add(thread);
                        thread.Start();
                    }
                }
            }
        }


        private void WaitForAllThreads()
        {
            foreach (Thread t in _threads)
            {
                t.Join();
            }
        }


        private void CloseAllConnections()
        {
            foreach (Connection c in _connections)
            {
                c.Close();
            }
        }
    }



    internal class SendThread
    {
        private String _mailfrom;
        private Connection _con;
        private RecipientProvider _recipientProvider;
        private String _vmta;


        public SendThread(String mailfrom, Connection con, String vmta,
                RecipientProvider provider)
        {
            _mailfrom = mailfrom;
            _con = con;
            _recipientProvider = provider;
            _vmta = vmta;
        }


        public void Run()
        {
            Recipient r;
            while ((r = _recipientProvider.GetNext()) != null)
            {
                Message msg = MakeMessage(r);
                _con.Submit(msg);
                Console.WriteLine("Message sent.");
            }
        }


        // Composes a multi-part message with a plain text and an HTML part.
        // The HTML part comes with an attached picture.
        // Uses PowerMTA's mail merge feature to personalize the message.
        private Message MakeMessage(Recipient r)
        {
            Message msg = new Message(_mailfrom);
            msg.VirtualMTA = _vmta;
            msg.AddRecipient(r);

            String outerBoundary = "outerboundary";
            String innerBoundary = "innerboundary";

            String headers =
                "To: \"[firstname] [lastname]\" <[*to]>\n" +
                "From: \"Trip Reminder Service\" <" + _mailfrom + ">\n" +
                "Subject: Trip reminder\n" +
                "MIME-Version: 1.0\n" +
                "Content-Type: multipart/related; boundary=\"" +
                outerBoundary +
                "\"\n";

            msg.AddMergeData(headers);
            // Optionally add a preamble for old mail clients that don't know
            // MIME:
            String preamble =
                "\n" +
                "This is a multi-part message in MIME format.\n" +
                "\n";
            AddData(msg, preamble);

            String innerPart =
                "--" + outerBoundary + "\n" +
                "Content-Type: multipart/alternative; boundary=\"" +
                innerBoundary + "\"\n" +
                "\n";
            AddData(msg, innerPart);

            String plainTextBody =
                "--" + innerBoundary + "\n" +
                "Content-Type: text/plain; charset=utf-8\n" +
                "\n" +
                "Dear [firstname],\n\n" +
                "here is your trip reminder.\n" +
                "You are leaving [airportLong] ([airportShort]) on " +
                "[travelDate] at [travelTime].\n" +
                "Please make sure you arrive an hour early.\n" +
                "\n" +
                "Thanks for your business and have a good trip!\n";


            msg.AddMergeData(plainTextBody);

            String logoId = "logo";
            String htmlBody =
                "--" + innerBoundary + "\n" +
                "Content-Type: text/html; charset=utf-8\n" +
                "\n" +
                "<html>\n" +
                "<body>\n" +
                // include an image on the web like this:
                // "<img src='http://www.port25.com/images/port25_LOGO.gif'" +
                // "   alt='logo' />" +
                // refer to an image sent with the email like this:
                "<img src='cid:" + logoId + "' alt='logo' />\n" +
                "<p>Dear <b>[firstname]</b>,\n\n" +
                "<p>here is your trip reminder.</p>\n" +
                "<p>You are leaving [airportLong] ([airportShort]) on " +
                "[travelDate] at [travelTime].</p>\n" +
                "<p>Please make sure you arrive an hour early.</p>\n" +
                "\n" +
                "<p>Thanks for your business and have a good trip!</p>\n";

            msg.AddMergeData(htmlBody);

            msg.BeginPart(2);
            string testMerge =
                "<center><b>hi TESTING PART 2!!!</b></center>\n" +
                "#1!!!!</font>\n" +
                "</body>\n" +
                "</html>\n\n";

            msg.AddMergeData(testMerge);
            AddData(msg, "\n--" + innerBoundary + "--\n");

            
            //msg.BeginPart(3);
            //string testMerge3 = "#3!!!!\n";
            //msg.AddMergeData(testMerge3);

            //InlineImage(msg, logoId, "examples\\Port25_LOGO.gif", outerBoundary);
            return msg;
        }


        private void AddData(Message msg, String data)
        {
            // PowerMTA encodes strings in UTF-8.  If you need a different encoding,
            // use something like this:
            //
            //      msg.AddData(System.Text.Encoding.X.GetBytes(headers));
            //
            // where X is the desired encoding.  You must also set the body part's
            // character set accordingly. 
            msg.AddData(data);
        }


        private void InlineImage(Message msg, String id, String path, String boundary)
        {
            String imageHeader =
                "\n--" + boundary + "\n" +
                "Content-ID: <" + id + ">\n" +
                "Content-Type: image/gif\n" +
                "Content-Transfer-Encoding: base64\n" +
                "Content-Disposition: inline; filename=" + id + "\n" +
                "\n";

            AddData(msg, imageHeader);

            msg.Encoding = port25.pmta.api.submitter.Encoding.Base64;
            FileStream fs = null;
            try
            {
                fs = new FileStream(path, FileMode.Open, FileAccess.Read,
                                                         FileShare.Read);
                byte[] imageData = new byte[fs.Length];
                fs.Read(imageData, 0, imageData.Length);
                msg.AddData(imageData);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }

            msg.Encoding = port25.pmta.api.submitter.Encoding.SevenBit;
            AddData(msg, "\n--" + boundary + "--\n");
        }
    }

}
