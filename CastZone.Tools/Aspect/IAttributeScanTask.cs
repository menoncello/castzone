using Castle.Core.Interceptor;
using System.Reflection;

namespace CastZone.Tools.Aspect
{
    public interface IAttributeScanTask
    {
        void Run(IInvocation invocation, bool isFirstCall);

        bool AttributeRecognize(ICustomAttributeProvider methodInfo);
    }
}
