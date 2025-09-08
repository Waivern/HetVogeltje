using HetVogeltje.Application.Common.Interfaces;
using HetVogeltje.Domein.Entities;
using HetVogeltje.Infrastructuur.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HetVogeltje.Infrastructuur.Repository
{
    public class VillaRepository : IVillaRepository
    {
        private readonly ApplicationDBContext _context;

        public VillaRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public void Add(Villa entity)
        {
           _context.Villas.Add(entity);
        }

        public IEnumerable<Villa> Get(Expression<Func<Villa, bool>> filter, string? includeProperties = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Villa> GetAll(Expression<Func<Villa, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<Villa> query = _context.Set<Villa>();
            if (filter != null)
            {
                query = query.Where(filter);
        }

        public void Remove(Villa entity)
        {
            _context.Villas.Remove(entity);

        }

        public void Save()
        {
            _context.Villas.Save();

        public void Update(Villa entity)
        {
            _context.Villas.Update(entity);
        }
    }
}
