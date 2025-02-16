namespace DictItApi.Entities;

public class SavedWord
{
    public int Id { get; set; }

    public string Word { get; set; } = string.Empty;

    public DateTime DateAdded { get; set; } = DateTime.UtcNow;

    public string UserId { get; set; } = string.Empty;

    public User User { get; set; } = new User();
}
