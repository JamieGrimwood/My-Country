using JupiterBank.Shared.Models; // For AccountType

namespace JupiterBank.Shared.DataTransferObjects;

public class BankAccountDto
{
    public int Id { get; set; }
    public string AccountNumber { get; set; } = null!;
    public string SortCode { get; set; } = null!;
    public AccountType AccountType { get; set; }
    public decimal Balance { get; set; }
    public decimal OverdraftLimit { get; set; }
    public DateTime CreatedAt { get; set; }

    public List<CardDto> Cards { get; set; } = new();
    public List<TransactionDto> Transactions { get; set; } = new();
}
