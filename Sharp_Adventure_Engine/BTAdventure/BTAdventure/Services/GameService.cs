using BTAdventure.Interfaces;
using BTAdventure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BTAdventure.Services
{
    public class GameService
    {
        private IPlayerCharacterRepository characterRepo;
        private IGameRepository gamerepo;
        private IEventChoiceRepository choiceRepo;
        private IOutcomeRepository outcomeRepo;
        private IPlayerRepository playerRepo;
        private ISceneRepository sceneRepo;
        private IEndingRepository endingRepo;
        public GameService(IPlayerCharacterRepository characterRepo, IGameRepository gamerepo, IEventChoiceRepository choiceRepo,
            IOutcomeRepository outcomeRepo, IPlayerRepository playerRepo, ISceneRepository sceneRepo, IEndingRepository endingRepo)
        {
            this.characterRepo = characterRepo;
            this.gamerepo = gamerepo;
            this.choiceRepo = choiceRepo;
            this.outcomeRepo = outcomeRepo;
            this.playerRepo = playerRepo;
            this.sceneRepo = sceneRepo;
            this.endingRepo = endingRepo;
        }


        public Tuple<Outcome, Outcome> FindOutcomeByEventChoiceId(int id)
        {
            Outcome posOutcome = new Outcome();
            Outcome negOutcome = new Outcome();

            foreach (var o in outcomeRepo.FindOutcomeByEventChoiceId(id))
            {
                if (o.Positive)
                {
                    posOutcome = o;
                }
                else
                {
                    negOutcome = o;
                }
            }

            return Tuple.Create(posOutcome, negOutcome);
        }

        public IEnumerable<Scene> FindAllScenes()
        {
            return sceneRepo.All();
        }

        public Ending FindEndingById(int? item2)
        {
            return endingRepo.FindById(item2);
        }

        public PlayerCharacter AddNewPlayerCharacter(PlayerCharacter character)
        {
            return characterRepo.AddNewPlayerCharacter(character);
        }

        public Scene FindSceneById(int id)
        {
            return sceneRepo.FindById(id);
        }

        public Scene FindSceneById(int? id)
        {
            return sceneRepo.FindById(id);
        }

        public Outcome FindOutcomeById(int id)
        {
            return outcomeRepo.FindById(id);
        }

        public EventChoice FindEventChoiceById(int? id)
        {
            return choiceRepo.FindById(id);
        }
        public PlayerCharacter FindPlayerCharacterById(int id)
        {
            return characterRepo.FindById(id);
        }

        public IEnumerable<Ending> FindAllEndings()
        {
            return endingRepo.All();
        }

        public Player FindPlayerById(string id)
        {
            return playerRepo.FindById(id);
        }

        public List<Game> FindAllGames()
        {
            return gamerepo.All().ToList();
        }

        public Game FindGameById(int id)
        {
            return gamerepo.FindById(id);
        }

        public Player SavePlayer(Player player)
        {
            var allPlayers = playerRepo.All();
            player.PlayerId = allPlayers.Any() ? allPlayers.Count() + 1 : 1;
            return playerRepo.Save(player);
        }

        public PlayerCharacter SaveCurrentPlayerCharacterGame(PlayerCharacter playerCharacter)
        {
            //PlayerCharacter currentCharacter;
            //var allPlayerCharacter = characterRepo.All();
            //currentCharacter = allPlayerCharacter.First(p => p.CharacterId == characterId && p.PlayerId == playerId);
            //currentCharacter.SceneId = sceneId;
            //currentCharacter.EventChoiceId = eventChoiceId;
            ////need to figure out how to handle the gold and health

            return characterRepo.Save(playerCharacter);
        }

        public Outcome CheckOutcomeStatus(bool posOrNeg, int eventChoiceId)
        {
            return outcomeRepo.CheckOutComeStatus(posOrNeg, eventChoiceId);
        }

        public Game NewGame(int playerId, int playerCharacterId, int sceneId)
        {
            var game = gamerepo.FindById(sceneId);

            return game;
        }

        public List<PlayerCharacter> FindListOfPlayerCharactersByPlayerId(string playerId)
        {
            var characters = new List<PlayerCharacter>();

            //sceneId and characterId is stored in player character table which determines all the game the player has
            characters = characterRepo.All().Where(c => c.PlayerId == playerId).ToList();

            return characters;
        }

        public IEnumerable<Outcome> GetOutcomes()
        {
            return outcomeRepo.All();
        }


        public IEnumerable<EventChoice> FindChoiceBySceneId(int? sceneRoute)
        {
            return choiceRepo.FindBySceneId(sceneRoute);
        }

        
        public Tuple<ChoiceResult, int?> DetermineNextRound(EventChoice eventChoice, bool isPositive)
        {
            int? route = null;
            if (isPositive)
            {
                if(eventChoice.PositiveEndingId == null)
                {
                    if(eventChoice.PositiveSceneRoute == null)
                    {
                        if(eventChoice.PositiveRoute == null)
                        {
                            route = -1;
                            return Tuple.Create(ChoiceResult.Error, route);
                        }
                        else
                        {
                            route = eventChoice.PositiveRoute;
                            return Tuple.Create(ChoiceResult.EventChoice, route);
                        }
                    }
                    else
                    {
                        route = eventChoice.PositiveSceneRoute;
                        return Tuple.Create(ChoiceResult.Scene, route);
                    }
                }
                else
                {
                    route = eventChoice.PositiveEndingId;
                    return Tuple.Create(ChoiceResult.Ending, route);
                }
            }
            else
            {
                if (eventChoice.NegativeEndingId == null)
                {
                    if (eventChoice.NegativeSceneRoute == null)
                    {
                        if (eventChoice.NegativeRoute == null)
                        {
                            route = -1;
                            return Tuple.Create(ChoiceResult.Error, route);
                        }
                        else
                        {
                            route = eventChoice.NegativeRoute;
                            return Tuple.Create(ChoiceResult.EventChoice, route);
                        }
                    }
                    else
                    {
                        route = eventChoice.NegativeSceneRoute;
                        return Tuple.Create(ChoiceResult.Scene, route);
                    }
                }
                else
                {
                    route = eventChoice.NegativeEndingId;
                    return Tuple.Create(ChoiceResult.Ending, route);
                }
            }
        }
    }
}
