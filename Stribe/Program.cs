using System.IO;
using System.Net.Sockets;
using System.Text;

class Stribe
{
    public static void Main()
    {
        string action = "AUTHORISE";
        var payloadData = new Dictionary<string, string>
        {
            { "transactionId", "txn_ghi789" },
            { "merchantId", "merch_shop456" },
            { "amount", "5.00" },
            { "currency", "GBP" },
            { "pan", "4567673664788726" },
            { "expiryMonth", "12" },
            { "expiryYear", "2028" },
            { "cvv", "123" }
        };

        string payload = string.Join(",", payloadData.Select(kvp => $"{kvp.Key}={kvp.Value}"));

        string body = $"{action}|{payload}";
        int bodyLength = body.Length;
        string message = $"{bodyLength}|{body}";

        Console.WriteLine($"[Client] Sending: {message}");
    }
}