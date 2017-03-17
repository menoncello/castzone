using CastZone.Tools.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CastZone.Tools.Logging
{
    public static class LoggingExtensions
    {
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LogLevel ToNLog(this LoggingLevel @this)
        {
            switch (@this)
            {
                case LoggingLevel.Trace:
                    return LogLevel.Trace;
                case LoggingLevel.Debug:
                    return LogLevel.Debug;
                case LoggingLevel.Info:
                    return LogLevel.Info;
                case LoggingLevel.Warning:
                    return LogLevel.Warn;
                case LoggingLevel.Error:
                    return LogLevel.Error;
                case LoggingLevel.Fatal:
                    return LogLevel.Fatal;
                default:
                    return null;
            }
        }
    }
}
