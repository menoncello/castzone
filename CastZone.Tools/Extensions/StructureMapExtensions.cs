using CastZone.Tools.Aspect;
using CastZone.Tools.Logging;
using StructureMap.Pipeline;

namespace StructureMap
{
    public static class StructureMapExtensions
    {
        public static SmartInstance<T, TPluginType> Proxy<T, TPluginType>(
            this SmartInstance<T, TPluginType> @this)
            where T: TPluginType
        {
            //return smartInstance;
            return @this
                .DecorateWith(x => (TPluginType) DynamicProxyHelper.CreateProxyFor(typeof(TPluginType), x));
        }

        public static void DefaultSetup(
            this ConfigurationExpression @this)
        {
            @this.For<ILogger>().Use<NLogger>();
            @this.For<IDirectoryTask>().Use<DirectoryTask>();
            @this.For<IAttributeScanEngine>().Use<AutoAttributeScanEngine>();
        }
    }
}
