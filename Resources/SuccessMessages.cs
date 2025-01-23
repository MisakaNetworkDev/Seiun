namespace Seiun.Resources;

public static class SuccessMessages
{
    public static class Controller
    {
        public static class User
        {
            public const string RegisterSuccess = "controller.user.register.register_success";
            public const string LoginSuccess = "controller.user.login.login_success";
            public const string ProfileUpdateSuccess = "controller.user.profile.update_success";
            public const string AvatarUpdateSuccess = "controller.user.profile.avatar_update_success";
            public const string GetProfileSuccess = "controller.user.profile.get_success";
        }
        public static class Comment
        {
            public const string CreateSuccess = "controller.comment.create.create_success";
            public const string DetailSuccess = "controller.comment.detail.detail_success";
            public const string DeleteSuccess = "controller.comment.delete.delete_success";
            public const string GetListSuccess = "controller.comment.get.list_success";
            public const string GetLikeSuccess = "controller.comment.get.like_success";
            public const string CancelLikeSuccess = "controller.comment.cancel.cancel_like_success";
            public const string GetDislikeSuccess = "controller.comment.get.dis_like_success";
            public const string CancelDislikeSuccess = "controller.comment.cancel.cancel_dislike_success";
        }
        public static class Reply
        {
            public const string CreateSuccess = "controller.reply.create.create_success";
            public const string DeleteSuccess = "controller.reply.delete.delete_success";
            public const string GetListSuccess = "controller.reply.get.list_success";
            public const string DetailSuccess = "controller.reply.detail.detail_success";
        }
    }
}