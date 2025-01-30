using DictItApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace DictItApi.Database;

public class SaveWordDbContext : DbContext
{
    public SaveWordDbContext(DbContextOptions<SaveWordDbContext> options) : base(options) { }

    public DbSet<SavedWord> SavedWords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}
