using TaskManagementApp.LogModels;

namespace TaskManagementApp.Logging
{
    public class CustomLogger : ICustomLogger
    {
        private readonly LogContext _context;

        public CustomLogger(LogContext context)
        {
            _context = context;
        }

        public void LogInformation(string description)
        {
            SaveLog(description, "Information");
        }

        public void LogWarning(string description)
        {
            SaveLog(description, "Warning");
        }

        public void LogError(string description)
        {
            SaveLog(description, "Error");
        }

        private void SaveLog(string description, string logLevel)
        {
            var log = new Log
            {
                Description = description,
                LogLevel = logLevel,
                LogTim = DateTime.Now
            };

            _context.Logs.Add(log);
            _context.SaveChanges();
        }
    }
}
