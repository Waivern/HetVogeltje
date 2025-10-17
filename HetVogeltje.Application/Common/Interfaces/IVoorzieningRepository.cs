using HetVogeltje.Domein.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HetVogeltje.Application.Common.Interfaces
{
    public interface IVoorzieningRepository : IRepository<Voorziening>
    {
        void Update(Voorziening entity);
        
    }
}
