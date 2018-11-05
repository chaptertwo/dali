using BTAdventure.Models;
using BTAdventure.Services;
using BTAdventure.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace BTAdventure.UI.Controllers
{
    public class GameplayController : Controller
    {
        // GET: Gameplay
        private GameService gameSerivce;

        public GameplayController(GameService gameService)
        {
            this.gameSerivce = gameService;
        }

        public ActionResult NewGame()
        {

            PlayerGame playerGame = new PlayerGame();
            playerGame.Games = gameSerivce.FindAllGames();
            
            return View(playerGame);
        }

        [HttpPost]
        public ActionResult NewGame(PlayerGame playerGame)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var userId = claim.Value;

            var charName = playerGame.Character.CharacterName;
            var gameId = playerGame.SelectGameId;
            var startingScene = gameSerivce.FindAllScenes().FirstOrDefault(a => a.GameId == gameId);
            var events = gameSerivce.FindChoiceBySceneId(startingScene.SceneId);
            var firstEvent = events.First().EventChoiceId;

            var thisGame = gameSerivce.FindAllGames().FirstOrDefault(g => g.GameId == gameId);
            PlayerCharacter character = new PlayerCharacter();
            character.CharacterName = charName;
            character.PlayerId = userId;
            character.HealthPoints = thisGame.Health;
            character.Gold = thisGame.Gold;
            character.SceneId = startingScene.SceneId;
            character.EventChoiceId = firstEvent;
            //save

            var thisPlayerCharacter = gameSerivce.AddNewPlayerCharacter(character);

            PlayerGame game = new PlayerGame();
            game.Character = character;
            game.Game = thisGame;
            game.Scene = startingScene;
            //GameSceneVM vm = new GameSceneVM();
            //vm.PlayerCharacter = gameSerivce.FindPlayerCharacterById(thisPlayerCharacter.CharacterId);

            //vm.Scene = gameSerivce.FindSceneById(vm.PlayerCharacter.SceneId);
            //vm.EventChoice = gameSerivce.FindEventChoiceById(vm.PlayerCharacter.EventChoiceId);


            return View("Intro", game);
        }

        public ActionResult Intro(PlayerCharacter playerCharacter)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Intro(int id)
        {
            PlayerCharacter character = new PlayerCharacter();
            character.CharacterId = id;

            return RedirectToAction("Game", character);
        }


        [HttpGet]
        public ActionResult Game(PlayerCharacter player)//we're taking in a character id ... last i heard
        {
            GameSceneVM vm = new GameSceneVM();
            vm.PlayerCharacter = gameSerivce.FindPlayerCharacterById(player.CharacterId);
            
            vm.Scene = gameSerivce.FindSceneById(vm.PlayerCharacter.SceneId);
            vm.EventChoice = gameSerivce.FindEventChoiceById(vm.PlayerCharacter.EventChoiceId);
            return View(vm);
        }

        [HttpGet]
        public ActionResult Ending(int id)
        {
            GameSceneVM vm = new GameSceneVM();
            vm.Ending = gameSerivce.FindEndingById(id);

            return View(vm);
        }

        [HttpGet]
        public ActionResult HealthDepletedEnding()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}