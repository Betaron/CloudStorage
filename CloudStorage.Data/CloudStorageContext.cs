using CloudStorage.Data.Storages;
using CloudStorage.Data.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CloudStorage.Data;

public class CloudStorageContext : DbContext
{
    public DbSet<UserDbModel> Users { get; set; }
    public DbSet<StorageDbModel> Storages { get; set; }

    public CloudStorageContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modeBuilder)
    {
        modeBuilder.ApplyConfigurationsFromAssembly(typeof(CloudStorageContext).Assembly);
        base.OnModelCreating(modeBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);
        base.OnConfiguring(optionsBuilder);
    }
}

public class Factory : IDesignTimeDbContextFactory<CloudStorageContext>
{
    public CloudStorageContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder()
            .UseNpgsql("FakeConnectionStringForMigrations")
            .UseSnakeCaseNamingConvention()
            .Options;

        return new CloudStorageContext(options);
    }
}