using Microsoft.EntityFrameworkCore;
using Minio;
using Seiun.Entities;
using Seiun.Models.Responses;

namespace Seiun.Repositories;


public class CommentLikeRepository(SeiunDbContext dbContext , IMinioClient minioClient)
    : BaseRepository<CommentLikeEntity>(dbContext , minioClient) , ICommentLikeRepository
{
    public async Task<CommentLikeEntity?> GetCommentLikeAsync(Guid userId, Guid commentId)
    {
        return await DbContext.Set<CommentLikeEntity>()
            .FirstOrDefaultAsync(like => like.UserId == userId && like.CommentId == commentId);
    }
}

public class CommentRepository(SeiunDbContext dbContext , IMinioClient minioClient)
    : BaseRepository<CommentEntity>(dbContext , minioClient) , ICommentRepository
{
    public async Task<IEnumerable<CommentEntity>> GetListByPostIdAsync(Guid postId)
    {
        var comments = await DbContext.Comments
            .Where(comment => comment.PostId == postId)
            .ToListAsync();

        if (comments.Count == 0)
        {
            return [];
        }
        return comments;
    }
}
