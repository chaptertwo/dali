using BTAdventure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTAdventure.Interfaces
{
    public interface IEventChoiceRepository
    {
        IEnumerable<EventChoice> All();
        EventChoice FindById(int? id);
        EventChoice Save(EventChoice level);
        bool Delete(int id);
        IEnumerable<EventChoice> FindBySceneId(int? sceneRoute);
        void UpdateEndingIdNeg(int eventChoiceId);
        void UpdateEndingIdPos(int eventChoiceId);
    }
}
