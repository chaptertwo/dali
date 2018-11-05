using BTAdventure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTAdventure.Interfaces
{
    public interface IOutcomeRepository
    {
        IEnumerable<Outcome> All();
        Outcome FindById(int id);
        Outcome Save(Outcome outcome);
        bool Delete(int id);
        Outcome FindByEventChoice(int id);
        IEnumerable<Outcome> FindOutcomeByEventChoiceId(int id);
        Outcome CheckOutComeStatus(bool posOrNeg, int eventChoiceId);
    }
}
