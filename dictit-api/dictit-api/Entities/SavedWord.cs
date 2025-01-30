using System.ComponentModel.DataAnnotations;

namespace DictItApi.Entities;

public class SavedWord
{
    [Key]
    public int Id { get; set; }
    public string Word { get; set; } = string.Empty;

    public User User { get; set; } = new User();
}
