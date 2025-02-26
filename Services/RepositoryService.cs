using Minio;
using Seiun.Entities;
using Seiun.Repositories;

namespace Seiun.Services;

public class RepositoryService(SeiunDbContext seiunDbContext, IMinioClient minioClient) : IRepositoryService
{
    private readonly Lazy<IUserRepository> _userRepository = new(() => new UserRepository(seiunDbContext, minioClient));
    public IUserRepository UserRepository => _userRepository.Value;
    private readonly Lazy<IArticleRepository> _articleRepository = new(() => new ArticleRepository(seiunDbContext, minioClient));
    public IArticleRepository ArticleRepository => _articleRepository.Value;

    private readonly Lazy<IUserArticleStatusRepository> _UserArticleStatusRepository = new(() => new UserArticleStatusRepository(seiunDbContext, minioClient));
    public IUserArticleStatusRepository UserArticleStatusRepository => _UserArticleStatusRepository.Value;

    private readonly Lazy<IPublicAnnouncementRepository> _publicAnnouncementRepository = new(() => new PublicAnnouncementRepository(seiunDbContext, minioClient));
    public IPublicAnnouncementRepository PublicAnnouncementRepository => _publicAnnouncementRepository.Value;
    private readonly Lazy<ICommentRepository> _commentRepository = new(() => new CommentRepository(seiunDbContext, minioClient));
    public ICommentRepository CommentRepository => _commentRepository.Value;

    private readonly Lazy<ICommentLikeRepository> _commentLikeRepository = new(() => new CommentLikeRepository(seiunDbContext, minioClient));
    public ICommentLikeRepository CommentLikeRepository => _commentLikeRepository.Value;

    private readonly Lazy<IReplyRepository> _replyRepository = new(() => new ReplyRepository(seiunDbContext, minioClient));
    public IReplyRepository ReplyRepository => _replyRepository.Value;

    private readonly Lazy<IWordRepository> _wordRepository = new(() => new WordRepository(seiunDbContext, minioClient));
    public IWordRepository WordRepository => _wordRepository.Value;

    private readonly Lazy<ITagRepository> _tagRepository = new(() => new TagRepository(seiunDbContext, minioClient));
    public ITagRepository TagRepository => _tagRepository.Value;

    private readonly Lazy<IUserWordRepository> _UserWordRepository = new(() => new UserWordRepository(seiunDbContext, minioClient));
    public IUserWordRepository UserWordRepository => _UserWordRepository.Value;
}