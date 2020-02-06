using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPClient
{
    class Program
    {
        static void Main(string[] args)
        {
            const string ip = "127.0.0.1";
            const int port = 8080;

            var TCPEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            // Создаём сокет
            var TCPSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            Console.WriteLine("Введите Ваше сообщение:");
            var msg = Console.ReadLine();

            var data = Encoding.UTF8.GetBytes(msg);

            TCPSocket.Connect(TCPEndPoint);
            TCPSocket.Send(data);

            var buff = new byte[256];   // Буфер для получения данных
            var size = 0;   // Количество реально полученных байт
            var answer = new StringBuilder();   // Ответ с сервера

            // Получаем данные
            do
            {
                size = TCPSocket.Receive(buff);
                answer.Append(Encoding.UTF8.GetString(buff, 0, size));
            }
            while (TCPSocket.Available > 0);

            Console.WriteLine(answer.ToString());

            TCPSocket.Shutdown(SocketShutdown.Both);
            TCPSocket.Close();

            Console.ReadLine();
        }
    }
}
