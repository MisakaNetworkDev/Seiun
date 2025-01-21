using Seiun.Repositories;

namespace Seiun.Services;

public interface IRepositoryService
{
    IUserRepository UserRepository { get; }
    ICommentRepository CommentRepository { get; }
    ICommentLikeRepository CommentLikeRepository { get; }
    IReplyRepository ReplyRepository { get; }
}