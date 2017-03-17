using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastZone.Tools.Pipes
{
    public static class Factory
    {
        private static Container _container;

        public static Container Container =>
            (_container ?? (_container = new Container()));
    }
}
