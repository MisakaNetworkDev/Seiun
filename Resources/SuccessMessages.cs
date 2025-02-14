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
        public static class Article
        {
            public const string CreateSuccess = "controller.article.create.create_success";
            public const string DeleteSuccess = "controller.article.delete.delete_success";
            public const string PinSuccess = "controller.article.pin.pin_success";
            public const string PinCancelSuccess = "controller.article.pin.pin_cancel_success";
            public const string GetArticleListSuccess = "controller.article.GetArticleList.get_articlelist_success";
            public const string GetArticleDetailSuccess = "controller.article.GetArticleDetail.get_articledetail_success";
            public const string LikeSuccess = "controller.article.like.like_success";
            public const string ArticleImgsUploadSuccess = "controller.article.articleimgs.upload_success";
            public const string GetArticleImgNameListSuccess = "controller.article.articleimgs.get_articleimgnamelist_success";
        }
    
        public static class PublicAnnouncement
        {
            public const string PublishSuccess = "controller.publicannouncement.publish.publish_success";
            public const string DeleteSuccess = "controller.publicannouncement.delete.delete_success";
            public const string GetPublicAnnouncementsSuccess = "controller.publicannouncement.get.get_publicannouncements_success";
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