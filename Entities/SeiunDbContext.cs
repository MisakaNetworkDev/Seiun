using Microsoft.EntityFrameworkCore;

namespace Seiun.Entities;

public class SeiunDbContext(DbContextOptions<SeiunDbContext> options) : DbContext(options)
{
    public required DbSet<UserEntity> Users { get; init; }
    public required DbSet<ArticleEntity> Articles { get; init; }
    public required DbSet<UserArticleStatusEntity> UserArticleStatus {get; init;}
    public required DbSet<PublicAnnouncementEntity> PublicAnnouncements {get; init;}
    
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     base.OnModelCreating(modelBuilder);
    //     
    // }
}