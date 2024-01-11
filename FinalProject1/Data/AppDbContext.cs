using Microsoft.EntityFrameworkCore;
using FinalProject1.Models;

public class AppDbContext : DbContext{    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)    {    }
    public DbSet<EndUser> Users { get; set; }     public DbSet<Playlist> Playlists { get; set; }    public DbSet<Comments> Comments { get; set; }
    public DbSet<BlogPost> BlogPost { get; set; }
    public DbSet<Reply> Replies { get; set; }
    public DbSet<Song> Songs { get; set; }
    public DbSet<Likes> Likes { get; set; }
}