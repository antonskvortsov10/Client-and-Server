using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDPClient
{
    class Program
    {
        static void Main(string[] args)
        {
            const string ip = "127.0.0.1";
            const int port = 8082;  // Поменяли порт

            var UDPEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);    // Создали подключение

            // Создаём сокет
            var UDPSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            UDPSocket.Bind(UDPEndPoint);    // Биндим с точкой

            while (true)
            {
                Console.WriteLine("Введите Ваше сообщение:");
                var msg = Console.ReadLine();

                var serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8081);
                UDPSocket.SendTo(Encoding.UTF8.GetBytes(msg), serverEndPoint);

                var buff = new byte[256];   // Буфер для получения данных
                var size = 0;   // Количество реально полученных байт
                var data = new StringBuilder();
                EndPoint senderEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8081);  // Здесь будет сохранён адрес клиента (и здесь, by the way, используется полиморфизм)

                do
                {
                    size = UDPSocket.ReceiveFrom(buff, ref senderEndPoint);
                    data.Append(Encoding.UTF8.GetString(buff));   // Продолжаем получать данные через StringBuilder
                }
                while (UDPSocket.Available > 0);

                Console.WriteLine(data);
                Console.ReadLine();
            }
        }
    }
}
