using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using JupiterBankCore.Data;
using Microsoft.EntityFrameworkCore;

namespace JupiterBankCore;

class JupiterBankCore
{
    public static void Main()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseNpgsql("Host=localhost;Database=JupiterBank;Username=root;Password=root")
    .Options;

        using var db = new AppDbContext(options);

        // Test connection
        db.Database.EnsureCreated();

        Console.WriteLine("Database ready.");

        TcpListener? server = null;
        try
        {
            // Set the TcpListener on port 13000.
            Int32 port = 13000;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            server = new TcpListener(localAddr, port);

            // Start listening for client requests.
            server.Start();
            Console.WriteLine("Server started on {0}:{1}", localAddr, port);

            // Enter the listening loop.
            while (true)
            {
                Console.WriteLine("Waiting for a connection...");

                // Accept client connection
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("New client connected");

                // Handle each client in a separate thread
                HandleClient clientHandler = new HandleClient(client);
                clientHandler.Start();
            }
        }
        catch (SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
        }
        finally
        {
            server?.Stop();
        }
    }
}