using Myth.Data.Repositories;
using Myth.Domain.Models;
using Myth.Domain.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myth.Tests
{
    [TestFixture]
    class RepoTests
    {
        [SetUp] 
        public void SetUp()
        {
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "DbReset";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cn;
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        


        [Test]
        public void AllCreatures()
        {
            var repo = new CreatureRepository();
            var creatures = repo.All();
            Assert.IsTrue(creatures != null && creatures.Any());
        }

        [Test]
        public void AllFootprints()
        {
            var repo = new FootprintRepository();
            var footprints = repo.All();
            Assert.IsTrue(footprints != null && footprints.Any());
        }

        [TestCase(1)]
        [TestCase(2)]
        public void FindById(int creatureId)
        {
            var repo = new CreatureRepository();
            var creature = repo.FindById(creatureId);
            Assert.AreEqual(creatureId, creature.CreatureId);
        }

        [TestCase("The Thing", 36.204824, 138.252924)]
        [TestCase("Ripley", 57.1472, -2.097237)]
        public void InsertCreature(string creatureName, decimal lat, decimal longitude)
        {
            var repo = new CreatureRepository();
            var creature = new Creature
            {
                CreatureName = creatureName,
                CreatureLat = lat,
                CreatureLong = longitude,
                NestId = 1,
                TraitId = 1,
                TypeId = 1
            };
            var result = repo.Save(creature);
            Assert.IsTrue(result.CreatureId > 0 && creature.CreatureIsPlaced == false);
        }

        [Test]
        public void DeleteCreature()
        {
            var repo = new CreatureRepository();
            var creature = new Creature
            {
                CreatureName = "Red Steward",
                CreatureLat = 36.204824M,
                CreatureLong = 138.252924M,
                NestId = 1,
                TraitId = 1,
                TypeId = 1
            };
            var saved = repo.Save(creature);
            var creatureCount = repo.All();
            Assert.IsTrue(repo.Delete(saved.CreatureId));
            Assert.AreEqual(7, creatureCount.Count());
        }

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        public void UpdateCreature(int traitId, int id)
        {

            CreatureRepository creatureRepository = new CreatureRepository();
            FootprintRepository footprintRepository = new FootprintRepository();
            NestRepository nestRepository = new NestRepository();
            RegionRepository regionRepository = new RegionRepository();
            TraitRepository traitRepository = new TraitRepository();
            TypeRepository typeRepository = new TypeRepository();

            MythService service = new MythService(creatureRepository, footprintRepository, nestRepository, regionRepository, traitRepository, typeRepository);

            var repo = new CreatureRepository();
            var creature = repo.FindById(id);
            service.DeleteTraitsByCreatureId(id);
            creature.NestId = 1;
            creature.TypeId = 3;
            creature.CreatureIsRevealed = true;
            creature.CreatureIsPlaced = false;
            creature.CreatureName = "Yohann";

            Assert.NotNull(service.SaveCreatureTrait(traitId, id));
        }

        [TestCase(1)]
        [TestCase(2)]
        public void DeleteAllCreatureTraits(int creatureId)
        {
            var repo = new TraitRepository();
            var creatureRepo = new CreatureRepository();
            var creature = creatureRepo.FindById(creatureId);
            var result = repo.DeleteAllTraitsPerCreature(creature.CreatureId);
            Assert.IsEmpty(repo.FindManyByCreatureId(creatureId));
        }

        [TestCase(1, 1)]
        [TestCase(2, 2)]
        public void DeleteNest(int creatureId, int nestId)
        {
            var repo = new NestRepository();
            var nest = repo.FindById(nestId);
            var result = repo.Delete(creatureId, nestId);
            Assert.IsTrue(result);
        }
    }
}
