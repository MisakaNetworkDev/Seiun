using Minio;
using Seiun.Entities;
using Seiun.Repositories;

namespace Seiun.Services;

public class RepositoryService(SeiunDbContext seiunDbContext, IMinioClient minioClient) : IRepositoryService
{
    private readonly Lazy<IUserRepository> _userRepository = new(() => new UserRepository(seiunDbContext, minioClient));
    public IUserRepository UserRepository => _userRepository.Value;

    private readonly Lazy<ICommentRepository> _commentRepository = new(() => new CommentRepository(seiunDbContext, minioClient));
    public ICommentRepository CommentRepository => _commentRepository.Value;

    private readonly Lazy<ICommentLikeRepository> _commentLikeRepository = new(() => new CommentLikeRepository(seiunDbContext, minioClient));
    public ICommentLikeRepository CommentLikeRepository => _commentLikeRepository.Value;

    private readonly Lazy<IReplyRepository> _replyRepository = new(() => new ReplyRepository(seiunDbContext, minioClient));
    public IReplyRepository ReplyRepository => _replyRepository.Value;
}