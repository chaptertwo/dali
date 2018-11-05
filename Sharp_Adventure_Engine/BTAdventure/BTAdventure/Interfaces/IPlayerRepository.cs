using BTAdventure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTAdventure.Interfaces
{
    public interface IPlayerRepository
    {
        IEnumerable<Player> All();
        Player FindById(string id);
        Player Save(Player player);
        bool Delete(int id);
    }
}
