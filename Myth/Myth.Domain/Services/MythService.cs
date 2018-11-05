using Myth.Domain.Interfaces;
using Myth.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myth.Domain.Services
{
    public class MythService
    {
        private ICreatureRepository creatureRepo;
        private IFootprintRepository footprintRepo;
        private INestRepository nestRepo;
        private IRegionRepository regionRepo;
        private ITraitRepository traitRepo;
        private ITypeRepository typeRepo;

        public MythService(ICreatureRepository creatureRepo, IFootprintRepository footprintRepo, INestRepository nestRepo,
            IRegionRepository regionRepo, ITraitRepository traitRepo, ITypeRepository typeRepo)
        {
            this.creatureRepo = creatureRepo;
            this.footprintRepo = footprintRepo;
            this.nestRepo = nestRepo;
            this.regionRepo = regionRepo;
            this.traitRepo = traitRepo;
            this.typeRepo = typeRepo;
        }

        public IEnumerable<Creature> CombineCreatureAttributes()
        {
            var creatures = creatureRepo.All();
            foreach (var c in creatures)
            {
                c.Traits = GetTraitsByCreature(c.CreatureId);
                c.Type = GetTypesByCreatureId(c.CreatureId);
                c.Nest = FindNestByCreatureId(c.CreatureId);
            }
            return creatures;
        }

        public IEnumerable<Footprint> GetFootprintsByCreature(Creature creature)
        {
            return footprintRepo.GetFootprintsByCreature(creature);
        }

        public void SaveNestFromMap(Nest nest)
        {
            nestRepo.Save(nest);
        }

        public IEnumerable<Footprint> GetAllFootprints()
        {
            return footprintRepo.All();
        }

        public Nest FindNestByCreatureId(int id)
        {
            return nestRepo.FindByCreatureId(id);
        }

        public IEnumerable<Nest> GetAllNests()
        {
            return nestRepo.All();
        }

        public void SaveCreatureFromMap(Creature creature)
        {
            creatureRepo.SaveFromMap(creature);
        }

        public IEnumerable<CreatureType> GetAllTypes()
        {
            return typeRepo.All();
        }

        public Creature SaveCreature(Creature creature)
        {
            return creatureRepo.Save(creature);
        }

        public IEnumerable<Trait> GetAllTraits()
        {
            return traitRepo.All();
        }

        public IEnumerable<Creature> GetAllCreatures()
        {
            return creatureRepo.All();
        }

        public IEnumerable<Trait> GetTraitsByCreature(int id)
        {
            return traitRepo.FindManyByCreatureId(id);
        }

        public CreatureType GetTypesByCreatureId(int id)
        {
            return typeRepo.FindByCreatureId(id);
        }

        public bool DeleteCreatureById(int id)
        {
            return creatureRepo.Delete(id);
        }

        public bool SaveCreatureTrait(int traitId, int creatureId)
        {
            return traitRepo.InsertByCreatureId(traitId, creatureId);
        }

        public bool UpdateCreatureTrait(int traitId, int creatureId)
        {
               return traitRepo.InsertByCreatureId(traitId, creatureId);
        }

        public void DeleteFootprintsByCreature(int id)
        {
            var thisCreature = creatureRepo.FindById(id);
            var footprints = footprintRepo.All().Where(f => f.CreatureId == thisCreature.CreatureId);
            foreach(var f in footprints)
            {
                footprintRepo.Delete(f.FootprintId);
            }
        }

        public Trait SaveTrait(Trait trait)
        {
            return traitRepo.Save(trait);
        }

        public void DeleteTraitsByCreatureId(int creatureId)
        {
            traitRepo.DeleteAllTraitsPerCreature(creatureId);
        }

        public Nest SaveNest(Nest nest)
        {
            return nestRepo.Save(nest);
        }

        //randomizes footprints based on creature location and creature's primary trait.
        public IEnumerable<Footprint> RandomizePrints(int id) //gets a creature
        {
            var creature = creatureRepo.All().FirstOrDefault(c => c.CreatureId == id);
            var nest = nestRepo.All().FirstOrDefault(n => n.NestId == creature.NestId);
            var lat = creature.CreatureLat;
            var lng = creature.CreatureLong;
            int distance;
            int numberOfPrints;

            var thisCreaturesTrait = creature.TraitId;
            ////calm 2, timid 3, hardy 5, careful 5, hasty 3, brave 2
            if(thisCreaturesTrait == 1)
            {
                distance = 80;
                numberOfPrints = 2;
            }
            else if(thisCreaturesTrait == 2)
            {
                distance = 95;
                numberOfPrints = 3;
            }
            else if(thisCreaturesTrait == 3)
            {
                distance = 128;
                numberOfPrints = 4;
            }
            else if(thisCreaturesTrait == 4)
            {
                distance = 148;
                numberOfPrints = 5;
            }
            else if(thisCreaturesTrait == 5)
            {
                distance = 92;
                numberOfPrints = 4;
            }
            else if(thisCreaturesTrait == 6)
            {
                distance = 71;
                numberOfPrints = 3;
            }
            else
            {
                distance = 60;
                numberOfPrints = 4;
            }
            List<Footprint> locations = new List<Footprint>();
            for (int i = 0; i < numberOfPrints; i++)
            {
                locations.Add(getLocation((double)lat, (double)lng, distance + 900));
            }
            foreach(var f in locations)
            {
                f.CreatureId = creature.CreatureId;
                f.FootprintDate = DateTime.Now;
                footprintRepo.Generate(f); 
            }
            return locations;
        }


        private static Random random = new Random();
        public static Footprint getLocation(double y0, double x0, int radius)
        {
            
            Footprint result = new Footprint();
            // Convert radius from meters to degrees
            double radiusInDegrees = radius / 111000f;

            double u = random.NextDouble();
            double v = random.NextDouble();
            double w = radiusInDegrees * Math.Sqrt(u);
            double t = 2 * Math.PI * v;
            double x = w * Math.Cos(t);
            double y = w * Math.Sin(t);

            // Adjust the x-coordinate for the shrinking of the east-west distances
            double new_x = x / Math.Cos(y0);

            double foundLongitude = new_x + x0;
            double foundLatitude = y + y0;
            double longitude = Math.Round(foundLongitude, 6);
            double latitude = Math.Round(foundLatitude, 6);
            result.FootprintLat = (decimal)latitude;
            result.FootprintLong = (decimal)longitude;
            return result;
        }

        public void UpdateCreatureNest(int creatureSelectedId, int nestId)
        {
            creatureRepo.UpdateCreatureNest(creatureSelectedId, nestId);
        }

        public Nest FindNestById(int id)
        {
            return nestRepo.FindById(id);
        }
    }
}
