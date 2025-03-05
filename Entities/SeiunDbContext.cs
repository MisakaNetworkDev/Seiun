using Microsoft.EntityFrameworkCore;

namespace Seiun.Entities;

public class SeiunDbContext(DbContextOptions<SeiunDbContext> options) : DbContext(options)
{
    public required DbSet<UserEntity> Users { get; init; }
    public required DbSet<ArticleEntity> Articles { get; init; }
    public required DbSet<ArticleLikeEntity> ArticleLikes { get; init; }
    public required DbSet<PublicAnnouncementEntity> PublicAnnouncements {get; init;}
    
    public required DbSet<CommentEntity> Comments { get; set; }
    public required DbSet<CommentLikeEntity> CommentLike { get; set; }
    public required DbSet<ReplyEntity> Replies { get; set; }
    
    public required DbSet<UserTagEntity> UserTag { get; set; }
    public required DbSet<TagEntity> Tag { get; set; }

    public required DbSet<FinishedWordRecordEntity> UserWordRecords { get; set; }
    public required DbSet<SessionEntity> Sessions { get; set; }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     base.OnModelCreating(modelBuilder);
    //     
    // }
}