using Microsoft.AspNetCore.Http;

namespace ERP.Utility.Helpers
{
    public static class SessionHelper
    {
        private const string UserIdKey = "LoggedInUserId";

        public static void SetLoggedInUserId(HttpContext httpContext, int userId)
        {
            byte[] userIdBytes = BitConverter.GetBytes(userId);
            httpContext.Session.Set(UserIdKey, userIdBytes);
        }

        public static int? GetLoggedInUserId(HttpContext httpContext)
        {
            if (httpContext.Session.TryGetValue(UserIdKey, out byte[] userIdBytes))
            {
                return BitConverter.ToInt32(userIdBytes, 0);
            }
            return null;
        }

        public static void ClearSession(HttpContext httpContext)
        {
            httpContext.Session.Remove(UserIdKey);
        }
    }
}