using System;
using System.Diagnostics;

namespace App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, to snake game!");
            StartWebserver();
            while (true)
            {
                new App().Run();
            }
        }

        static void StartWebserver()
        {
            Process webServer = new Process();
            webServer.StartInfo.FileName = "dotnet";
            webServer.StartInfo.Arguments = "/home/marc/netcore/SimpleHttpServer/SimpleHttpServer.dll";
            webServer.Start();
        }
    }
}