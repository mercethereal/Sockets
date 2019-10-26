using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Xml;
using System.Diagnostics;

namespace TurnCommerce
{
    class Program
    {
        private static Dictionary<int, string> testanswer = new Dictionary<int, string>();
        private static String xmlRequestBegin = @"<?xml version=""1.0""encoding=""ISO-8859-1""?><request><requestID>";
        private static String xmlRequestEnd = @"</requestID></request>";
        private static string host = "216.38.192.141";
        private static int port = 8765;
        private const int loopcontrol = 500;
        private static Stopwatch sw = new Stopwatch();      
        
        static void Main(string[] args)
        {
            sw.Start();
            SendMessage();
            Console.WriteLine("Press Any Key to quit");
            Console.ReadLine();
            writeanswer();
        }


        private static async void SendMessage()
        {
            Task t;
            for (int i=1; i <= loopcontrol; i++)
            {
                TcpClient tcpClient = new TcpClient(host, port);
                NetworkStream NetStream = tcpClient.GetStream();
                string message = xmlRequestBegin + i.ToString() + xmlRequestEnd;
                //Console.WriteLine(message);
                byte[] ClientRequestBytes = Encoding.UTF8.GetBytes(message);
                t = NetStream.WriteAsync(ClientRequestBytes, 0, ClientRequestBytes.Length);
                var buffer = new byte[1000];
                int byteCount = 0;
                await NetStream.ReadAsync(buffer, 0, buffer.Length);
                var response = Encoding.UTF8.GetString(buffer, 0, byteCount);
                //Console.WriteLine("[Client] Server response was {0}", response);
                using (XmlReader reader = XmlReader.Create(new StringReader(response)))
                {
                    reader.ReadToFollowing("response");
                    reader.ReadToFollowing("responseID");
                    int ID = reader.ReadElementContentAsInt();
                    reader.ReadToFollowing("message");
                    Console.WriteLine(ID.ToString());
                    testanswer.Add(ID, reader.ReadElementContentAsString().Substring(2, 1));
                } 
            }
            await t;
        }


 

        private static void writeanswer()
        { 

            using (System.IO.StreamWriter answerfile = new System.IO.StreamWriter(@"C:\sharp\sockets\answer.txt"))
            { 
                
                for (int i = loopcontrol; i >= 1; i--)
                {
                    try
                    {
                        answerfile.Write(testanswer[i]);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.InnerException);
                    }

                }
                sw.Stop();
                string elapsedTime = sw.ElapsedMilliseconds.ToString();
                answerfile.WriteLine();
                answerfile.WriteLine("Programmed finished in {0} milliseconds",elapsedTime);

            }
        }
    }
}
