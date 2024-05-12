//Manzi Giuliano 5i, Client side of a chat application. THe chat ends when "fine" is sent as a message.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Runtime.Remoting.Channels;
using System.Net.Configuration;
using System.Threading;

namespace ConsoleAppClient
{
    class Client
    {
        static void Main(string[] args)
        {
            string data = " ";
            string scelta = "";
            byte[] bytes = new byte[1024];

            WriteLine("Manzi Giuliano 5i");
            WriteLine("\nPROGRAMMA CLIENT\n");

            do
            {
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[1];

                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

                Socket sender = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    sender.Connect(remoteEP);
                    WriteLine("Socket connected to {0}",
                        sender.RemoteEndPoint.ToString());

                    do
                    {
                        data = " ";
                        ForegroundColor = ConsoleColor.Red;
                        Write("Messaggio da inviare al server: ");
                        ForegroundColor = ConsoleColor.White;
                        string messaggio = ReadLine() + "<EOF>";
                        byte[] msg = Encoding.UTF8.GetBytes(messaggio);

                        int byteSent = sender.Send(msg);
                        if (messaggio.ToUpper().Trim() != "FINE<EOF>")
                        {
                            do
                            {
                                int bytesRec = sender.Receive(bytes);
                                data += Encoding.UTF8.GetString(bytes, 0, bytesRec);
                            } while (data.IndexOf("<EOF>") <= -1);

                            ForegroundColor = ConsoleColor.Green;
                            Write("\nServer: ");
                            ForegroundColor = ConsoleColor.White;
                            WriteLine(data.Substring(0, data.Length - 5) + "\n");
                        }
                        else
                            data = "FINE<EOF>";
                    } while (data.ToUpper().Trim() != "FINE<EOF>");
                }
                catch (Exception e)
                {
                    WriteLine("Errore: {0}", e.ToString());
                }
                WriteLine("\nFine della chat\n");
                do
                {
                    Write("\nIniziarne un'altra? s/n: ");
                    scelta = ReadLine().ToUpper().Trim();
                    if (scelta != "S" && scelta != "N")
                        WriteLine("Effettuare una sceltas valida");
                } while (scelta != "S" && scelta != "N");
            } while (scelta == "S");
            
        }    
    }
}
