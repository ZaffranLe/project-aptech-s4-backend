using System;
using NLog;

namespace ElectricShop.Common
{
    public class ConsoleLog
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static void WriteLine(object strConsole, params object[] arr)
        {
            if (strConsole == null) return;
            if (arr != null && arr.Length > 0)
            {
                var str = string.Format(strConsole.ToString(), arr);
                logger.Debug(str);
                return;
            }
            //Ghi console len man hinh
            logger.Debug(strConsole);
        }

        public static void Write(object strConsole, params object[] arr)
        {
            WriteLine(strConsole, arr);
        }
    }

    public class TestLog
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static bool DevelopmentLog { get; set; }

        public static void WriteLine(object strConsole, params object[] arr)
        {
            if (!DevelopmentLog) return;
            if (strConsole == null) return;
            if (arr != null && arr.Length > 0)
            {
                var str = string.Format(strConsole.ToString(), arr);
                logger.Trace(str);
                return;
            }
            //Ghi console len man hinh
            logger.Trace(strConsole);
        }

        public static void Write(object strConsole, params object[] arr)
        {
            WriteLine(strConsole, arr);
        }
    }

    public class LogSynToCore
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private const string FORMAT = "{0},{1},{2},{3},{4},{5},{6}";

        public static void Log(DateTime runDate, string orderId, bool isEod, string lastState, string lastTriger, string currentState, string msg)
        {
            if (string.IsNullOrWhiteSpace(orderId)) return;
            msg = msg.Replace(',', '_');
            logger.Trace(FORMAT, runDate.ToString("yyyyMMdd"), orderId, (isEod ? "EOD" : "OTHER"), lastState, lastTriger, currentState, msg);
        }
    }

    public class LogCheck
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private const string FORMAT = "{0},{1},{2},{3},{4}";

        public static void Log(string orderId, string lastState, string lastTriger, string currentState, string msg)
        {
            if (string.IsNullOrWhiteSpace(orderId)) return;
            msg = msg.Replace(',', '_');
            logger.Trace(FORMAT, orderId, lastState, lastTriger, currentState, msg);
        }
    }

    public class LogSynMurex
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static void Log(string msg)
        {
            if (string.IsNullOrWhiteSpace(msg)) return;
            msg = msg.Replace(',', '_');
            logger.Trace(msg);
        }
    }

    public class LogWebService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private const string FORMAT = "{0},{1},{2}";
        private const string FORMAT_FILE = "LogWebService-{0}.csv";

        public static string Log(DateTime dateRun, string functionAction, string paramInput, string message)
        {
            if (string.IsNullOrWhiteSpace(functionAction)) return "";
            message = message.Replace(',', '_');
            logger.Trace(FORMAT, functionAction, paramInput, message);
            return string.Format(FORMAT_FILE, dateRun.ToString("yyyyMMdd"));
        }
    }
    public class ReportLog
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static bool DevelopmentLog { get; set; }

        public static void WriteLine(object strConsole, params object[] arr)
        {
            if (!DevelopmentLog) return;
            if (strConsole == null) return;
            if (arr != null && arr.Length > 0)
            {
                var str = string.Format(strConsole.ToString(), arr);
                logger.Trace(str);
                return;
            }
            //Ghi console len man hinh
            logger.Trace(strConsole);
        }

        public static void Write(object strConsole, params object[] arr)
        {
            WriteLine(strConsole, arr);
        }
    }
}
