namespace Automated.API.Common.Base;

public abstract class UrlListBase
{
    private static readonly string ControllerSuffix = "Controller";

    private readonly Dictionary<string, string> _urlList = new();

    public virtual string ApiBaseUrl { get; set; } = string.Empty;

    public virtual string ApiBaseSuffix { get; set; } = string.Empty;

    public string GetUrl(string controllerName)
    {
        AddToDictionaryIfNotExistsYet(controllerName);
        return _urlList[controllerName];
    }

    private void AddToDictionaryIfNotExistsYet(string controllerName)
    {
        if (!_urlList.ContainsKey(controllerName))
        {
            var property = this.GetType().GetProperty(controllerName);
            _urlList[controllerName] = property?.GetValue(this) as string
                                    ?? controllerName.Replace(ControllerSuffix, string.Empty).ToLowerInvariant();
        }
    }
}
