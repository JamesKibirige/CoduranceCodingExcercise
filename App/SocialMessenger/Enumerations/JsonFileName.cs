namespace SocialMessenger.Enumerations
{
    public abstract class JsonFileName : Enumeration
    {
        public static readonly JsonFileName AppSettings = new AppSettingsFileName();

        private JsonFileName(string name, int value)
            : base(name, value)
        {
        }

        private class AppSettingsFileName : JsonFileName
        {
            public AppSettingsFileName()
                : base("appsettings.json", 1)
            {
            }
        }
    }
}