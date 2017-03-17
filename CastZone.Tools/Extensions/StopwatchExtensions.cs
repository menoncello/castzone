using CastZone.Tools.Pipes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastZone.Tools.Extensions
{
    public static class StopwatchExtensions
    {
        public static void TrackTime(this Stopwatch stopwatch, Action before, Action method, Action<TimeSpan, Exception> finish)
        {
            before?.Invoke();

            stopwatch.Start();

            Exception exception = null;

            method();

            stopwatch.Stop();

            finish(stopwatch.Elapsed, exception);
        }

        public static void TrackTime(this Stopwatch stopwatch, Action method, Action<TimeSpan, Exception> finish) =>
            stopwatch.TrackTime(null, method, finish);
    }
}
