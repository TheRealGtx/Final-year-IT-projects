//Test with a client that sends a static message and a server that receives it, server side.
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
            string data = null;
            //Data buffer for incoming data
            byte[] bytes = new byte[1024];

            WriteLine("\nPROGRAMMA SERVER\n");

            //Estabilish the local endpoint for the socket.
            //Dns.GetHostName returns the name of the
            //host running the application

            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[1];

            //IPAddress iPAddress = IPAddress.Parse("192.168.56.1");

            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            //Create a TPC/IP socket
            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            //Bind the socket to the local endpoint and
            //listen for incoming connection
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                //Start listening for connections.
                while(true)
                {
                    WriteLine("Waiting for a connection...");
                    //Program is suspended while waiting for an incoming connection
                    Socket handler = listener.Accept();
                    data = null;

                    //An incoming connection needs to be processed
                    do
                    {
                        int bytesRec = handler.Receive(bytes);
                        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    }
                    while((data.IndexOf("<EOF>") <= -1));

                    //Show the data on the console
                    WriteLine("Text received: {0}", data);


                    //Echo the data back to the client
                    byte[] msg = Encoding.ASCII.GetBytes(data);

                    handler.Send(msg);
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();

                    //choice = Readkey(true).Key;
                }
            }
            catch (Exception e)
            {
                WriteLine(e.ToString());
            }

            WriteLine("\nPress any key to continue...");
            ReadKey();
        }
    }
}
