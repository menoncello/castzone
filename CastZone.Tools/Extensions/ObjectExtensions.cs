using CastZone.Tools.Pipes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

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
        public static string ToJson<T>(this T @this, bool beaultify = false) =>
            JsonConvert.SerializeObject(@this, beaultify ? Formatting.Indented : Formatting.None);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T FromJson<T>(this string @this) =>
            JsonConvert.DeserializeObject<T>(@this);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] ToByteJson<T>(this T @this, Encoding encoding = null) =>        
            (encoding ?? Encoding.UTF8).GetBytes(@this.ToJson());

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T FromByte<T>(this byte[] @this, Encoding encoding = null) =>
            (encoding ?? Encoding.UTF8).GetString(@this).FromJson<T>();

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Lock<T>(this T @this, Action method, bool firstTime = true)
        {
            lock (@this)
            {
                if (firstTime)
                    method();
            }
        }
    }
}
