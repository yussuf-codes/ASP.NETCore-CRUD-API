namespace API;

public static class Endpoints
{
    private const string APIBase = "api";

    public static class Notes
    {
        private const string Base = $"{APIBase}/notes";

        public const string Create = Base;
        public const string Delete = $"{Base}/{{id:Guid}}";
        public const string Get = Base;
        public const string GetById = $"{Base}/{{id:Guid}}";
        public const string Update = $"{Base}/{{id:Guid}}";
    }
}
