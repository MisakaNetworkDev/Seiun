using System.Net.Mime;
using Microsoft.EntityFrameworkCore;
using Minio;
using Minio.DataModel.Args;
using Seiun.Entities;
using Seiun.Utils;

namespace Seiun.Repositories;

public class ReplyRepository(SeiunDbContext dbContext, IMinioClient minioClient)
    : BaseRepository<ReplyEntity>(dbContext, minioClient), IReplyRepository
{
    public async Task<IEnumerable<ReplyEntity>> GetListByCommentIdAsync(Guid commentId)
    {
        var replyies = await DbContext.Set<ReplyEntity>()
            .Where(reply => reply.CommentId == commentId)
            .ToListAsync();

        if (replyies.Count == 0)
        {
            return [];
        }
        return replyies;
    }
}