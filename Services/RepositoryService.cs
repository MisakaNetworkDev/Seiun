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
}