using BTAdventure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTAdventure.Interfaces
{
    public interface ISceneRepository
    {
        IEnumerable<Scene> All();
        Scene FindById(int id);
        Scene FindById(int? id);
        IEnumerable<Scene> FindByGameId(int id);
        Scene Save(Scene scene);
        bool Delete(int id);
        Scene FindSceneByCharacterId(int id);
    }
}
