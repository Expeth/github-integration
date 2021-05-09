namespace GithubIntegration.Host.Constants
{
    public static class ResponseConsts
    {
        public static readonly (int code, string msg) InternalServerError = (5000, "Internal server error.");
        public static readonly (int code, string msg) ExternalServerError = (5100, "External server error.");
    }
}