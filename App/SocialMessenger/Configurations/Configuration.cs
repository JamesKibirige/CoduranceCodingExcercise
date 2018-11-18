using Microsoft.Extensions.Configuration;
using SocialMessenger.Enumerations;

namespace SocialMessenger.Configurations
{
    public abstract class Configuration
    {
        public static readonly Configuration Development = new DevelopmentConfiguration();

        public IConfigurationRoot ConfigurationRoot { get; set; }

        protected Configuration(IConfigurationRoot configurationRoot)
        {
            ConfigurationRoot = configurationRoot;
        }

        private class DevelopmentConfiguration : Configuration
        {
            public DevelopmentConfiguration()
                : base
                (
                    new ConfigurationBuilder()
                        .AddJsonFile(JsonFileName.AppSettings, false, true)
                        .Build()
                )
            {
            }
        }
    }
}