using CastZone.Tools.Pipes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;

namespace System
{
    public static class ObjectExtensions
    {
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsDefault<T>(this T @this) =>
           EqualityComparer<T>.Default.Equals(@this, default(T));

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNull<T>(this T @this) =>
            @this == null;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long GetMemorySize<T>(this T @this) =>
            Disposable.Using(
                () => new MemoryStream(),
                s =>
                {
                    new BinaryFormatter().Serialize(s, @this);
                    return s.Length;
                });

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Log<T>(this T @this)
        {
            Console.WriteLine(@this.ToJson());
            return @this;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToJson<T>(this T @this) =>
            JsonConvert.SerializeObject(@this);
    }
}
