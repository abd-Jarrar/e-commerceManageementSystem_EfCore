using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_CommerceManagementSystemEfCore.Interfaces;

namespace E_CommerceManagementSystemEfCore.Interceptors
{
    public class SoftDeleteInterceptor:SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            if (eventData.Context is null)
                return result;
            foreach(var entry in eventData.Context.ChangeTracker.Entries())
            {
                if (entry is null || entry.State != EntityState.Deleted || entry.Entity is not ISoftDeleable entity)
                    continue;
                entry.State= EntityState.Modified;
                entity.Delete();
            }
            return result;
        }
    }
}
