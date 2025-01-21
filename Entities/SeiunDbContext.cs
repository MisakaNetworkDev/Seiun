using Microsoft.EntityFrameworkCore;

namespace Seiun.Entities;

public class SeiunDbContext(DbContextOptions<SeiunDbContext> options) : DbContext(options)
{
    public required DbSet<UserEntity> Users { get; init; }
    public required DbSet<CommentEntity> Comments { get; set; }
    public required DbSet<CommentLikeEntity> CommentLike { get; set; }
    public required DbSet<ReplyEntity> Replies { get; set; }
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     base.OnModelCreating(modelBuilder);
    //     
    // }
}