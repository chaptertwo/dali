using Myth.Domain.Services;
using Myth.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Myth.UI.Controllers
{
    public class GameController : Controller
    {
        private MythService mythService;

        public GameController(MythService mythService)
        {
            this.mythService = mythService;
        }
        // GET: Game
        public ActionResult Play()
        {
            GameVM vm = new GameVM();
            vm.Creatures = mythService.CombineCreatureAttributes();
            vm.Types = mythService.GetAllTypes();
            vm.Traits = mythService.GetAllTraits();
            foreach (var c in vm.Creatures)
            {
                c.Traits = mythService.GetTraitsByCreature(c.CreatureId);
                c.Type = mythService.GetTypesByCreatureId(c.CreatureId);
                c.Nest = mythService.FindNestByCreatureId(c.CreatureId);
                c.Footprints = mythService.GetAllFootprints().Where(f => f.CreatureId == c.CreatureId);
            }
            vm.Nests = mythService.GetAllNests();
            vm.Footprints = mythService.GetAllFootprints();
            return View(vm);
        }
    }
}