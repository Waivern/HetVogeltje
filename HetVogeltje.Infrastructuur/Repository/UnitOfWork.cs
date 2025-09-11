using HetVogeltje.Application.Common.Interfaces;
using HetVogeltje.Infrastructuur.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HetVogeltje.Infrastructuur.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _context;
        public IVillaRepository Villa { get; private set; }
        public IVillaNumberRepository VillaNumber { get; private set; }
        public UnitOfWork(ApplicationDBContext db)
        {
            _context = db;
            Villa = new VillaRepository(_context);
            VillaNumber = new VillaNumberRepository(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
