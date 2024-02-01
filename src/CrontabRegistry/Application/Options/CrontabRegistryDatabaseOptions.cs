namespace CrontabRegistry.Application.Options
{
    public class CrontabRegistryDatabaseOptions
    {
        public string ConnectionString { set; get; } = string.Empty;
        public string DatabaseName { set; get; } = string.Empty;
        public string SummariesCollectionName { set; get; } = string.Empty;
    }
}
