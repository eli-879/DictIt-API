using DictItApi.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DictItApi.Database;

public class DictItDbContext : IdentityDbContext<User>
{
    public DictItDbContext(DbContextOptions<DictItDbContext> options) : base(options) { }
    public DbSet<SavedWord> SavedWords { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
