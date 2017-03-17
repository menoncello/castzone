using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastZone.Tools.Aspect
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class LockAttribute : Attribute
    {
    }
}
