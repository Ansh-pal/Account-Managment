public class Account
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string AccountName { get; set; }

    public decimal Balance { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; }
}