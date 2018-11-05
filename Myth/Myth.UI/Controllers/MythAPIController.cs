using Myth.Domain.Models;
using Myth.Domain.Services;
using Myth.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Myth.UI.Controllers
{
    public class MythAPIController : ApiController
    {
        private MythService mythService;
        public MythAPIController(MythService mythService)
        {
            this.mythService = mythService;
        }
        
        [Route("game/nests")]
        [AcceptVerbs("GET")]
        public IHttpActionResult AllNests()
        {
            return Ok(mythService.GetAllNests().ToList());
        }

        [Route("game/creatures")]
        [AcceptVerbs("GET")]
        public IHttpActionResult AllCreatures()
        {
            return Ok(mythService.GetAllCreatures().ToList());
        }

        [Route("game/footprints")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GrabFootprints()
        {
            Creature creature = new Creature(); 
            FootprintVM vm = new FootprintVM();
            vm.Creatures = mythService.CombineCreatureAttributes();
            var types = mythService.GetAllTypes();
            var creatures = mythService.GetAllCreatures();
            foreach(var c in creatures)
            {
                c.Footprints = mythService.GetFootprintsByCreature(c);
                c.Traits = mythService.GetTraitsByCreature(c.CreatureId);
                c.Type = mythService.GetTypesByCreatureId(c.CreatureId);
                c.Nest = mythService.FindNestByCreatureId(c.CreatureId);
            }
            var footprints = mythService.GetAllFootprints();
            foreach (var f in footprints)
            {
                foreach(var c in creatures)
                {
                    if(c.CreatureId == f.CreatureId)
                    {
                        f.FootprintType = c.Type.FootprintType;
                    }
                }
            }
            return Ok(footprints); 
        }

        [Route("game/setnest")]
        [AcceptVerbs("POST")]
        public IHttpActionResult SetNest(Nest nest)
        {
            nest.IsPlaced = true;
            mythService.SaveNestFromMap(nest);
            return Ok(mythService.GetAllNests().ToList());
        }

        [Route("game/setcreature")]
        [AcceptVerbs("POST")]
        public IHttpActionResult SetCreature(Creature creature)
        {
            creature.CreatureIsPlaced = true;
            mythService.SaveCreatureFromMap(creature);
            mythService.RandomizePrints(creature.CreatureId);
            //IsPlaced
            return Ok(mythService.GetAllCreatures().ToList());
        }
    }


    
}

