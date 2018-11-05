using Myth.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myth.Domain.Interfaces
{
    public interface IRegionRepository
    {
        IEnumerable<Region> All();
        Region FindById(int id);
        bool Delete(int id);
        Region Save(Region region);
    }
}
