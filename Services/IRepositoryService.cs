using Seiun.Repositories;

namespace Seiun.Services;

public interface IRepositoryService
{
    IUserRepository UserRepository { get; }
    IArticleRepository ArticleRepository { get; }
    IUserArticleStatusRepository UserArticleStatusRepository { get; }
    IPublicAnnouncementRepository PublicAnnouncementRepository { get; }
    ICommentRepository CommentRepository { get; }
    ICommentLikeRepository CommentLikeRepository { get; }
    IReplyRepository ReplyRepository { get; }
    IWordRepository WordRepository { get; }
}