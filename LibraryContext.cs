using EightApp.Demo.EfCoreCodeFirst01.Models;
using Microsoft.EntityFrameworkCore;

public class LibraryContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Publisher> Publishers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("BookDBConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Configure the Book table
        modelBuilder.Entity<Book>().ToTable("Books");

        modelBuilder.Entity<Book>()
            .HasKey(b => b.Id);

        modelBuilder.Entity<Book>()
            .Property(b => b.Title)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<Book>()
            .HasOne(b => b.Publisher)
            .WithMany(p => p.Books)
            .HasForeignKey(b => b.PublisherId)
            .IsRequired();

        modelBuilder.Entity<Book>()
            .HasMany(b => b.Authors)
            .WithMany(a => a.Books)
            .UsingEntity(j => j.ToTable("BookAuthor"));

        //Configure the Publisher table
        modelBuilder.Entity<Publisher>().ToTable("Publishers");

        modelBuilder.Entity<Publisher>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Publisher>()
            .Property(b => b.Name)
            .HasMaxLength(100)
            .IsRequired();

        //Configure the Author table
        modelBuilder.Entity<Author>().ToTable("Authors");

        modelBuilder.Entity<Author>()
            .HasKey(a => a.Id);

        modelBuilder.Entity<Author>()
            .Property(b => b.Name)
            .HasMaxLength(100)
            .IsRequired();
    }
}
