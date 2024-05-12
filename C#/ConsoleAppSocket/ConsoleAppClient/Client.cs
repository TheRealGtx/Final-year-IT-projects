//Test with a client that sends a static message and a server that receives it, client side.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Runtime.Remoting.Channels;

namespace ConsoleAppClient
{
    class Client
    {
        static void Main(string[] args)
        {
            string data = null;
            //Data buffer for incoming data
            byte[] bytes = new byte[1024];

            WriteLine("\nPROGRAMMA CLIENT\n");

            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[1];

            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

            //Create a TPC/IP socket
            Socket sender = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);
           
            try
            {
                sender.Connect(remoteEP);

                WriteLine("Socket connected to {0}",
                    sender.RemoteEndPoint.ToString());

                //Encode the data string into a byte array
                byte[] msg = Encoding.ASCII.GetBytes("This is a test<EOF>");

                //Send the data through the socket
                int byteSent = sender.Send(msg);

                //receive the response from the remote device

                do
                {
                    int bytesRec = sender.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                } while(data.IndexOf("<EOF>") <= -1);

                WriteLine("Echoed test = {0}", data);

                //Release the socket
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
                WriteLine("\nPress any key to continue...");
                ReadKey();
            }
            catch (ArgumentException ane)
            {
                WriteLine("ArgumentNullException: {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                WriteLine("SocketException: {0}", se.ToString());
                WriteLine("\nPress any key to continue...");
                ReadKey();
            }
            catch (Exception e)
            {
                WriteLine("Unexpected exception: {0}", e.ToString());
                WriteLine("\nPress any key to continue...");
                ReadKey();
            }
        }
    }
}
