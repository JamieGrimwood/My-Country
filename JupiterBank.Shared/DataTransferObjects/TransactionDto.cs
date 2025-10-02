namespace JupiterBank.Shared.DataTransferObjects;

public class TransactionDto
{
    public int Id { get; set; }
    public string TransactionId { get; set; } = null!;
    public string Type { get; set; } = null!; // "SALE", "DEPOSIT", etc.
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "GBP";
    public DateTime Timestamp { get; set; }
}
