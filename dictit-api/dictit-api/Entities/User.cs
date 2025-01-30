using Microsoft.AspNetCore.Identity;

namespace DictItApi.Entities;

public class User : IdentityUser
{
    public string Name { get; set; } = string.Empty;

    public ICollection<SavedWord> SavedWords { get; set; } = [];
}
