using BTAdventure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTAdventure.Interfaces
{
    public interface IEndingRepository
    {
        IEnumerable<Ending> All();
        Ending FindById(int? id);
        IEnumerable<Ending> FindEndingsByGameId(int id);
        Ending Save(Ending ending);
        bool Delete(int id);
    }
}
