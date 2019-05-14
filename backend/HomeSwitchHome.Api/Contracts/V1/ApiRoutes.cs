namespace HomeSwitchHome.API.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public static class Residences
        {
            public const string GetAll = Base + "/residences";
            public const string Get = Base + "/residences/{id}";
            public const string Create = Base + "/residences";
            public const string Update = Base + "/residences/{id}";
            public const string Delete = Base + "/residences/{id}";
        }

        public static class Users
        {
            public const string Register = Base + "/users/register";
            public const string GetAll = Base + "/users";
            public const string Get = Base + "/users/{id}";
            public const string Update = Base + "/users/{id}";
            public const string Delete = Base + "/users/{id}";
            public const string Profile = Base + "/users/profile";
        }

        public static class Auth
        {
            public const string Authenticate = Base + "/auth/authenticate";
            public const string Refresh = Base + "/auth/refresh";
        }
    }
}