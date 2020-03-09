namespace Kaysho.NET.Core.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public static class Blogs
        {
            public const string GetAll = Base + "/blogs";

            public const string Update = Base + "/blogs/{id}";

            public const string Delete = Base + "/blogs/{id}";

            public const string Get = Base + "/blogs/{id}";
            public const string Put = Base + "/blogs/{id}";

            public const string Create = Base + "/blogs";
        }

        public static class Comments
        {
            public const string GetAll = Base + "/comments";

            public const string Get = Base + "/comments/{id}";

            public const string Create = Base + "/comments";
            public const string Put = Base + "/comments/{id}";

            public const string Delete = Base + "/comments/{id}";
            public const string GetComment = Base + "/comments/{blogId}/{id}";
        }

        public static class Authentication
        {
            public const string Login = Base + "/authentication/login";

            public const string Register = Base + "/authentication/register";

            public const string Refresh = Base + "/authentication/refresh";

            public const string FacebookAuth = Base + "/authentication/auth/fb";
        }
    }
}
