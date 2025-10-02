namespace JupiterBank.Shared.Models;

public class Transaction
{
    public int Id { get; set; }
    public string TransactionId { get; set; } = null!;
    public string MerchantId { get; set; } = null!;
    public string Type { get; set; } = null!; // "SALE", "DEPOSIT", "WITHDRAW", "TRANSFER"
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "GBP";
    public DateTime Timestamp { get; set; }

    public int BankAccountId { get; set; }
    public BankAccount BankAccount { get; set; } = null!;
}
