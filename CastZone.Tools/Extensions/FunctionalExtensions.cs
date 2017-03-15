using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace System
{
    public static class FunctionalExtensions
    {
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Tee<T>(this T @this, Action<T> action)
        {
            action(@this);
            return @this;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult Map<TSource, TResult>
            (this TSource @this, Func<TSource, TResult> fn) =>
            fn(@this);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T When<T>
            (this T @this, Func<bool> predicate, Func<T, T> fn) =>
              predicate()
                ? fn(@this)
                : @this;
    }
}
