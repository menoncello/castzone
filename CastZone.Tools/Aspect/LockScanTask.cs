using Castle.Core.Interceptor;
using CastZone.Tools.Pipes;
using System;
using System.Reflection;

namespace CastZone.Tools.Aspect
{
    public class LockScanTask : ScanTask<LockAttribute>
    {
        protected override void Execute(Action method, string methodName, bool isFirstCall) => 
            method.Lock(method, isFirstCall);
    }
}
