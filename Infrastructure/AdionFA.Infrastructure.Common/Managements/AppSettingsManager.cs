using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace AdionFA.Infrastructure.Common.Managements
{
    public class AppSettingsManager
    {
        private static readonly Lazy<AppSettingsManager> instance = new Lazy<AppSettingsManager>(() => new AppSettingsManager());
        private static IConfiguration _configuration;

        private AppSettingsManager()
        {
            _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
        }

        public static AppSettingsManager Instance => instance.Value;

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
        public string MetaTraderHost { get; set; }
        public int MetaTraderPort { get; set; }
    }
}
