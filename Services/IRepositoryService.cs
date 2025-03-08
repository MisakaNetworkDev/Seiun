using Seiun.Repositories;

namespace Seiun.Services;

public interface IRepositoryService
{
    IUserRepository UserRepository { get; }
    IArticleRepository ArticleRepository { get; }
    IArticleLikeRepository ArticleLikeRepository { get; }
    IPublicAnnouncementRepository PublicAnnouncementRepository { get; }
    ICommentRepository CommentRepository { get; }
    ICommentLikeRepository CommentLikeRepository { get; }
    IReplyRepository ReplyRepository { get; }
    IWordRepository WordRepository { get; }
    ITagRepository TagRepository { get; }
    IUserTagRepository UserTagRepository { get; }
    ISessionRepository SessionRepository { get; }
    IErrorWordRepository ErrorWordRepository { get; }

    IFinishedWordRepository FinishedWordRepository { get; }
}