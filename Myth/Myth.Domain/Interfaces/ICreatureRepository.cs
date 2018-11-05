using Myth.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myth.Domain.Interfaces
{
    public interface ICreatureRepository
    {
        IEnumerable<Creature> All();
        Creature FindById(int id);
        bool Delete(int id);
        Creature Save(Creature creature);
        void UpdateCreatureNest(int creatureSelectedId, int nestId);
        void SaveFromMap(Creature creature);
    }
}
