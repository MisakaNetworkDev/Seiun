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
        
        public const string OverPhoneNumberLength = "error.validation.over_phone_number_length";
        public const string OverNickNameLength = "error.validation.over_nickname_length";
        public const string OverUserNameLength = "error.validation.over_username_length";
        public const string OverDescriptionLength = "error.validation.over_description_length";
        public const string OverEmailLength = "error.validation.over_email_length";
        
        public const string AtLeastOnePropertyRequired = "error.validation.at_least_one_property_is_required";
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
    }
}