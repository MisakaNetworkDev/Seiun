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
<<<<<<< HEAD

    IUserCheckInRepository UserCheckInRepository { get; }
    IAIArticleRepository AIArticleRepository { get; }
=======
    IAIArticleRepository AIArticleRepository { get; }
    IUserCheckInRepository UserCheckInRepository { get; }
>>>>>>> b146b558ac009a67e541bad7b44028a2b3c9d153
}