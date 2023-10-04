using System;
using System.Net;
using System.Net.Sockets;

class Program
{
    static void Main()
    {
        // Define the IP address and port to listen on
        IPAddress ipAddress = IPAddress.Parse("192.168.1.16");
        int port = 8000;

        // Create a TCP listener
        TcpListener listener = new TcpListener(ipAddress, port);

        try
        {
            // Start listening for incoming connctions
            listener.Start();
            Console.WriteLine($"Listening on {ipAddress}:{port}");

            while (true)
            {
                // Accept incoming client connections
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine($"Accepted connection from {((IPEndPoint)client.Client.RemoteEndPoint).Address}:{((IPEndPoint)client.Client.RemoteEndPoint).Port}");

                // Handle the client connection in a separate thread or method
                // For example, you can create a new thread to handle each client:
                // Thread clientThread = new Thread(() => HandleClient(client));
                // clientThread.Start();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            // Stop listening when done
            listener.Stop();
        }
    }

    // Define a method to handle client communication
    // You can implement your specific logic here
    static void HandleClient(TcpClient client)
    {
        // Implement your communication logic with the client here
        // You can use the client.GetStream() method to read and write data
    }
}
