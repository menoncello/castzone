using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastZone.Tools.Logging
{
    public class NLogger : ILogger
    {
        Logger logger = LogManager.GetLogger("MyClassName");

        public void Debug(string msg, params object[] args)
        {
            Log(LoggingLevel.Debug, msg, args);
        }

        public void Error(Exception e)
        {
            Log(LoggingLevel.Error, "Error ocorred: {0}", e.ToJson(true));
        }

        public void Error(Exception e, string msg, params object[] args)
        {
            Log(LoggingLevel.Error, e, msg, args);
        }

        public void Fatal(string msg, params object[] args)
        {
            Log(LoggingLevel.Fatal, msg, args);
        }

        public void Info(string msg, params object[] args)
        {
            Log(LoggingLevel.Info, msg, args);
        }

        public void Log(LoggingLevel level, string msg, params object[] args)
        {
            logger.Log(level.ToNLog(), msg, args);
        }

        public void Log(LoggingLevel level, Exception e, string msg = null, params object[] args)
        {
            logger.Log(level.ToNLog(), e, msg, args);
        }

        public void Trace(string msg, params object[] args)
        {
            Log(LoggingLevel.Trace, msg, args);
        }

        public void Warning(string msg, params object[] args)
        {
            Log(LoggingLevel.Warning, msg, args);
        }
    }
}
