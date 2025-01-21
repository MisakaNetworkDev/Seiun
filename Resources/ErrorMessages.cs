namespace Seiun.Resources;

public static class ErrorMessages
{
    public static class ValidationError
    {
        public const string PhoneRequired = "error.validation.phone_number_required";
        public const string PasswordRequired = "error.validation.password_required";
        
        public const string InvalidPhone = "error.validation.invalid_phone_number";
        public const string InvalidEmail = "error.validation.invalid_email";
        public const string InvalidPassword = "error.validation.invalid_password";
        public const string InvalidUserName = "error.validation.invalid_username";
        public const string InvalidLikeCount = "error.validation.invalid_like_count";
        public const string InvalidDisLikeCount = "error.validation.invalid_dislike_count";
        
        public const string OverPhoneNumberLength = "error.validation.over_phone_number_length";
        public const string OverNickNameLength = "error.validation.over_nickname_length";
        public const string OverUserNameLength = "error.validation.over_username_length";
        public const string OverDescriptionLength = "error.validation.over_description_length";
        public const string OverEmailLength = "error.validation.over_email_length";
        public const string OverContentLength = "error.validation.over_content_length";

        public const string AtLeastOnePropertyRequired = "error.validation.at_least_one_property_is_required";
        public const string ActionTypeRequired = "erroe.validation.action_type_required";
        public const string UserIdRequired = "error.validation.user_id_required";

        public const string CommentIdRequired = "error.validation.comment_id_required";

        public const string ContentRequired = "error.validation.Content_required";

        public const string PostIdRequired = "erroe.validation.post_id_required";
        
    }

    public static class Controller
    {
        public static class Any
        {
            public const string ParamValidFailed = $"error.controller.any.param_valid_failed";
            public const string FileNotUploaded = "error.controller.any.file_not_uploaded";
            public const string FileTooLarge = "error.controller.any.file_too_large";
            public const string FileFormatNotSupported = "error.controller.any.file_format_not_supported";
            public const string ImageSizeTooLarge = "error.controller.any.image_size_too_large";
            public const string UnknownFileProcessingError = "error.controller.any.unknown_file_processing_error";
            public const string InvalidJwtToken = "error.controller.any.invalid_jwt_token";
        }
        public static class User
        {
            public const string UserNotFound = "error.controller.user.not_found";
            public const string UserLoginFailed = "error.controller.user.login_failed";
            public const string ProfileUpdateFailed = "error.controller.user.profile.update_failed";
            public const string PhoneNumberDuplicated = "error.controller.user.register.phone_number_already_exists";
            public const string RegisterFailed = "error.controller.user.register.register_failed";
        }
        public static class Comment
        {
            
            public const string CreateFailed = "error.controller.comment.create_failed";
            public const string CommentNotFound = "error.controller.comment.not_found";
            public const string CommentIdRequired = "error.controller.comment.commentid_required"; 
            public const string GetListFailed = "error.controller.comment.get_list_failed"; 
            public const string AlreadyLiked = "error.controller.comment.already_liked"; 
            public const string GetLikeFailed = "error.controller.comment.like_failed"; 
            public const string AlreadyCancelLiked = "error.controller.comment.already_cancel_liked"; 
            public const string CancelLikeFailed = "error.controller.comment.cancel_like_failed"; 
            public const string GetDislikeFailed = "error.controller.comment.get_dislike_failed";
            public const string AlreadyDisliked = "error.controller.comment.already_dislike";
            public const string AlreadyCancelDisliked = "error.controller.comment.already_cancel_dislike";
            public const string CancelDislikeFailed = "error.controller.comment.cancel_dislike_failed";
        }
        public static class Reply
        {
            public const string CreateFailed = "error.controller.reply.create_failed";
            public const string ReplyIdRequired = "error.controller.reply.replyid_required";
            public const string ReplyNotFound = "error.controller.reply.not_found";
            public const string NoPermission = "error.controller.reply.no_permission";
            public const string DeleteFailed = "error.controller.reply.delete_failed";
            public const string CommentNotFound = "error.controller.comment.not_found";
            public const string ParentReplyNotFound = "error.controller.reply.parent_reply_not_found";
        }
    }
}