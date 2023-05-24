using System;
using System.Diagnostics;

namespace App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello to the snake game!");

            // Start the HTTP server in a new thread
            Thread httpServerThread = new Thread(SimpleHttpServer.SimpleHttpServer.Main);
            httpServerThread.Start();

            while (true)
            {
                new App().Run();
            }
        }
    }
}