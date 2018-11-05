using Myth.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myth.Domain.Interfaces
{
    public interface ITraitRepository
    {
        IEnumerable<Trait> All();
        Trait FindById(int id);
        bool Delete(int id);
        Trait Save(Trait trait);
        IEnumerable<Trait> FindManyByCreatureId(int id);
        //Trait SaveByCreatureId(Trait trait, int id);
        bool InsertByCreatureId(int traitId, int creatureId);
        bool UpdateByCreatureId(int traitId, int creatureId);
        bool DeleteAllTraitsPerCreature(int creatureId);
    }
}
