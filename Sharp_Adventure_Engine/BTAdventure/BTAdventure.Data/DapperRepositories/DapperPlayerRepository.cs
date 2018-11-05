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
    public class DapperPlayerRepository : IPlayerRepository
    {
        private const string CONN_STRING_KEY = "BinaryTextAdventure";

        public IEnumerable<Player> All()
        {
            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                return conn.Query<Player>("SELECT PlayerId, PlayerName FROM Player;");
            }
        }

        public bool Delete(int id)
        {
            const string sql = "DELETE FROM Player WHERE PlayerId = @PlayerId";
            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                return conn.Execute(sql, new { PlayerId = id }) > 0;
            }
        }

        public Player FindById(string id)
        {
            const string sql = "SELECT PlayerId, PlayerName "
                   + "FROM Player "
                   + "WHERE PlayerId = @PlayerId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                return conn.Query<Player>(sql, new { PlayerId = id })
                    .FirstOrDefault();
            }
        }

        public Player Save(Player player)
        {
            if (player.PlayerId > 0)
            {
                return Update(player);
            }
            return Insert(player);
        }

        private Player Insert(Player player)
        {
            const string sql = "INSERT INTO Player (PlayerId, PlayerName) "
                   + "VALUES (@PlayerId, @PlayerName); "
                   + "SELECT SCOPE_IDENTITY()";

            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                player.PlayerId = conn.Query<int>(sql, player).First();
            }
            return player;
        }

        private Player Update(Player player)
        {
            const string sql = "UPDATE Player SET "
                + "PlayerId = @PlayerId, "
                + "PlayerName = @PlayerName "
                + "WHERE PlayerId = @PlayerId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                if (conn.Execute(sql, player) > 0)
                {
                    return player;
                }
            }

            return null;
        }
    }
}
