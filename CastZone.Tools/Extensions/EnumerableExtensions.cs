using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class EnumerableExtensions
    {

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> LogAll<T>(this IEnumerable<T> @this)
        {
            return @this.Tee(x => x.Log());
        }
    }
}
