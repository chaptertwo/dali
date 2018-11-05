using Myth.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myth.Domain.Interfaces
{
    public interface IFootprintRepository
    {
        IEnumerable<Footprint> All();
        Footprint FindById(int id);
        bool Delete(int id);
        Footprint Save(Footprint footprint);
        IEnumerable<Footprint> GetFootprintsByCreature(Creature creature);
        Footprint Generate(Footprint f);
    }
}
