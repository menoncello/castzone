using System;

namespace CastZone.Tools.Logging
{
    public interface ILogger
    {
        void Trace(string msg, params object[] args);
        void Debug(string msg, params object[] args);
        void Info(string msg, params object[] args);
        void Warning(string msg, params object[] args);
        void Error(Exception e);
        void Error(Exception e, string msg, params object[] args);
        void Fatal(string msg, params object[] args);

        void Log(LoggingLevel level, string msg, params object[] args);
        void Log(LoggingLevel level, Exception e, string msg = null, params object[] args);
    }
}