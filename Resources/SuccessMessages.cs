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
    }
}