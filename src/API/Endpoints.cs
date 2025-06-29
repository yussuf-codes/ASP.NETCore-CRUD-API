namespace API;

public static class Endpoints
{
    private const string EndpointsBase = "api";

    public static class Auth
    {
        public const string Register = $"{EndpointsBase}/register";
        public const string Login = $"{EndpointsBase}/login";
    }

    public static class Notes
    {
        private const string NotesBase = $"{EndpointsBase}/notes";

        public const string Create = NotesBase;
        public const string Delete = $"{NotesBase}/{{id:Guid}}";
        public const string Get = NotesBase;
        public const string GetById = $"{NotesBase}/{{id:Guid}}";
        public const string Update = $"{NotesBase}/{{id:Guid}}";
    }
}
