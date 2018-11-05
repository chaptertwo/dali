using BTAdventure.Models;
using BTAdventure.Services;
using BTAdventure.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTAdventure.UI.Controllers
{
    [Authorize(Roles ="Creator")]
    public class CreatorController : Controller
    {        
        private CreatorService creatorService;
        public CreatorController(CreatorService creatorService)
        {
            this.creatorService = creatorService;
        }

        // GET: Creator
        [Authorize(Roles = "Admin,Creator")]
        public ActionResult Index()
        {
            return View();
        }

        // this will probably be removed. will look into it later
        public ActionResult ListGames()
        {
            var allScene = creatorService.GetAllScenes();
            return View(allScene);
        }

        [Authorize(Roles = "Admin,Creator")]
        public ActionResult NewOrEditGame(int id = 0)
        {
            Game game = new Game();
            if (id > 0)
            {
                game = creatorService.GetAllGames().Where(g => g.GameId == id).FirstOrDefault();
            }

            ViewBag.GameId = game.GameId;

            return View(game);
        }


        [Authorize(Roles = "Admin,Creator")]
        public ActionResult EditGame()
        {
            var allGames = creatorService.GetAllGames();

            //this is mock data
            //var something = new Game
            //{
            //    GameId = 1,
            //    GameTitle = "The Title",
            //    IntroText = "The Intro"
            //};
            //allGames.Add(something);
            //allGames.Add(something);
            //allGames.Add(something);
            //allGames.Add(something);
            //allGames.Add(something);
            return View(allGames);
        }

        [Authorize(Roles = "Admin,Creator")]
        public ActionResult SceneMain(int id = 0)
        {
            IEnumerable<Scene> allScenesFromGameId = new List<Scene>();
            if (id > 0)
            {

                allScenesFromGameId = creatorService.GetAllScenes().Where(s => s.GameId == id);
                ViewBag.GameTitle = creatorService.GetAllGames().Where(g => g.GameId == id).First().GameTitle;
                ViewBag.GameId = creatorService.GetAllGames().Where(g => g.GameId == id).First().GameId;
            }

            return View(allScenesFromGameId);
        }

        [Authorize(Roles = "Admin,Creator")]
        [HttpPost]
        public ActionResult SceneMain(Game game)
        {
            //Note: Added game just to see if this would compile. Also replaced Add(game) with Add(scene). Sorry to step on any toes. Don't hate me. - Rich
            //Game game = new Game();
            if (game.GameId == 0)
            {
                game = creatorService.CreateGame(game);
            }
            else
            {
                creatorService.EditGame(game);
            }
            ViewBag.GameTitle = creatorService.GetAllGames().Where(g => g.GameId == game.GameId).First().GameTitle;
            ViewBag.GameId = creatorService.GetAllGames().Where(g => g.GameId == game.GameId).First().GameId;

            IEnumerable<Scene> allScenesFromGameId = new List<Scene>();
            if (game.GameId > 0)
            {
                allScenesFromGameId = creatorService.GetAllScenes().Where(s => s.GameId == game.GameId);
            }
            //Changed game to scene here. Also gor rid of the "allScenesFromGameId = " part.
            //allScenesFromGameId.ToList().Add(scene);

            ViewBag.gameId = game.GameId;

            return View(allScenesFromGameId);
        }

        [Authorize(Roles = "Admin,Creator")]
        public ActionResult CreateOrEditScene(int? sceneId, int gameId)
        {
            //will need to move to service/domain layer eventually
            Scene scene;
            if (gameId > 0)
            {
                scene = creatorService.GetAllScenes().Where(s=>s.SceneId == sceneId).FirstOrDefault();
                if (scene == null)
                {
                    scene = new Scene { GameId = gameId };
                }

            }
            else
            {

                scene = new Scene { GameId = gameId };
            }

            ViewBag.GameId = gameId;

            return View(scene);
        }

        [Authorize(Roles = "Admin,Creator")]
        [HttpPost]
        public ActionResult CreateOrEditScene(Scene scene)
        {
            if (scene.SceneId > 0)
            {
                scene = creatorService.SaveScene(scene);
            }
            else
            {

                scene = creatorService.CreateScene(scene);
            }

            ViewBag.GameId = scene.GameId;

            return RedirectToAction("SceneMain",new { id = scene.GameId});
        }

        [Authorize(Roles = "Admin,Creator")]
        public ActionResult EditGeneration(int id)
        {
            //will probably need to call a different method to assign values to the VM, possibly
            var model = new EditGenerationVM();
            //model.AllScenes = creatorService.GetAllScenes().Where(s => s.GameId == gameId);
            //model.CurrentEvent = creatorService.GetAllEventChoice().First();
            //model.AllEventByScene = creatorService.GetAllEventChoice().Where(e=>e.SceneId == model.CurrentEvent.SceneId);
            ViewBag.SceneId = id;
            ViewBag.GameId = creatorService.GetAllScenes().Where(s => s.SceneId == id).First().GameId;
            model.AllEventByScene = creatorService.GetAllEventChoice().Where(e => e.SceneId == id);
            model.AllEventChoice = creatorService.GetAllEventChoice();
            model.AllScene = creatorService.GetAllScenes();
            ViewBag.sceneTitle = creatorService.GetAllScenes().Where(s=>s.SceneId == id).First().SceneName;
            return View("EditGeneration", model);
        }

        [Authorize(Roles = "Admin,Creator")]
        [HttpPost]
        public ActionResult EditSceneEventChoice(EditCreateEventRequest editCreateEventRequest)
        {
            var model = new EventCreationData();
            model.SceneId = editCreateEventRequest.SceneId;
            model.EventChoice = creatorService.FindEventById(editCreateEventRequest.EventId);
            if(model.EventChoice == null)
            {
                model.EventChoice = new EventChoice()
                {
                    EventChoiceId = 0,
                    SceneId = editCreateEventRequest.SceneId
                };

                model.PositiveOutcome = new Outcome()
                {
                    Positive = true
                };
                model.NegativeOutcome = new Outcome();
            }
            else
            {
                Tuple<Outcome, Outcome> posNegOutcomes = creatorService.FindOutcomesByEventId(model.EventChoice.EventChoiceId);
                model.PositiveOutcome = posNegOutcomes.Item1;
                model.NegativeOutcome = posNegOutcomes.Item2;
            }

            List<EventChoice> availableEvents = new List<EventChoice>();

            foreach(var e in creatorService.FindEventsWithHigherGenNumber(model.EventChoice.EventChoiceId, model.SceneId))
            {
                if(e.EventChoiceId != model.EventChoice.EventChoiceId)
                {
                    availableEvents.Add(e);
                }
            }

            model.AvailableChoices = availableEvents;
                        
            List<Scene> otherScenes = new List<Scene>();

            foreach(var s in creatorService.FindScenesInGameBySceneId(model.SceneId))
            {
                if (s.SceneId != model.SceneId)
                {
                    otherScenes.Add(s);
                }
            }

            model.GameScenes = otherScenes;

            model.GameEndings = creatorService.FindGameEndingBySceneId(model.SceneId);

            return View("CreateEditEvent",model);
        }

        private ActionResult FailedSaveEvent(SaveEventRequest saveEventRequest)
        {
            EventCreationData eventCreationData = new EventCreationData();
            eventCreationData.EventChoice = new EventChoice
            {
                EventChoiceId = saveEventRequest.EventId,
                SceneId = saveEventRequest.SceneId,
                GenerationNumber = saveEventRequest.GenerationNumber,
                EventName = saveEventRequest.EventName,
                StartText = saveEventRequest.StartText,
                PositiveText = saveEventRequest.PositiveText,
                NegativeText = saveEventRequest.NegativeText,
                PositiveButton = saveEventRequest.PositiveButton,
                NegativeButton = saveEventRequest.NegativeButton,
                PositiveRoute = saveEventRequest.PositiveRoute,
                NegativeRoute = saveEventRequest.NegativeRoute,
                PositiveSceneRoute = saveEventRequest.PositiveSceneRoute,
                NegativeSceneRoute = saveEventRequest.NegativeSceneRoute,
                PositiveEndingId = saveEventRequest.PositiveEndingId,
                NegativeEndingId = saveEventRequest.NegativeEndingId
            };
            eventCreationData.AvailableChoices = creatorService.FindEventsWithHigherGenNumber(saveEventRequest.EventId, saveEventRequest.SceneId);
            List<Scene> scenes = new List<Scene>();
            foreach(var s in creatorService.FindScenesInGameBySceneId(saveEventRequest.SceneId))
            {
                if(s.SceneId != saveEventRequest.SceneId)
                {
                    scenes.Add(s);
                }
            }
            
            eventCreationData.GameScenes = scenes;
            eventCreationData.GameEndings = creatorService.FindGameEndingBySceneId(saveEventRequest.SceneId);
            eventCreationData.PositiveOutcome = new Outcome()
            {
                EventChoiceId = saveEventRequest.EventId,
                OutcomeId = saveEventRequest.PositiveOutcomeId,
                Positive = true,
                Health = saveEventRequest.PositiveHealth,
                Gold = saveEventRequest.PositiveGold
            };
            eventCreationData.NegativeOutcome = new Outcome()
            {
                EventChoiceId = saveEventRequest.EventId,
                OutcomeId = saveEventRequest.NegativeOutcomeId,
                Positive = false,
                Health = saveEventRequest.NegativeHealth,
                Gold = saveEventRequest.NegativeGold
            };
            eventCreationData.SceneId = saveEventRequest.SceneId;
            
            ViewBag.GameId = scenes.First().GameId;

            return View("CreateEditEvent", eventCreationData);
        }

        [Authorize(Roles = "Admin,Creator")]
        [HttpPost]
        public ActionResult SaveEvent(SaveEventRequest saveEventRequest)
        {
            bool isValid = false;

            EventChoice originalChoice = new EventChoice();

            if (saveEventRequest.EventId > 0)
            {
                originalChoice = creatorService.FindEventById(saveEventRequest.EventId);
            }

            EventChoice eventChoice = new EventChoice
            {
                EventChoiceId = saveEventRequest.EventId,
                SceneId = saveEventRequest.SceneId,
                GenerationNumber = saveEventRequest.GenerationNumber,
                EventName = saveEventRequest.EventName,
                StartText = saveEventRequest.StartText,
                ImgUrl = saveEventRequest.ImgURL,
                PositiveText = saveEventRequest.PositiveText,
                NegativeText = saveEventRequest.NegativeText,
                PositiveButton = saveEventRequest.PositiveButton,
                NegativeButton = saveEventRequest.NegativeButton,
                PositiveRoute = saveEventRequest.PositiveRoute,
                NegativeRoute = saveEventRequest.NegativeRoute,
                PositiveSceneRoute = saveEventRequest.PositiveSceneRoute,
                NegativeSceneRoute = saveEventRequest.NegativeSceneRoute,
                PositiveEndingId = saveEventRequest.PositiveEndingId,
                NegativeEndingId = saveEventRequest.NegativeEndingId
            };

            eventChoice = creatorService.SaveEventChoice(eventChoice);
            
            if (saveEventRequest.EventId > 0)
            {
                creatorService.RecalculateOldGenNumbers(originalChoice);
            }

            if (eventChoice.EventChoiceId > 0)
            {
                isValid = true;
            }
            else
            {
                isValid = false;
            }

            if (isValid)
            {
                Outcome positiveOutcome = new Outcome()
                {
                    EventChoiceId = eventChoice.EventChoiceId,
                    OutcomeId = saveEventRequest.PositiveOutcomeId,
                    Positive = true,
                    Health = saveEventRequest.PositiveHealth,
                    Gold = saveEventRequest.PositiveGold
                };
                Outcome negativeOutcome = new Outcome()
                {
                    EventChoiceId = eventChoice.EventChoiceId,
                    OutcomeId = saveEventRequest.NegativeOutcomeId,
                    Positive = false,
                    Health = saveEventRequest.NegativeHealth,
                    Gold = saveEventRequest.NegativeGold
                };

                positiveOutcome = creatorService.SaveOutcome(positiveOutcome);
                negativeOutcome = creatorService.SaveOutcome(negativeOutcome);

                if(positiveOutcome == null || negativeOutcome == null)
                {
                    isValid = false;
                }
            }
            
            if (isValid)
            {
                return EditGeneration(saveEventRequest.SceneId);
            }
            else
            {
                return FailedSaveEvent(saveEventRequest);
            }
        }
        
        [Authorize(Roles = "Admin,Creator")]
        public ActionResult DeleteEvent(EditCreateEventRequest editCreateEventRequest)
        {
            creatorService.DeleteEventChoice(editCreateEventRequest.EventId);

            return EditGeneration(editCreateEventRequest.SceneId); 
        }

        public ActionResult DeleteGame(int id)
        {
            var model = creatorService.GetAllGames().Where(g => g.GameId == id).First();

            ViewBag.GameId = model.GameId;

            return View(model);
        }

        [Authorize(Roles = "Admin,Creator")]
        [HttpPost]
        public ActionResult DeleteGame(Game game)
        {
            creatorService.DeleteGame(game.GameId);
            ViewBag.GameId = game.GameId;
            return RedirectToAction("EditGame");
        }

        [Authorize(Roles = "Admin,Creator")]
        public ActionResult DeleteScene(int id)
        {
            var scene = creatorService.GetAllScenes().Where(s=>s.SceneId == id).First();

            ViewBag.GameId = scene.GameId;

            return View(scene);
        }

        [Authorize(Roles = "Admin,Creator")]
        [HttpPost]
        public ActionResult DeleteScene(Scene scene)
        {
            creatorService.DeleteScene(scene.SceneId);
            
            ViewBag.GameId = scene.GameId;

            return RedirectToAction("SceneMain",new { id=scene.GameId });
        }

        [Authorize(Roles = "Admin,Creator")]
        public ActionResult NewEnding(int gameId) //grab game id
        {
            //find game by game id, then associate ending..
            var thisGame = creatorService.GetAllGames().FirstOrDefault(g => g.GameId == gameId);
            Ending ending = new Ending();
            ending.GameId = gameId;

            ViewBag.GameId = gameId;

            return View(ending);
        }

        [Authorize(Roles = "Admin,Creator")]
        [HttpPost]
        public ActionResult NewEnding(Ending ending)
        {
            ViewBag.GameId = creatorService.GetAllGames().Where(g => g.GameId == ending.GameId).First().GameId;
            creatorService.SaveEnding(ending);

            return RedirectToAction("Index"); //MAYBE RETURN TO SCENEMAIN
        }

        [Authorize(Roles = "Admin,Creator")]
        public ActionResult EditEnding(int gameId) //grab ending id
        {
            //EndingVM endings = new EndingVM();
            var endings = creatorService.GetAllEndings().Where(g => g.GameId == gameId).ToList();
            ViewBag.GameId = creatorService.GetAllGames().Where(g => g.GameId == gameId).First().GameId;

            return View(endings);
        }

        [Authorize(Roles = "Admin,Creator")]
        [HttpPost]
        public ActionResult EditEnding(List<Ending> endings)
        {
            ViewBag.GameId = endings.First().GameId;
            foreach (var e in endings)
            {
                creatorService.EditEnding(e);
            }
            return RedirectToAction("Index"); //MAYBE RETURN TO SCENEMAIN
        }

        [Authorize(Roles = "Admin,Creator")]
        public ActionResult DeleteEnding(int id)
        {
            var thisEnding = creatorService.GetAllEndings().FirstOrDefault(g => g.EndingId == id);
            creatorService.DeleteEndingById(thisEnding.EndingId);

            ViewBag.GameId = thisEnding.GameId;

            //delete from service
            return RedirectToAction("Index");
        }
    }
}