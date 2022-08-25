namespace HttpClientHelper.Library
{
    public static class ApplicationLogging
    {
        public static ILoggerFactory? LoggerFactory { get; set; }// = new LoggerFactory();

        internal static ILogger<T>? CreateLogger<T>() => LoggerFactory?.CreateLogger<T>();

        internal static ILogger? CreateLogger(string categoryName) => LoggerFactory?.CreateLogger(categoryName);
    }
}
