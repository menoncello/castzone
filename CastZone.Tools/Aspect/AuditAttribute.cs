using System;

namespace CastZone.Tools.Aspect
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AuditAttribute : Attribute
    {
    }
}
