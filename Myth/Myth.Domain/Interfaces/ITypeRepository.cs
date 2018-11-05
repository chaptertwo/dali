using Myth.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myth.Domain.Interfaces
{
    public interface ITypeRepository
    {
        IEnumerable<CreatureType> All();
        CreatureType FindById(int id);
        bool Delete(int id);
        CreatureType Save(CreatureType creatureType);
        CreatureType FindByCreatureId(int id);
    }
}
