using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        IPAddress ipAddress = IPAddress.Parse("192.168.1.22");
        int port = 8000;

        // if th
        TcpListener listener = new TcpListener(ipAddress, port);

        try
        {
            listener.Start();
            Console.WriteLine($"Listening on {ipAddress}:{port}");

            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                Console.WriteLine($"Accepted connection from {((IPEndPoint)client.Client.RemoteEndPoint).Address}:{((IPEndPoint)client.Client.RemoteEndPoint).Port}");

                _ = HandleClientAsync(client);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            listener.Stop();
        }
    }

static async Task HandleClientAsync(TcpClient client)
{
    try
    {
        using (NetworkStream stream = client.GetStream())
        {
            byte[] buffer = new byte[1024];
            int bytesRead;

            while (true)
            {
                bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                if (bytesRead <= 0)
                    break;

                Console.WriteLine("Received data:");

                for (int i = 0; i < bytesRead; i += 16)
                {
                    Console.Write($"{i:X8}: ");
                    for (int j = i; j < i + 16 && j < bytesRead; j++)
                    {
                        Console.Write($"{buffer[j]:X2} ");
                    }
                    Console.Write("  ");
                    for (int j = i; j < i + 16 && j < bytesRead; j++)
                    {
                        char c = (char)buffer[j];
                        Console.Write(char.IsControl(c) ? '.' : c);
                    }
                    Console.WriteLine();
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
    finally
    {
        client.Close();
    }
}

}