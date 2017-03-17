using Castle.Core.Interceptor;
using CastZone.Tools.Logging;
using CastZone.Tools.Pipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CastZone.Tools.Aspect
{
    public class AutoAttributeScanEngine : IAttributeScanEngine
    {
        private readonly IDirectoryTask _directoryTask;
        private readonly ILogger _logger;
        private bool _firstcall = true;

        public AutoAttributeScanEngine() : this(
            Factory.Container.GetInstance<IDirectoryTask>(),
            Factory.Container.GetInstance<ILogger>())
        {
        }

        public AutoAttributeScanEngine(IDirectoryTask directoryTask, ILogger logger)
        {
            _directoryTask = directoryTask;
            _logger = logger;
        }

        public void Run(IInvocation invocation, Type type)
        {
            ((IAttributeScanTask)Activator.CreateInstance(type, _logger))
                .Run(invocation, _firstcall);
            _firstcall = false;
        }

        public virtual IEnumerable<Type> AutoFindAttribute(string path = null) =>
            (path ?? _directoryTask.GetCurrentDirectory())
                .Map(x => _directoryTask.GetFiles(x, $"*.dll"))
                .Map(x => FindAllAssemblies(x));

        private static IEnumerable<Type> FindAllAssemblies(IEnumerable<string> paths) =>
            paths
                .Select(x => Assembly.LoadFrom(x))
                .Where(x => x != null)
                .SelectMany(x => x.GetTypes());

        private static IEnumerable<Type> FindAllTypes(IEnumerable<Type> types) =>
            types
                .Where(type => typeof(IAttributeScanTask).IsAssignableFrom(type)
                    && type.FullName != typeof(IAttributeScanTask).FullName);
    }
}
