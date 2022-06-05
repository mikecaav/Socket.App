using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public class Client
    {
        private System.Net.Sockets.Socket sender;
        private IPEndPoint remoteEp;
        public Client()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());  
            IPAddress ipAddress = ipHostInfo.AddressList[0];  
            remoteEp = new IPEndPoint(ipAddress,11000);  
      
            // Create a TCP/IP  socket.  
            sender = new System.Net.Sockets.Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp );  
        }

        public void SendMessage(string message) {  
            // Data buffer for incoming data.  
            byte[] bytes = new byte[1024];  
      
            // Connect to a remote device.  
            try {

                // Connect the socket to the remote endpoint. Catch any errors.  
                try { 
                    sender.Connect(remoteEp);

                    if (sender.RemoteEndPoint != null)
                        Console.WriteLine("Socket connected to {0}",
                            sender.RemoteEndPoint.ToString());

                    // Encode the data string into a byte array.  
                    byte[] msg = Encoding.ASCII.GetBytes(message);  
  
                    // Send the data through the socket.  
                    int bytesSent = sender.Send(msg);  
  
                    // Receive the response from the remote device.  
                    int bytesRec = sender.Receive(bytes);  
                    Console.WriteLine("Echoed test = {0}",  
                        Encoding.ASCII.GetString(bytes,0,bytesRec));

                } catch (ArgumentNullException ane) {  
                    Console.WriteLine("ArgumentNullException : {0}",ane.ToString());  
                } catch (SocketException se) {  
                    Console.WriteLine("SocketException : {0}",se.ToString());  
                } catch (Exception e) {  
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());  
                }  
      
            } catch (Exception e) {  
                Console.WriteLine( e.ToString());  
            }
        }

        public void Close()
        {
            // Release the socket.  
            this.sender.Shutdown(SocketShutdown.Both);  
            this.sender.Close();
        }
    }
}