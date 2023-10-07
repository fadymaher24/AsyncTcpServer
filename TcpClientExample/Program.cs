// On Windows:

// Open the Control Panel.
// Go to Network and Sharing Center.
// Click on "Change adapter settings."
// Right-click on your network adapter and select "Properties."
// Select "Internet Protocol Version 4 (TCP/IPv4)" and click "Properties."
// Choose the option to use a static IP address and enter the IP address, subnet mask, gateway, and DNS server addresses as needed.
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main()
    {
        string serverIpAddress = "192.168.1.130"; // Replace with the actual foreign IP address
        int port = 8000; // Replace with the port the remote server is listening on

        TcpClient client = new TcpClient();
        
        try
        {
            client.Connect(serverIpAddress, port);
            Console.WriteLine("Connected to server at " + serverIpAddress + ":" + port);

            // Handle client communication here
            NetworkStream stream = client.GetStream();

            // Send data to the server
            string message = "Hello, server!";
            byte[] data = Encoding.ASCII.GetBytes(message);
            stream.Write(data, 0, data.Length);

            // Receive data from the server
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Received from server: " + response);
        }
        catch (SocketException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            client.Close(); // Close the client connection
        }
    }
}
