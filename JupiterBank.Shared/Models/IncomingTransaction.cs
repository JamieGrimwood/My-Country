namespace JupiterBank.Shared.Models;

public class IncomingTransaction
{
    public string? TransactionId { get; set; }
    public string? MerchantId { get; set; }
    public decimal Amount { get; set; }
    public string? Currency { get; set; }
    public string? Pan { get; set; }
    public int ExpiryMonth { get; set; }
    public int ExpiryYear { get; set; }
    public int Cvv { get; set; }
}
