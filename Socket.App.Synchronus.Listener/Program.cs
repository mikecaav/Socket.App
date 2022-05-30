using System;
using System.Diagnostics;

namespace Socket.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start listening");
            Listener.StartListening();
            Console.WriteLine("Stopped listening");
        }
    }
}