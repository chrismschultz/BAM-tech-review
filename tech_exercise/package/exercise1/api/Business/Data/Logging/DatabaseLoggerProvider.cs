namespace StargateAPI.Business.Data.Logging
{
    public class DatabaseLoggerProvider : ILoggerProvider
    {
        private readonly string _connectionString;

        public DatabaseLoggerProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new DatabaseLogger(_connectionString, categoryName);
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
