using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace System
{
    public static class EntityFrameworkExtensions
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<DbContext> AddAndSaveChangesAsync<T>(this DbContext @this, T entity)
            where T: class
        {
            Logger.Trace("adding and saving changes for: {0}", entity);
            @this.Set<T>().Add(entity);
            await @this.SaveChangesAsync();
            Logger.Trace("added and saved changes for: {0}", entity);
            return @this;
        }
    }
}
