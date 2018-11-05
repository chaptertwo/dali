using BTAdventure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTAdventure.Interfaces
{
    public interface IPlayerCharacterRepository
    {
        IEnumerable<PlayerCharacter> All();
        PlayerCharacter FindById(int id);
        PlayerCharacter Save(PlayerCharacter character);
        IEnumerable<PlayerCharacter> AllLoggedIn(string UserID);
        bool Delete(int id);
        PlayerCharacter AddNewPlayerCharacter(PlayerCharacter character);
    }
}
