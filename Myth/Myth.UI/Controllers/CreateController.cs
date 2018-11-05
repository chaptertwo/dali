using Myth.Domain.Models;
using Myth.Domain.Services;
using Myth.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Myth.UI.Controllers
{
    public class CreateController : Controller
    {
        private MythService mythService;

        public CreateController(MythService mythService)
        {
            this.mythService = mythService;
        }

        // GET: Create
        public ActionResult Index()
        {
            CreateCreatureVM vm = new CreateCreatureVM();
            vm.Creatures = mythService.GetAllCreatures();
            vm.Types = mythService.GetAllTypes();
            vm.Traits = mythService.GetAllTraits();
            foreach (var c in vm.Creatures)
            {
                c.Traits = mythService.GetTraitsByCreature(c.CreatureId);
                c.Type = mythService.GetTypesByCreatureId(c.CreatureId);
                c.Nest = mythService.FindNestByCreatureId(c.CreatureId);
            }
            return View(vm);
        }

        public ActionResult Creature()
        {
            CreateCreatureVM vm = new CreateCreatureVM();
            vm.Creatures = mythService.GetAllCreatures();
            vm.Traits = mythService.GetAllTraits();
            vm.Types = mythService.GetAllTypes();
            vm.Nests = mythService.GetAllNests();
            vm.TraitsSelect = (from trait in mythService.GetAllTraits()
                               select new TraitVM { Trait = trait, IsSelected = false }).ToList();
            return View(vm);
        }

        [HttpPost]
        public ActionResult Creature(CreateCreatureVM vm)
        {
            
            Creature creature = vm.Creature;
            creature.TypeId = vm.SelectTypeId;
            creature.CreatureIsRevealed = false;
            creature.NestId = vm.SelectNestId;
            
            var selectedIds = vm.TraitsSelect.Where(t => t.IsSelected).Select(t => t.Trait.TraitId);
            if (ModelState.IsValid)
            {
                var result = mythService.SaveCreature(creature);
                foreach (var t in vm.TraitsSelect)
                {
                    if (t.IsSelected)
                    {
                        mythService.SaveCreatureTrait(t.Trait.TraitId, vm.Creature.CreatureId);
                    }
                }
            }
            else
            {
                vm.Creatures = mythService.GetAllCreatures();
                vm.Traits = mythService.GetAllTraits();
                vm.Types = mythService.GetAllTypes();
                vm.Nests = mythService.GetAllNests();
                vm.TraitsSelect = (from trait in mythService.GetAllTraits()
                                   select new TraitVM { Trait = trait, IsSelected = false }).ToList();
                return View("Creature", vm);
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteCreature(int id)
        {
            mythService.DeleteFootprintsByCreature(id);
            mythService.DeleteCreatureById(id);
            return RedirectToAction("Index");
        }

        public ActionResult EditCreature(int id)
        {
            CreateCreatureVM vm = new CreateCreatureVM();
            vm.Creature = mythService.GetAllCreatures().FirstOrDefault(c => c.CreatureId == id);
            vm.Creature.Traits = mythService.GetTraitsByCreature(id);
            vm.Creatures = mythService.GetAllCreatures();
            vm.TraitsSelect = (from trait in mythService.GetAllTraits()
                               select new TraitVM { Trait = trait, IsSelected = false }).ToList();
            var selectedIds = vm.TraitsSelect.Where(t => t.IsSelected).Select(t => t.Trait.TraitId);
            
            vm.SelectTypeId = vm.Creature.TypeId;
            vm.Types = mythService.GetAllTypes();
            vm.Traits = mythService.GetAllTraits();
            vm.Nests = mythService.GetAllNests();
            return View(vm);
        }

        [HttpPost]
        public ActionResult EditCreature(CreateCreatureVM vm)
        {
            Creature creature = vm.Creature;
            creature.TypeId = vm.SelectTypeId;
            creature.CreatureIsRevealed = false;
            creature.NestId = vm.SelectNestId;
            creature.CreatureHasNest = true;

            if (ModelState.IsValid)
            {
                mythService.DeleteTraitsByCreatureId(creature.CreatureId);
                foreach (var t in vm.TraitsSelect)
                {
                    if (t.IsSelected)
                    {
                        mythService.SaveCreatureTrait(t.Trait.TraitId, vm.Creature.CreatureId);
                    }

                }
                var result = mythService.SaveCreature(creature);
                return RedirectToAction("Index");
            }
            else
            {
                vm.Creature = mythService.GetAllCreatures().FirstOrDefault(c => c.CreatureId == creature.CreatureId);
                vm.Creature.Traits = mythService.GetTraitsByCreature(creature.CreatureId);
                vm.Creatures = mythService.GetAllCreatures();
                vm.TraitsSelect = (from trait in mythService.GetAllTraits()
                                   select new TraitVM { Trait = trait, IsSelected = false }).ToList();
                var selectedIds = vm.TraitsSelect.Where(t => t.IsSelected).Select(t => t.Trait.TraitId);

                vm.SelectTypeId = vm.Creature.TypeId;
                vm.Types = mythService.GetAllTypes();
                vm.Traits = mythService.GetAllTraits();
                vm.Nests = mythService.GetAllNests();
                return View("EditCreature", vm);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Trait()
        {
            Trait trait = new Trait();
            return View(trait);
        }

        [HttpPost]
        public ActionResult Trait(Trait trait)
        {
            mythService.SaveTrait(trait);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        public ActionResult Nest()
        {
            NestCM nest = new NestCM();
            nest.Creatures = mythService.GetAllCreatures().Where(c => c.CreatureHasNest == false);
            var creatures = mythService.GetAllCreatures().Where(c => c.CreatureHasNest == false);

            
            nest.CreatureSelect = (from creature in creatures
                                   select new NestVM { Creature = creature, IsSelected = false }).ToList();

            return View(nest);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Nest(NestCM nestCm)
        {
            if (!ModelState.IsValid)
            {
                nestCm.Creatures = mythService.GetAllCreatures().Where(c => c.CreatureHasNest == false);
                var creatures = mythService.GetAllCreatures().Where(c => c.CreatureHasNest == false);
                nestCm.CreatureSelect = (from creature in creatures
                                       select new NestVM { Creature = creature, IsSelected = false }).ToList();
                var selectedIds = nestCm.CreatureSelect.Where(t => t.IsSelected).Select(t => t.Creature.CreatureId);
                return View(nestCm);
            }
            else
            {
                Nest nest = new Nest();
                nest.IsPlaced = false;
                nest.NestName = nestCm.Nest.NestName;

                var nestReturned = mythService.SaveNest(nest);
                var nestId = nestReturned.NestId;

                mythService.UpdateCreatureNest(nestCm.CreatureSelectedId, nestId);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult AllNests() 
        {
            NestCM cm = new NestCM();
            cm.Nests = mythService.GetAllNests();
            cm.Creatures = mythService.GetAllCreatures();
            cm.CreatureSelect = (from creature in mythService.GetAllCreatures()
                                   select new NestVM { Creature = creature, IsSelected = false }).ToList();
            return View(cm);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult EditNest(int id)
        {
            Nest nest = new Nest();
            nest = mythService.FindNestById(id);
           
            return View(nest);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult EditNest(Nest nest)
        {
            if (ModelState.IsValid)
            {
                mythService.SaveNest(nest);
                return RedirectToAction("Index");
            }
            else
            {
                return View(nest);
            }
            return RedirectToAction("Index");
        }
    }
}