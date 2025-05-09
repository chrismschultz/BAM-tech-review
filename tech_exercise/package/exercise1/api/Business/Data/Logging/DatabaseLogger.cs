
using Microsoft.EntityFrameworkCore;

namespace StargateAPI.Business.Data.Logging
{
    public class DatabaseLogger : ILogger
    {
        private readonly string _connectionString;
        private readonly string _categoryName;

        public DatabaseLogger(string connectionString, string categoryName)
        {
            _connectionString = connectionString;
            _categoryName = categoryName;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= LogLevel.Information;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var message = formatter(state, exception);
            var logEntry = new LogEntry
            {
                Timestamp = DateTime.Now,
                Exception = exception?.ToString(),
                Message = message
            };

            using var context = new StargateContext(new DbContextOptionsBuilder<StargateContext>().UseSqlite(_connectionString).Options);

            context.LogEntries.Add(logEntry);
            context.SaveChanges();
        }
    }
}
