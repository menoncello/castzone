using CastZone.Tools.Pipes;
using System;
using System.Reflection;
using Castle.Core.Interceptor;
using CastZone.Tools.Logging;
using System.Linq.Expressions;

namespace CastZone.Tools.Aspect
{
    public abstract class ScanTask<T> : IAttributeScanTask
        where T: Attribute
    {
        protected ILogger Logger { get; }

        public ScanTask()
            : this(Factory.Container.GetInstance<ILogger>())
        {
        }

        public ScanTask(ILogger logger)
        {
            Logger = logger;
        }

        public bool AttributeRecognize(ICustomAttributeProvider methodInfo) =>
            methodInfo.GetCustomAttributes(typeof(T), true)
                .Map(x => x != null && x is T[]);

        public void Run(IInvocation invocation, bool isFirstCall)
        {
            if (AttributeRecognize(invocation.Method) && isFirstCall)
            {
                Catcher.TryCatch(
                    () => Execute(invocation.Proceed, invocation.Method.Name, isFirstCall),
                    Logger.Error);
            }
        }

        protected abstract void Execute(Action method, string methodName, bool isFirstCall);
    }
}
