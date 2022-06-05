using System;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {

            Client client1 = new Client();
            Client client2 = new Client();
            Client client3 = new Client();

            client1.SendMessage("Mensaje Cliente 1");
            client2.SendMessage("Mensaje Cliente 2");
            client3.SendMessage("Mensaje Cliente 3");

            client1.Close();
            client2.Close();
            client3.Close();
        }
    }
}