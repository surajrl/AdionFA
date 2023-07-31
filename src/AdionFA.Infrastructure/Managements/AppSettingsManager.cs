using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace AdionFA.Infrastructure.Managements
{
    public class AppSettingsManager
    {
        private static readonly Lazy<AppSettingsManager> _instance = new(() => new AppSettingsManager());
        private static IConfiguration _configuration;

        private AppSettingsManager()
        {
            _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
        }

        public static AppSettingsManager Instance => _instance.Value;

        public T Get<T>()
        {
            return _configuration.GetSection(typeof(T).Name).Get<T>();
        }
    }

    public class AppSettings
    {
        public string DefaultConnection { get; set; }
        public string SecurityConnection { get; set; }
        public string Cultures { get; set; }
    }
}
