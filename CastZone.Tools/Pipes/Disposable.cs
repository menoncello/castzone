using System;
using System.Threading.Tasks;

namespace CastZone.Tools.Pipes
{
    public static class Disposable
    {
        public static TResult Using<TDisposable, TResult>(
          Func<TDisposable> factory,
          Func<TDisposable, TResult> fn)
          where TDisposable : IDisposable
        {
            using (var disposable = factory())
            {
                return fn(disposable);
            }
        }
        public static void Using<TDisposable>(
          Func<TDisposable> factory,
          Action<TDisposable> fn)
          where TDisposable : IDisposable
        {
            using (var disposable = factory())
            {
                fn(disposable);
            }
        }

        public static async Task<TResult> UsingAsAsync<TDisposable, TResult>(
          Func<TDisposable> factory,
          Func<TDisposable, TResult> fn)
          where TDisposable : IDisposable
        {
            using (var disposable = factory())
            {
                return await Task<TResult>.Factory.StartNew(() => fn(disposable));
            }
        }
        public static async Task UsingAsAsync<TDisposable>(
          Func<TDisposable> factory,
          Action<TDisposable> fn)
          where TDisposable : IDisposable
        {
            using (var disposable = factory())
            {
                await Task.Factory.StartNew(() => fn(disposable));
            }
        }
    }
}