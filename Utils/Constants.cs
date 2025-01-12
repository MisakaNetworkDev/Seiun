using Microsoft.IdentityModel.Tokens;

namespace Seiun.Utils;

public static class Constants
{
    public static class Email
    {
        public const int MaxLength = 256;
    }

    public static class User
    {
        public const int MaxDescriptionLength = 256;
        public const int MaxUserNameLength = 32;
        public const int MaxNickNameLength = 32;
        public const int MaxPhoneNumberLength = 15;
        public const int MaxAvatarSize = 8 * 1024 * 1024; // 8MB
        public const int AvatarMaxWidth = 1024;
        public const int AvatarMaxHeight = 1024;
        public const int AvatarStorageWidth = 256;
        public const int AvatarStorageHeight = 256;
        public static readonly string[] AllowedAvatarExtensions = [".jpg", ".jpeg", ".png", "webp"];
    }

    public static class Token
    {
        public const int TokenExpirationTime = 7 * 24; // 7 days
        public const string SignAlgorithm = SecurityAlgorithms.HmacSha256;
    }

    public static class BucketNames
    {
        public const string Avatar = "avatars";
    }
}