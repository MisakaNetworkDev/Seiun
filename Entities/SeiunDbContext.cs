using Microsoft.EntityFrameworkCore;

namespace Seiun.Entities;

public class SeiunDbContext(DbContextOptions<SeiunDbContext> options) : DbContext(options)
{
    public required DbSet<UserEntity> Users { get; init; }
    public required DbSet<ArticleEntity> Articles { get; init; }
    public required DbSet<UserArticleStatusEntity> UserArticleStatus {get; init;}
    public required DbSet<PublicAnnouncementEntity> PublicAnnouncements {get; init;}
    
    public required DbSet<CommentEntity> Comments { get; set; }
    public required DbSet<CommentLikeEntity> CommentLike { get; set; }
    public required DbSet<ReplyEntity> Replies { get; set; }
    public required DbSet<UserTagEntity> UserTagEntity { get; set; }
    public required DbSet<TagEntity> TagEntity { get; set; }

    public required DbSet<UserWordRecordEntity> UserWordRecords { get; set; }
    public required DbSet<StudyStageEntity> StudyStage { get; set; }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     base.OnModelCreating(modelBuilder);
    //     
    // }
}