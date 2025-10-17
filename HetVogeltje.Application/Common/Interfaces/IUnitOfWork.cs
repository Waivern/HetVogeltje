using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HetVogeltje.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {

        IVillaRepository Villa { get; }
        IVillaNumberRepository VillaNumber { get; }
        IVoorzieningRepository Voorziening { get; }
        void Save();
    }
}
