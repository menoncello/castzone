using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastZone.Tools.Pipes
{
    public static class Validator
    {
        public static Result<T, string> IsNotNull<T>(T value)
            where T : class =>
                value == null
                    ? Result<T, string>.FailWith("Value cannot be null")
                    : Result<T, string>.SucceedWith(value);


        public static Result<IEnumerable<T>, string> IsNotEmpty<T>(IEnumerable<T> value) =>
            !value.Any()
                ? Result<IEnumerable<T>, string>.FailWith("Value cannot be empty")
                : Result<IEnumerable<T>, string>.SucceedWith(value);

        public static Result<TValue, string> Is<TTest, TValue>(TValue value) =>
                value is TTest
                    ? Result<TValue, string>.SucceedWith(value)
                    : Result<TValue, string>.FailWith("Value is not of " + typeof(TTest).Name);

        public static Result<IEnumerable<TValue>, string> IsAll<TTest, TValue>(IEnumerable<TValue> value) =>
                value.Any(x => !(x is TTest))
                    ? Result<IEnumerable<TValue>, string>
                        .FailWith("At least one of the values is not of " + typeof(TTest).Name)
                    : Result<IEnumerable<TValue>, string>.SucceedWith(value);

        public static Result<string, string> HasValue(string value) =>
            string.IsNullOrEmpty(value)
                ? Result<string, string>.FailWith("Value cannot be empty")
                : Result<string, string>.SucceedWith(value);

        public static Func<string, Result<string, string>> MinLength(int minLength) =>
            value =>
                value.Length < minLength
                    ? Result<string, string>.FailWith($"Value must be at least {minLength} characters long")
                    : Result<string, string>.SucceedWith(value);
    }
}
