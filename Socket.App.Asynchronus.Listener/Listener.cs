using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Socket.App.Asynchronus.Listener
{
    public class Listener
    {
        // Incoming data from the client.  
        private static string _data = null;  

        public static void Listen()
        {
            byte[] bytes = new byte[1024];

            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());  
            IPAddress ipAddress = ipHostInfo.AddressList[0];
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
                    _data = null;
                    new Thread(() => { DoWork(handler, bytes);}).Start();
                }

            } catch (Exception e) {  
                Console.WriteLine(e.ToString());  
            }  
      
            Console.WriteLine("\nPress ENTER to continue...");  
            Console.Read();
        }

        public static void DoWork(System.Net.Sockets.Socket handler, byte[] bytes)
        {
                _data = String.Empty;
                int bytesRec = handler.Receive(bytes);  
                _data += Encoding.ASCII.GetString(bytes,0,bytesRec);  

                // Show the data on the console.  
                Console.WriteLine( "Text received : {0}", _data);

                // Echo the data back to the client.  
                byte[] msg = Encoding.ASCII.GetBytes(_data);

                handler.Send(msg);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
        }
    }
}