using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;

namespace MusicCollection_DAL.Models;
public class SpotifyContext : DbContext
{
    public SpotifyContext(DbContextOptions<SpotifyContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<M_User>()
            .HasOne(u => u.Status)
            .WithMany(s => s.Users)
            .HasForeignKey(u => u.StatusId);

        modelBuilder.Entity<M_DbEntity>().UseTpcMappingStrategy();

        // Default values for M_Song
        modelBuilder.Entity<M_Song>()
            .Property(s => s.Poster)
            .HasDefaultValue("https://cdn.pixabay.com/photo/2014/04/02/14/04/vinyl-306070_1280.png");

        modelBuilder.Entity<M_Song>()
            .Property(s => s.Likes)
            .HasDefaultValue(0);

        modelBuilder.Entity<M_Song>()
            .Property(s => s.Plays)
            .HasDefaultValue(0);

        modelBuilder.Entity<M_Song>()
            .Property(s => s.Downloads)
            .HasDefaultValue(0);

        // Default values for M_Album
        modelBuilder.Entity<M_Album>()
            .Property(a => a.Poster)
            .HasDefaultValue("https://img.freepik.com/free-vector/realistic-music-record-label-disk-mockup_1017-33906.jpg?size=626&ext=jpg&ga=GA1.1.1908636980.1711929600&semt=ais");

        // Default values for M_DbEntity
        modelBuilder.Entity<M_DbEntity>()
            .Property(e => e.UploadDate)
            .HasDefaultValueSql("GETDATE()");

        modelBuilder.Entity<M_DbEntity>()
            .Property(e => e.DataUpdate)
            .HasDefaultValueSql("GETDATE()");

        // Default values for M_Genre
        modelBuilder.Entity<M_Genre>()
            .Property(g => g.Poster)
            .HasDefaultValue("https://cdn.venngage.com/template/thumbnail/small/bf008bfe-9bf6-4511-b795-e86f070bfff5.webp");

        modelBuilder.Entity<M_Genre>()
            .Property(g => g.BackgroundColor)
            .HasDefaultValue("#fff");


    }
    public DbSet<M_Album> Albums { get; set; }
    public DbSet<M_Artist> Artists { get; set; }
    public DbSet<M_Genre> Genres { get; set; }
    public DbSet<M_Song> Songs { get; set; }
    public DbSet<M_User> Users { get; set; }
    public DbSet<M_UserStatus> Statuses { get; set; }
}

public class SampleContextFactory : IDesignTimeDbContextFactory<SpotifyContext>
{
    public SpotifyContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SpotifyContext>();

        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json");
        var config = builder.Build();
        string connectionString = config.GetConnectionString("DefaultConnection");

        var optionsBulder = new DbContextOptionsBuilder<SpotifyContext>();
        optionsBulder.UseLazyLoadingProxies().UseSqlServer(connectionString);
        return new SpotifyContext(optionsBuilder.Options);
    }
}
