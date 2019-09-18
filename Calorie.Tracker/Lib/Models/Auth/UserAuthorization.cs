namespace Lib.Models.Auth
{
    public static class UserAuthorization
    {
        public const string POLICY_ADMIN_ONLY = "ADMIN_ONLY";
        public const string ADMIN = "ADMIN";
        public const string USER = "USER";

        public static readonly string[] AllRoles =
        {
            ADMIN,
            USER
        };
    }
}
