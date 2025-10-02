namespace JupiterBank.Shared.Models;

public enum CardType
{
    Debit,
    Credit
}

public class Card
{
    public int Id { get; set; }
    public string CardNumber { get; set; } = null!;
    public int ExpiryMonth { get; set; }
    public int ExpiryYear { get; set; }
    public string CVV { get; set; } = null!;
    public CardType CardType { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int BankAccountId { get; set; }
    public BankAccount BankAccount { get; set; } = null!;
}
