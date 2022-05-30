using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Socket.App
{
    public class Listener
    {
        // Incoming data from the client.  
        public static string data = null;  
      
        public static void StartListening() {
            byte[] bytes = new Byte[1024];

            IPAddress ipAddress = IPAddress.Any;
            Console.WriteLine($"Host started at {ipAddress} ip address");
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);  
      
            // Create a TCP/IP socket.  
            System.Net.Sockets.Socket listener = new System.Net.Sockets.Socket(ipAddress.AddressFamily,  
                SocketType.Stream, ProtocolType.Tcp );  
      
            // Bind the socket to the local endpoint and
            // listen for incoming connections.  
            try {  
                listener.Bind(localEndPoint);  
                listener.Listen(10);  
      
                // Start listening for connections.  
                while (true) {  
                    Console.WriteLine("Waiting for a connection...");  
                    // Program is suspended while waiting for an incoming connection.  
                    System.Net.Sockets.Socket handler = listener.Accept();  
                    data = null;  
      
                    // An incoming connection needs to be processed.  
                    while (handler.Connected) {
                        data = String.Empty;
                        int bytesRec = handler.Receive(bytes);  
                        data += Encoding.ASCII.GetString(bytes,0,bytesRec);  

                        // Show the data on the console.  
                        Console.WriteLine( "Text received : {0}", data);

                        // Echo the data back to the client.  
                        byte[] msg = Encoding.ASCII.GetBytes(data);

                        handler.Send(msg);

                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();
                    }
                }

            } catch (Exception e) {  
                Console.WriteLine(e.ToString());  
            }  
      
            Console.WriteLine("\nPress ENTER to continue...");  
            Console.Read();
        }
    }
}