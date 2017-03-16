using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class EntityFrameworkExtensions
    {
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task AddAndSaveChangesAsync<T>(this DbContext @this, T entity)
            where T: class
        {
            @this.Set<T>().Add(entity);
            await @this.SaveChangesAsync();
        }
    }
}
