namespace TaskManagementApp.Logging
{
    public interface ICustomLogger
    {
        void LogInformation(string description);
        void LogWarning(string description);
        void LogError(string description);
    }
}
