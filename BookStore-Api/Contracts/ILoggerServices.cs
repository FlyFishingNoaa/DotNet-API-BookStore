using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore_Api.Contracts
{
    public interface ILoggerServices
    {

        void LogInfo(string message);
        void LogWarn(string message);
        void LogDebug(string message);
        void LogIError(string message);


    }
}
