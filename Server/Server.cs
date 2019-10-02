using ModelLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Server
    {
        // backing fields
        private TcpListener server;
        private static List<Bog> _bogs = new List<Bog>()
        {
            new Bog("Crimes of McDonalds", "Donald Trump", 200, "123456789-ABC"),
            new Bog("How to eat food", "Bear Grylls", 300, "223456789-ABC"),
            new Bog("The art of the Meal", "Gordon Ramsey", 600, "323456789-ABC"),
            new Bog("Songs of Peter & Anders", "George R.R. Martin", 200, "423456789-ABC")
        };

        // constructor
        public Server()
        {
            // giving IP address & port
            server = new TcpListener(IPAddress.Loopback, 4646);
            // starting TcpListener
            server.Start();

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                Task.Run(() =>
                {
                    DoClient(client);
                });
            }
        }

        private void DoClient(TcpClient client)
        {
            NetworkStream ns = client.GetStream();
            using (StreamReader sr = new StreamReader(ns))
            using (StreamWriter sw = new StreamWriter(ns))
            {
                // start message
                CommandGuide(sw);

                string clientcommand = sr.ReadLine() + "";

                // deleting spaces and lowercasing string value to match the switch-cases
                switch (clientcommand.Replace(" ", "").ToLower())
                {
                    case "get":
                        sw.WriteLine("Give ISBN");
                        sw.Flush();
                        sw.WriteLine(GetBook(sr.ReadLine()));
                        sw.Flush();
                        break;
                    case "getall":
                        sw.Write("Press enter");
                        sw.Flush();
                        sr.ReadLine();
                        sw.WriteLine(GetAllBooks());
                        sw.Flush();
                        break;
                    case "save":
                        sw.WriteLine(SaveBook(sr.ReadLine()));
                        sw.Flush();
                        break;
                    default:
                        sw.WriteLine("Unknown command");
                        sw.Flush();
                        break;
                }
            }
        }

        private string GetBook(string isbn)
        {
            Bog bog = _bogs.Find(c => c.Isbn13 == isbn);
            if (bog != null)
            {
                return JsonConvert.SerializeObject(bog);
            }
            else
            {
                new ArgumentException("Cannot find ISBN");
                return null;
            }
        }

        private string GetAllBooks()
        {
            // using stringbuilder as return value, listing all books
            StringBuilder sb = new StringBuilder();

            foreach (Bog bog in _bogs)
            {
                sb.Append(JsonConvert.SerializeObject(bog));
            }

            return sb.ToString();
        }

        // method return string message after completing appending to list
        private string SaveBook(string book)
        {
            _bogs.Add(JsonConvert.DeserializeObject<Bog>(book));
            return "Book has been saved!";
        }

        private void CommandGuide(StreamWriter sw)
        {
            sw.WriteLine("Welcome");
            sw.WriteLine("");
            sw.WriteLine("Write 'GET' & then write the ISBN");
            sw.WriteLine("Write 'GETALL'");
            sw.WriteLine("Write 'SAVE' & then write book data in JSON");
            sw.WriteLine("'SAVE' example: {Author:\"bob\",Isbn:\"1231231231234\",\"Pages\":\"25\",\"Title\":\"Book of the ages\"}");
        }

    }
}
