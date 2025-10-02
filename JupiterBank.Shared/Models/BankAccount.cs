namespace JupiterBank.Shared.Models;

public enum AccountType
{
    Current,
    Savings
}

public class BankAccount
{
    public int Id { get; set; }
    public string AccountNumber { get; set; } = null!;
    public string SortCode { get; set; } = null!;
    public AccountType AccountType { get; set; }
    public decimal Balance { get; set; }
    public decimal OverdraftLimit { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Card> Cards { get; set; } = new List<Card>();
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
