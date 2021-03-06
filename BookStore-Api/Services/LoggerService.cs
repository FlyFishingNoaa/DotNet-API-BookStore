using BookStore_Api.Contracts;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore_Api.Services
{
    public class LoggerService : ILoggerServices

    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

       // private readonly ILogger logger = LogManager.GetCurrentClassLogger();
        public void LogDebug(string message)
        {
            logger.Debug(message);
        }

        public void LogIError(string message)
        {
            logger.Error(message);
        }

        public void LogInfo(string message)
        {
            logger.Info(message);
        }

        public void LogWarn(string message)
        {
            logger.Warn(message);
        }
    }
}
