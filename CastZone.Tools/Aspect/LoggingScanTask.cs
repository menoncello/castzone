using CastZone.Tools.Extensions;
using System;
using System.Diagnostics;

namespace CastZone.Tools.Aspect
{
    public class LoggingScanTask : ScanTask<LoggingAttribute>
    {
        protected override void Execute(Action method, string methodName, bool isFirstCall) =>
            new Stopwatch()
                .TrackTime(
                    () => Logger.Info("Starting method {0}", methodName),
                    method, 
                    (time, e) => Logger.Info("{0} finished in: {1:N3}s", methodName, time.TotalSeconds)
                );
    }
}
