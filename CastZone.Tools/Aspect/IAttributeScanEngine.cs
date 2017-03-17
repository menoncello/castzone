using Castle.Core.Interceptor;
using System;
using System.Collections.Generic;

namespace CastZone.Tools.Aspect
{
    public interface IAttributeScanEngine
    {
        void Run(IInvocation invocation, Type type);
        IEnumerable<Type> AutoFindAttribute(string path = null);
    }
}
