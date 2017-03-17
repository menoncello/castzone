using Castle.DynamicProxy;
using System;

namespace CastZone.Tools.Aspect
{
    public class DynamicProxyHelper
    {
        public static object CreateProxyFor(Type interfaceType, object concreteObject)
        {
            var dynamicProxy = new ProxyGenerator();

            var result = dynamicProxy
                .CreateInterfaceProxyWithTargetInterface(
                    interfaceType,
                    concreteObject,
                    new[] { new AspectInterceptor() });

            return result;
        }
    }
}
