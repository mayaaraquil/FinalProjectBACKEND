using Microsoft.EntityFrameworkCore;
using FinalProject1.Models;

public class AppDbContext : DbContext{    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)    {    }
    public DbSet<User> Users { get; set; }     public DbSet<Playlist> Playlists { get; set; }
    //public DbSet<Product> Products { get; set; }
}