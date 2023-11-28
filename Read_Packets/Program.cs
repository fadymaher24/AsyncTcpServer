using System;
using System.Net;
using System.Net.Sockets;

class Program
{
    static void Main()
    {
        // Set the IP address and port to connect to
        string ipAddress = "192.168.1.22"; // Change this to the target IP address
        int port = 8000; // Change this to the target port number
        
        try
        {
            // Create a TCP socket
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Console.Write("Connecting...");

            // Connect to the server
            socket.Connect(IPAddress.Parse(ipAddress), port);
            Console.WriteLine("Connected!");

            byte[] buffer = new byte[1024]; // Buffer for receiving data
            int bytesRead;
            
            while (true)
            {
                if (socket.Available > 0)
                {
                    
                    Console.WriteLine("Received data:");
                    bytesRead = socket.Receive(buffer);
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

            // Close the socket when done (this is unreachable in the current code)
            socket.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}




