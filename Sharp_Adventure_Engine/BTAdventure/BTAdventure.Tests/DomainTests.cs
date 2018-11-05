using BTAdventure.Data.DapperRepositories;
using BTAdventure.Models;
using BTAdventure.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTAdventure.Tests
{
    // this will be testing the domain/services and such
    [TestFixture]
    class DomainTests
    {
        [TestCase(6, 5, 4, 3, 1, 2, true, false, true)]
        [TestCase(6, 5, 4, 3, 1, 2, false, false, true)]
        [TestCase(6, 5, 4, 3, null, null, true, true, false)]
        [TestCase(6, 5, 4, 3, null, null, false, true, false)]
        [TestCase(6, 5, null, null, null, null, true, false, false)]
        [TestCase(6, 5, null, null, null, null, false, false, false)]
        [TestCase(null, null, null, null, null, null, true, false, false)]
        [TestCase(null, null, null, null, null, null, false, false, false)]
        public void CheckEventResult(int? pRoute, int? nRoute, int? pSRoute, int? nSRoute, int? pEnding, int? nEnding, bool positive, bool isScene, bool isEnding)
        {
            EventChoice choice = new EventChoice
            {
                EventChoiceId = 6,
                SceneId = 1,
                GenerationNumber = 0,
                EventName = "Test",
                StartText = "SText",
                PositiveText = "PText",
                NegativeText = "NText",
                PositiveButton = "PButton",
                NegativeButton = "NButton",
                PositiveRoute = pRoute,
                NegativeRoute = nRoute,
                PositiveSceneRoute = pSRoute,
                NegativeSceneRoute = nSRoute,
                PositiveEndingId = pEnding,
                NegativeEndingId = nEnding
            };

            DapperPlayerCharacterRepository dapperPlayerCharacterRepository = new DapperPlayerCharacterRepository();
            DapperGameRepository dapperGameRepository = new DapperGameRepository();
            DapperEventChoiceRepository dapperEventChoiceRepository = new DapperEventChoiceRepository();
            DapperOutcomeRepository dapperOutcomeRepository = new DapperOutcomeRepository();
            DapperPlayerRepository dapperPlayerRepository = new DapperPlayerRepository();
            DapperSceneRepository dapperSceneRepository = new DapperSceneRepository();
            DapperEndingRepository dapperEndingRepository = new DapperEndingRepository();

            GameService gService = new GameService(dapperPlayerCharacterRepository, dapperGameRepository, dapperEventChoiceRepository,
                dapperOutcomeRepository, dapperPlayerRepository, dapperSceneRepository, dapperEndingRepository);

            Tuple<ChoiceResult, int?> tuple = gService.DetermineNextRound(choice, positive);
            //EventChoice outputChoice = gService.DetermineNextRound(choice, positive);

            if (isEnding)
            {
                if (positive)
                {
                    Assert.AreEqual(ChoiceResult.Ending, tuple.Item1);
                    Assert.AreEqual(pEnding, tuple.Item2);
                }
                else
                {
                    Assert.AreEqual(ChoiceResult.Ending, tuple.Item1);
                    Assert.AreEqual(nEnding, tuple.Item2);
                }
            }
            else if (isScene)
            {
                if (positive)
                {
                    Assert.AreEqual(ChoiceResult.Scene, tuple.Item1);
                    Assert.AreEqual(pSRoute, tuple.Item2);
                }
                else
                {
                    Assert.AreEqual(ChoiceResult.Scene, tuple.Item1);
                    Assert.AreEqual(nSRoute, tuple.Item2);
                }
            }
            else if (tuple.Item2 == null || tuple.Item2 == -1)
            {
                Assert.AreEqual(ChoiceResult.Error, tuple.Item1);
            }
            else
            {
                if (positive)
                {
                    Assert.AreEqual(ChoiceResult.EventChoice, tuple.Item1);
                    Assert.AreEqual(pRoute, tuple.Item2);
                }
                else
                {
                    Assert.AreEqual(ChoiceResult.EventChoice, tuple.Item1);
                    Assert.AreEqual(nRoute, tuple.Item2);
                }
            }
        }





    }
}
