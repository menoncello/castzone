using System;

namespace CastZone.Tools.Logging
{
    [AttributeUsage(AttributeTargets.Method)]
    public class LoggingAttribute : Attribute
    {
        private readonly LoggingLevel _level;

        public LoggingAttribute(LoggingLevel level)
        {
            _level = level;
        }
        
        public enum LoggingLevel
        {
            Trace,
            Debug,
            Info,
            Warning,
            Error,
            Fatal
        }
    }
}