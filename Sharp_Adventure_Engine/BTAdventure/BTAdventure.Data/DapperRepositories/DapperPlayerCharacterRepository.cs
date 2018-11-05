using BTAdventure.Interfaces;
using BTAdventure.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTAdventure.Data.DapperRepositories
{
    public class DapperPlayerCharacterRepository : IPlayerCharacterRepository
    {
        private const string CONN_STRING_KEY = "BinaryTextAdventure";

        public PlayerCharacter AddNewPlayerCharacter(PlayerCharacter character)
        {
            const string sql = "INSERT INTO PlayerCharacter (PlayerId, SceneId, EventChoiceId, CharacterName, HealthPoints, Gold) "
                   + "VALUES (@PlayerId, @SceneId, @EventChoiceId, @CharacterName, @HealthPoints, @Gold); "
                   + "SELECT SCOPE_IDENTITY()";

            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                character.CharacterId = conn.Query<int>(sql, character).First();
            }
            return character;

        }

        public IEnumerable<PlayerCharacter> All()
        {
            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                return conn.Query<PlayerCharacter>("SELECT CharacterId, PlayerId, SceneId, EventChoiceId, CharacterName, HealthPoints, Gold FROM PlayerCharacter;");
            }
        }

        public IEnumerable<PlayerCharacter> AllLoggedIn(string UserID)
        {
            var result = new List<PlayerCharacter>();

            foreach(var c in All())
            {
                if(c.PlayerId == UserID)
                {
                    result.Add(c);
                }
            }
            return result;
        }

        public bool Delete(int id)
        {
            const string sql = "DELETE FROM PlayerCharacter WHERE CharacterId = @CharacterId";
            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                return conn.Execute(sql, new { CharacterId = id }) > 0;
            }
        }

        public PlayerCharacter FindById(int id)
        {
            const string sql = "SELECT CharacterId, PlayerId, SceneId, EventChoiceId, CharacterName, HealthPoints, Gold "
                   + "FROM PlayerCharacter "
                   + "WHERE CharacterId = @CharacterId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                return conn.Query<PlayerCharacter>(sql, new { CharacterId = id })
                    .FirstOrDefault();
            }
        }

        

        public PlayerCharacter Save(PlayerCharacter character)
        {
            if (character.CharacterId > 0)
            {
                return Update(character);
            }
            return Insert(character);
        }

        private PlayerCharacter Insert(PlayerCharacter character)
        {
            const string sql = "INSERT INTO PlayerCharacter (PlayerId, SceneId, EventChoiceId,, CharacterName, HealthPoints, Gold) "
                   + "VALUES (@PlayerId, @SceneId, @EventChoiceId, @CharacterName, @HealthPoints, @Gold); "
                   + "SELECT SCOPE_IDENTITY()";

            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                character.CharacterId = conn.Query<int>(sql, character).First();
            }
            return character;
        }

        private PlayerCharacter Update(PlayerCharacter character)
        {
            const string sql = "UPDATE PlayerCharacter SET "
                + "PlayerId = @PlayerId, "
                + "SceneId = @SceneId, "
                + "EventChoiceId = @EventChoiceId, "
                + "CharacterName = @CharacterName, "
                + "HealthPoints = @HealthPoints, "
                + "Gold = @Gold "
                + "WHERE CharacterId = @CharacterId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                if (conn.Execute(sql, character) > 0)
                {
                    return character;
                }
            }

            return null;
        }
    }
}
