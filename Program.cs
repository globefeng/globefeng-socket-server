using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 80);

            //server.Start();
            //Console.WriteLine("Server has started on 127.0.0.1:80.{0}Waiting for a connection…", Environment.NewLine);

            //TcpClient client = server.AcceptTcpClient();

            //Console.WriteLine("A client connected.");

            ExecuteServer();

        }

        public static void ExecuteServer()
        {
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            Socket listener = new Socket(localEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {

                // Using Bind() method we associate a
                // network address to the Server Socket
                // All client that will connect to this 
                // Server Socket must know this network
                // Address
                listener.Bind(localEndPoint);

                // Using Listen() method we create 
                // the Client list that will want
                // to connect to Server
                listener.Listen(10);

                while (true)
                {

                    Console.WriteLine("Waiting connection ... ");

                    // Suspend while waiting for
                    // incoming connection Using 
                    // Accept() method the server 
                    // will accept connection of client
                    Socket clientSocket = listener.Accept();

                    byte[] bytes = new Byte[1024];
                    string data = null;

                    //while (true)
                    {

                        int numByte = clientSocket.Receive(bytes);

                        data += Encoding.ASCII.GetString(bytes,
                                                   0, numByte);

                        //if (data.IndexOf("<EOF>") > -1)
                        //    break;
                    }

                    Console.WriteLine("Text received -> {0} ", data);

                    byte[] message = Encoding.ASCII.GetBytes("Test Server");

                    // Send a message to Client 
                    // using Send() method
                    clientSocket.Send(message);

                    // Close client Socket using the
                    // Close() method. After closing,
                    // we can use the closed Socket 
                    // for a new Client Connection
                    //clientSocket.Shutdown(SocketShutdown.Both);
                    //clientSocket.Close();
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
