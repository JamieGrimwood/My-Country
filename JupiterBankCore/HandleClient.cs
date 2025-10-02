using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using JupiterBankCore.Functions;

namespace JupiterBankCore;

public class HandleClient
{
    private TcpClient _client;      // only this class can touch this
    private NetworkStream? _stream; // only this class can touch this

    public HandleClient(TcpClient client) // anyone can call this constructor
    {
        _client = client;
        _stream = _client.GetStream();
    }

    public void Start()  // anyone can call Start()
    {
        Thread clientThread = new Thread(HandleConnection);
        clientThread.Start();
    }

    private void HandleConnection() // only this class can run this method
    {
        try
        {
            using (_stream)
            using (StreamReader reader = new StreamReader(_stream!, Encoding.UTF8))
            using (StreamWriter writer = new StreamWriter(_stream!, Encoding.UTF8) { AutoFlush = true })
            {
                Console.WriteLine("[Thread {0}] Client connected from {1}", Thread.CurrentThread.ManagedThreadId, _client.Client.RemoteEndPoint);
                while (true)
                {
                    string? header = ReadUntilDelimiter(reader, '|');
                    if (header == null) return;

                    if (!int.TryParse(header, out int length))
                    {
                        Console.WriteLine($"Invalid length header: {header}");
                        return;
                    }

                    // Read the rest of the message body
                    char[] buffer = new char[length];
                    int read = reader.ReadBlock(buffer, 0, length);
                    if (read < length) break; // connection closed prematurely

                    string message = new string(buffer, 0, read);

                    Console.WriteLine($"Received: {message}");

                    string[] parts = message.Split("|");

                    if (parts[0] == "SALE")
                    {
                        Console.WriteLine($"Sale Initiated: {parts[1]}");
                    }
                    else if (parts[0] == "AUTHORISE")
                    {
                        AuthoriseFunction.Run(parts[1]);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Error] Client connection failed: {ex.Message}");
        }
        finally
        {
            _client.Close();
        }
    }
    private string? ReadUntilDelimiter(StreamReader reader, char delimiter)
    {
        StringBuilder sb = new StringBuilder();
        while (true)
        {
            int ch = reader.Read();
            if (ch == -1) return null; // end of stream
            if (ch == delimiter) break;
            sb.Append((char)ch);
        }
        return sb.ToString();
    }
}
