using Microsoft.EntityFrameworkCore;

namespace LootBoxSimulator.Models.DAO;

public class RemoteDatabaseContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=45.67.56.214;Port=5424;Username=user14;Password=PPFlStRD;Database=user14");
    }
    public DbSet<ItemDao> Items { get; set; }
    public DbSet<RateDao> Rates { get; set; }
    public DbSet<CategoryDao> Categories { get; set; }
    public DbSet<ChestDao> Chest { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ItemDao>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<ItemDao>()
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<RateDao>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<CategoryDao>()
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<CategoryDao>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<RateDao>()
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();
             modelBuilder.Entity<ChestDao>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<ChestDao>()
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();
    }
}