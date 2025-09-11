using HetVogeltje.Application.Common.Interfaces;
using HetVogeltje.Domein.Entities;
using HetVogeltje.Infrastructuur.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HetVogeltje.Infrastructuur.Repository
{
    public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
    {
        private readonly ApplicationDBContext _context;

        public VillaNumberRepository(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }  
    
        public void Update(VillaNumber entity)
        {
            //Villas hoeft niet, omdat we al in de context zitten.
            _context.Update(entity);
        }
    }
}
