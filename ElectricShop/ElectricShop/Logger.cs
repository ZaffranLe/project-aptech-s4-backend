using NLog;

namespace ElectricShop
{
    public class Logger
    {
        private static ILogger _logger;

        public static void Write(string msg, bool isError = false)
        {
            if (_logger == null)
                _logger = LogManager.GetCurrentClassLogger();
            if (isError)
                _logger.Error(msg);
            else
                _logger.Info(msg);
        }
    }
}