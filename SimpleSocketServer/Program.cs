using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main()
    {
        // פתח סוקט לקבלת חיבורים מלקוחות
        Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1"); // כתובת ה-IP של השרת
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 8080); // פורט להאזנה

        listener.Bind(localEndPoint);
        listener.Listen(10); // האזן לעד 10 חיבורים

        Console.WriteLine("השרת מאזין לחיבורים...");

        while (true)
        {
            // המתן לחיבור מלקוח
            Socket clientSocket = listener.Accept();

            byte[] bytes = new byte[1024];
            int bytesReceived = clientSocket.Receive(bytes);
            string data = Encoding.UTF8.GetString(bytes, 0, bytesReceived);

            Console.WriteLine($"התקבלה הודעה: {data}");

            clientSocket.Close(); // סגור את החיבור ללקוח
        }
    }
}
