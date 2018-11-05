using BTAdventure.Models;
using BTAdventure.Services;
using BTAdventure.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace BTAdventure.UI.Controllers
{
    public class GameWebApiController : ApiController
    {
        private GameService gameService;
        public GameWebApiController(GameService gameService)
        {
            this.gameService = gameService;
        }

        
       
        [Route("api/game/")]
        [HttpPost]
        public ReturnJSONObject Post(ChoiceJSONObject choice) //////ADD OUTCOMES
        {
            var PosOrNeg = choice.PositiveOrNegative;
            var eventChoice = gameService.FindEventChoiceById(choice.EventChoiceId);
            var nextRound = gameService.DetermineNextRound(eventChoice, choice.PositiveOrNegative);
            ReturnJSONObject response = new ReturnJSONObject();
            response.IsValidGame = true;
            response.Outcome = gameService.CheckOutcomeStatus(PosOrNeg, choice.EventChoiceId);

            if (nextRound.Item1 == ChoiceResult.Ending)
            {
                Ending ending = gameService.FindEndingById(nextRound.Item2);
                if (ending != null)
                {
                    response.Ending = ending;
                    response.IsEnding = true;
                    return response;
                }
                else
                {
                    response.IsValidGame = false;
                    return response;
                }
            }
            else if (nextRound.Item1 == ChoiceResult.Scene)
            {
                Scene scene = gameService.FindSceneById(nextRound.Item2);
                if (scene != null)
                {
                    List<EventChoice> choiceOfEvents = gameService.FindChoiceBySceneId(scene.SceneId).ToList();
                    foreach (var c in choiceOfEvents)
                    {
                        if (c.GenerationNumber == 0)
                        {
                            response.EventChoice = c;
                            response.Scene = gameService.FindSceneById(c.SceneId);
                            response.PlayerCharacter = gameService.FindPlayerCharacterById(choice.CharacterId);
                        }
                    }
                }
                else
                {
                    response.IsValidGame = false;
                    return response;
                }
            }
            else if (nextRound.Item1 == ChoiceResult.EventChoice)
            {
                var eventChoiceok = gameService.FindEventChoiceById(nextRound.Item2);
                if (eventChoiceok != null)
                {
                    response.EventChoice = eventChoiceok;
                    response.Scene = gameService.FindSceneById(eventChoiceok.SceneId);
                    response.PlayerCharacter = gameService.FindPlayerCharacterById(choice.CharacterId);
                }
                else
                {
                    response.IsValidGame = false;
                    return response;
                }
            }
            else
            {
                response.IsValidGame = false;
                return response;
            }

            response.PlayerCharacter.Gold += response.Outcome.Gold;
            response.PlayerCharacter.HealthPoints += response.Outcome.Health;
            response.PlayerCharacter.EventChoiceId = response.EventChoice.EventChoiceId; //this SEEMS weird, but since we are updateing the eventchoice table with the tuple, we also need to update PC
            response.PlayerCharacter.SceneId = response.EventChoice.SceneId;
            
            gameService.SaveCurrentPlayerCharacterGame(response.PlayerCharacter);   
            return response;
        }


    }



}