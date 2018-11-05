using Myth.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myth.Domain.Interfaces
{
    public interface INestRepository
    {
        IEnumerable<Nest> All();
        Nest FindById(int id);
        bool Delete(int creatureId, int id);
        Nest Save(Nest nest);
        Nest FindByCreatureId(int id);
    }
}
