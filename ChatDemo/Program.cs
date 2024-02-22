using System.Net;
using System.Net.Sockets;
using System.Text;






class Program
{

    public static async Task SaveMsg(EndPoint adres, string msg)
    {
        string newIp = ModIP(adres);
        using var sw = new StreamWriter(String.Format($"C:/Users/IS22-11/source/repos/ChatApp/ChatApp/bin/Debug/net8.0/{newIp}.txt"), true);
        await sw.WriteAsync(adres.ToString() + '_' + msg + "\n");
        sw.Flush();


    }

    public static string ModIP(EndPoint adres)
    {

        return adres.ToString().Replace('.', '_').Substring(0, 15);




    }


    public static async Task Main()
    {
        var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, 1235);
        socket.Bind(ipPoint);
        socket.Listen();








        while (true)
        {
            using var tcpClient = await socket.AcceptAsync();
            System.Console.WriteLine("Есть подключиние");
            byte[] bytesRead = new byte[255];

            int count = await tcpClient.ReceiveAsync(bytesRead, SocketFlags.None);
            string msg = Encoding.UTF8.GetString(bytesRead);
            await Console.Out.WriteLineAsync("Принято сообщение");

            if (count > 0)
            {

                await SaveMsg(tcpClient.RemoteEndPoint, msg);

            }


        }


    }
}