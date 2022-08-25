namespace Automated.API.Common.Base;

public class ConfigurationBase<TSettings> where TSettings : class, IAppSettings
{
    private readonly Lazy<TSettings> _settings = new(GetSettings);

    private static TSettings GetSettings()
    {
        var configuration = new ConfigurationBuilder()
                               .AddJsonFile("appSettings.json")
                               .Build();

        string appSettingName = nameof(AppSettings);
        return configuration.GetSection(appSettingName).Get<TSettings>();
    }

    public TSettings AppSettings => _settings.Value;
}
