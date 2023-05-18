using System;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace SimpleHttpServer {

    public class SimpleHttpServer {
        public static string fileName;
        public static void Main() {
            fileName = "snake.log";
            Console.WriteLine("SimpleHttpServer startet...");
            TcpListener listener = new TcpListener(8080);
            listener.Start();
            while(true) {
                TcpClient client = listener.AcceptTcpClient();
                Thread handler = new Thread(new HttpHandler(client).Do);
                handler.Start();
            }
        }
    }
}
