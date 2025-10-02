using JupiterBank.Shared.Models;

namespace JupiterBank.Shared.DataTransferObjects;

public class CardDto
{
    public int Id { get; set; }
    public string CardNumber { get; set; } = null!;
    public int ExpiryMonth { get; set; }
    public int ExpiryYear { get; set; }
    public CardType CardType { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}
