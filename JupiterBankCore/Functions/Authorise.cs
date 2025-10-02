using JupiterBank.Shared.Models;

namespace JupiterBankCore.Functions;

public class AuthoriseFunction
{
    public static void Run(string payload)
    {
        Dictionary<string, string> dataDict = payload.Split(',').Select(part => part.Split('=')).ToDictionary(split => split[0], split => split[1]);

        IncomingTransaction transactionObject = new IncomingTransaction
        {
            TransactionId = dataDict.GetValueOrDefault("transactionId"),
            MerchantId = dataDict.GetValueOrDefault("merchantId"),
            Amount = decimal.Parse(dataDict.GetValueOrDefault("amount", "0")),
            Currency = dataDict.GetValueOrDefault("currency"),
            Pan = dataDict.GetValueOrDefault("pan"),
            ExpiryMonth = int.Parse(dataDict.GetValueOrDefault("expiryMonth", "0")),
            ExpiryYear = int.Parse(dataDict.GetValueOrDefault("expiryYear", "0")),
            Cvv = int.Parse(dataDict.GetValueOrDefault("cvv", "0"))
        };

        Console.WriteLine(transactionObject.TransactionId);
        
        return;
    }
}