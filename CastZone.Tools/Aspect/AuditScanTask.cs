using System;

namespace CastZone.Tools.Aspect
{
    public class AuditScanTask : ScanTask<AuditAttribute>
    {
        protected override void Execute(Action method, string methodName, bool isFirstCall)
        {
            if (isFirstCall)
                method();
        }
    }
}
