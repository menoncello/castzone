using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CastZone.Tools.Pipes
{
    public static class Catcher
    {
        public static void TryCatch(Expression body, params CatchBlock[] handlers)
        {
            Expression.TryCatch(body, handlers);
        }
        public static void TryCatch(Action body, Action<Exception> exception)
        {
            TryCatch(body, exception);
        }
    }
}
