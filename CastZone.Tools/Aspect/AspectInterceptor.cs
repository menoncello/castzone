using Castle.Core.Interceptor;
using CastZone.Tools.Logging;
using CastZone.Tools.Pipes;
using System;
using System.Linq;

namespace CastZone.Tools.Aspect
{
    public class AspectInterceptor : IInterceptor
    {
        private readonly ILogger _logger;
        private readonly IAttributeScanEngine _attributeEngine;
        
        public AspectInterceptor() : this(
            Factory.Container.GetInstance<ILogger>(),
            Factory.Container.GetInstance<IAttributeScanEngine>())
        {
        }

        public AspectInterceptor(ILogger logger, IAttributeScanEngine attributeEngine)
        {
            _logger = logger;
            _attributeEngine = attributeEngine;
        }

        public void Intercept(IInvocation invocation) =>
            _attributeEngine.AutoFindAttribute()
                .Select(x => new
                {
                    Name = GetAttributeName(GetScanTaskName(x.FullName)),
                    Attribute = x
                })
                .Where(x => string.IsNullOrEmpty(x.Name))
                .Select(x => new
                {
                    Type = Type.GetType(x.Name),
                    Name = x.Name,
                    Attribute = x.Attribute
                })
                .Where(x => x != null)
                .ToList()
                .ForEach(x => _attributeEngine.Run(invocation, x.Attribute));
                //.ForEach(x => Catcher.TryCatch(
                //    () => _attributeEngine.Run(invocation, x.Attribute), 
                //    _logger.Error));
        

        private static string GetScanTaskName(string fullName) =>
            fullName
                .Substring(fullName.LastIndexOf(".") + 1, fullName.Length - fullName.LastIndexOf(".") - 1)
                .Map(x => x.Contains("ScanTask") ? x.Substring(0, x.IndexOf("ScanTask")) : x);

        private static string GetAttributeName(string scanTaskName) =>
            $"{scanTaskName}Attribute";
    }
}
