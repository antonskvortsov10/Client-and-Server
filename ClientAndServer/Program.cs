using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientAndServer
{
    class Program
    {
        static void Main(string[] args)
        {
            const string ip = "127.0.0.1";
            const int port = 8080;
            const int backlog = 5;

            var TCPEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            // Создаём сокет
            var TCPSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            TCPSocket.Bind(TCPEndPoint);
            TCPSocket.Listen(backlog);

            while (true)
            {
                var listener = TCPSocket.Accept();
                var buff = new byte[256];   // Буфер для получения данных
                var size = 0;   // Количество реально полученных байт
                var data = new StringBuilder();

                // Считываем, пока в подключении есть данные
                do
                {
                    size = listener.Receive(buff);
                    data.Append(Encoding.UTF8.GetString(buff, 0, size));
                }
                while (listener.Available > 0);

                Console.WriteLine(data);

                listener.Send(Encoding.UTF8.GetBytes("Successfully!")); // Обратный ответ

                listener.Shutdown(SocketShutdown.Both);
                listener.Close();
            }
        }
    }
}
