using BTAdventure.Data.DapperRepositories;
using BTAdventure.Models;
using BTAdventure.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTAdventure.Tests
{
    [TestFixture]
    class DapperRepoTests
    {
        [SetUp]
        public void Init()
        {
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["BinaryTextAdventure"].ConnectionString))
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
        public void AllEndingsTest()
        {
            DapperEndingRepository dapperEndingRepository = new DapperEndingRepository();

            List<Ending> endings = dapperEndingRepository.All().ToList();

            Assert.AreEqual(7, endings.Count());

            Assert.AreEqual(3, endings[0].GameId);
            Assert.AreEqual("The End", endings[0].EndingName);
            Assert.AreEqual("The end is at hand.", endings[0].EndingText);

        }

        [Test]
        public void FindEndingByIdTest()
        {
            DapperEndingRepository dapperEndingRepository = new DapperEndingRepository();

            Ending ending = dapperEndingRepository.FindById(2);

            Assert.AreEqual(2, ending.EndingId);
            Assert.AreEqual(1, ending.GameId);
            Assert.AreEqual("Bad End", ending.EndingName);
            Assert.AreEqual("Bad Ending here. Hi.", ending.EndingText);
        }

        [Test]
        public void SaveEnding()
        {
            DapperEndingRepository dapperEndingRepository = new DapperEndingRepository();

            Ending ending = new Ending()
            {
                GameId = 3,
                EndingName = "The Test",
                EndingText = "Test ending"
            };

            dapperEndingRepository.Save(ending);

            Ending insertedEnding = dapperEndingRepository.FindById(8);

            Assert.AreEqual(8, insertedEnding.EndingId);
            Assert.AreEqual(3, insertedEnding.GameId);
            Assert.AreEqual("The Test", insertedEnding.EndingName);
            Assert.AreEqual("Test ending", insertedEnding.EndingText);

            insertedEnding.GameId = 2;
            insertedEnding.EndingName = "The Updated Test";
            insertedEnding.EndingText = "Ending Test Update";

            dapperEndingRepository.Save(insertedEnding);

            Ending updatedEnding = dapperEndingRepository.FindById(8);

            Assert.AreEqual(8, updatedEnding.EndingId);
            Assert.AreEqual(2, updatedEnding.GameId);
            Assert.AreEqual("The Updated Test", updatedEnding.EndingName);
            Assert.AreEqual("Ending Test Update", updatedEnding.EndingText);

        }

        [Test]
        public void DeleteEndingByIdTest()
        {
            DapperEndingRepository dapperEndingRepository = new DapperEndingRepository();

            Ending ending = new Ending()
            {
                GameId = 3,
                EndingName = "The Test",
                EndingText = "Test ending"
            };

            dapperEndingRepository.Save(ending);

            List<Ending> endings = dapperEndingRepository.All().ToList();

            Ending insertedEnding = dapperEndingRepository.FindById(8);

            Assert.AreEqual(8, endings.Count);
            Assert.AreEqual(8, insertedEnding.EndingId);
            Assert.AreEqual("Test ending", insertedEnding.EndingText);

            dapperEndingRepository.Delete(8);

            endings = dapperEndingRepository.All().ToList();

            Assert.AreEqual(7, endings.Count);
        }

        [Test]
        public void AllEventChoiceTest()
        {
            DapperEventChoiceRepository dapperRepository = new DapperEventChoiceRepository();

            List<EventChoice> choices = dapperRepository.All().ToList();

            Assert.AreEqual(5, choices.Count());

            Assert.AreEqual(1, choices[0].EventChoiceId);
            Assert.AreEqual(1, choices[0].SceneId);
            Assert.AreEqual(0, choices[0].GenerationNumber);
            Assert.AreEqual("Greeting Event", choices[0].EventName);
            Assert.AreEqual("You are greeted in the foyer.", choices[0].StartText);
            Assert.AreEqual("You are gracious to your host", choices[0].PositiveText);
            Assert.AreEqual("You punch numerous holes in the foyer walls. A magical portal opens from one hole and you are teleported somewhere.", choices[0].NegativeText);
            Assert.AreEqual(2, choices[0].PositiveRoute);
            Assert.AreEqual(null, choices[0].NegativeRoute);
            Assert.AreEqual("Be gracious.", choices[0].PositiveButton);
            Assert.AreEqual("Punch holes in walls.", choices[0].NegativeButton);
            Assert.AreEqual(null, choices[0].PositiveSceneRoute);
            Assert.AreEqual(7, choices[0].NegativeSceneRoute);
            Assert.AreEqual(null, choices[0].PositiveEndingId);
            Assert.AreEqual(null, choices[0].NegativeEndingId);

        }

        [Test]
        public void FindEventChoiceByIdTest()
        {
            DapperEventChoiceRepository dapperRepository = new DapperEventChoiceRepository();

            EventChoice choice = dapperRepository.FindById(1);

            Assert.AreEqual(1, choice.EventChoiceId);
            Assert.AreEqual(1, choice.SceneId);
            Assert.AreEqual(0, choice.GenerationNumber);
            Assert.AreEqual("Greeting Event", choice.EventName);
            Assert.AreEqual("You are greeted in the foyer.", choice.StartText);
            Assert.AreEqual("You are gracious to your host", choice.PositiveText);
            Assert.AreEqual("You punch numerous holes in the foyer walls. A magical portal opens from one hole and you are teleported somewhere.", choice.NegativeText);
            Assert.AreEqual(2, choice.PositiveRoute);
            Assert.AreEqual(null, choice.NegativeRoute);
            Assert.AreEqual("Be gracious.", choice.PositiveButton);
            Assert.AreEqual("Punch holes in walls.", choice.NegativeButton);
            Assert.AreEqual(null, choice.PositiveSceneRoute);
            Assert.AreEqual(7, choice.NegativeSceneRoute);
            Assert.AreEqual(null, choice.PositiveEndingId);
            Assert.AreEqual(null, choice.NegativeEndingId);

        }

        [Test]
        public void SaveEventChoice()
        {
            DapperEventChoiceRepository dapperRepository = new DapperEventChoiceRepository();

            EventChoice newChoice = new EventChoice()
            {
                SceneId = 1,
                GenerationNumber = 2, 
                EventName = "TestEvent",
                StartText = "TestStart",
                PositiveText = "PositiveText",
                NegativeText = "NegativeText",
                PositiveRoute = null,
                NegativeRoute = null,
                PositiveButton = "PButton",
                NegativeButton = "NButton",
                PositiveSceneRoute = 7,
                NegativeSceneRoute = null,
                PositiveEndingId = null,
                NegativeEndingId = 3
            };

            dapperRepository.Save(newChoice);

            List<EventChoice> choices = dapperRepository.All().ToList();

            EventChoice choice = dapperRepository.FindById(6);

            Assert.AreEqual(6, choices.Count);

            Assert.AreEqual(6, choice.EventChoiceId);
            Assert.AreEqual(1, choice.SceneId);
            Assert.AreEqual(2, choice.GenerationNumber);
            Assert.AreEqual("TestEvent", choice.EventName);
            Assert.AreEqual("TestStart", choice.StartText);
            Assert.AreEqual("PositiveText", choice.PositiveText);
            Assert.AreEqual("NegativeText", choice.NegativeText);
            Assert.AreEqual(null, choice.PositiveRoute);
            Assert.AreEqual(null, choice.NegativeRoute);
            Assert.AreEqual("PButton", choice.PositiveButton);
            Assert.AreEqual("NButton", choice.NegativeButton);
            Assert.AreEqual(7, choice.PositiveSceneRoute);
            Assert.AreEqual(null, choice.NegativeSceneRoute);
            Assert.AreEqual(null, choice.PositiveEndingId);
            Assert.AreEqual(3, choice.NegativeEndingId);
        }

        [Test]
        public void DeleteEventChoiceByIdTest()
        {
            DapperEventChoiceRepository dapperRepository = new DapperEventChoiceRepository();

            EventChoice newChoice = new EventChoice()
            {
                SceneId = 1,
                GenerationNumber = 2,
                EventName = "TestEvent",
                StartText = "TestStart",
                PositiveText = "PositiveText",
                NegativeText = "NegativeText",
                PositiveRoute = null,
                NegativeRoute = null,
                PositiveButton = "PButton",
                NegativeButton = "NButton",
                PositiveSceneRoute = 7,
                NegativeSceneRoute = null,
                PositiveEndingId = null,
                NegativeEndingId = 3
            };

            dapperRepository.Save(newChoice);

            List<EventChoice> choices = dapperRepository.All().ToList();

            EventChoice choice = dapperRepository.FindById(6);

            Assert.AreEqual(6, choices.Count);

            dapperRepository.Delete(6);

            choices = dapperRepository.All().ToList();

            Assert.AreEqual(5, choices.Count);
        }
    }
}
