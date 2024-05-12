//Manzi Giuliano 5i, Server side of a chat application. THe chat ends when "fine" is sent as a message.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using static System.Console;

namespace ConsoleAppServer
{
    class Server
    {
        static void Main(string[] args)
        {
            string data = " ";
            byte[] bytes = new byte[1024];

            WriteLine("Manzi Giuliano 5i");
            WriteLine("\nPROGRAMMA SERVER\n");

            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[1];

            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                while (true)
                {
                    WriteLine("Waiting for a connection...");
                    Socket handler = listener.Accept();
                    data = " ";
                    string messaggio = " ";
                    while (data.ToUpper().Trim() != "FINE<EOF>" && messaggio.ToUpper().Trim() != "FINE<EOF>")
                    {
                        data = " ";
                        do
                        {
                            int bytesRec = handler.Receive(bytes);
                            data += Encoding.UTF8.GetString(bytes, 0, bytesRec);
                        }
                        while ((data.IndexOf("<EOF>") <= -1));

                        ForegroundColor = ConsoleColor.Green;
                        Write("\nClient: ");
                        ForegroundColor = ConsoleColor.White;
                        WriteLine(data.Substring(0, data.Length - 5) + "\n");

                        if (data.ToUpper().Trim() != "FINE<EOF>")
                        {
                            ForegroundColor = ConsoleColor.Red;
                            Write("Messaggio da inviare al client: ");
                            ForegroundColor = ConsoleColor.White;
                            messaggio = ReadLine() + "<EOF>";
                            byte[] msg = Encoding.UTF8.GetBytes(messaggio);

                            handler.Send(msg);
                        }
                    }
                    WriteLine("\nFine della chat\n");
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception e)
            {
                WriteLine("Errore: {0}", e.ToString());
            }
            WriteLine("\nPress any key to continue...");
            ReadKey();
        }
    }
}
