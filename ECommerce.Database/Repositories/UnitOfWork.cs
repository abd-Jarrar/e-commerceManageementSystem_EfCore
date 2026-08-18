using ECommerce.Business.Interfaces;
using ECommerce.Database.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Database.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context=context;
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
