using BTAdventure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTAdventure.Interfaces
{
    public interface IGameRepository
    {
        IEnumerable<Game> All();
        Game FindById(int id);
        Game Save(Game game);
        bool Delete(int id);
    }
}
