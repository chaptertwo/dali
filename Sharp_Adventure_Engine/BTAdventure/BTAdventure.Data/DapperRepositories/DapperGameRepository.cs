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
    public class DapperGameRepository : IGameRepository
    {
        private const string CONN_STRING_KEY = "BinaryTextAdventure";

        public IEnumerable<Game> All()
        {
            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                return conn.Query<Game>("SELECT GameId, GameTitle, IntroText, Health, Gold FROM Game;");
            }
        }

        public bool Delete(int id)
        {
            const string sql = "DELETE FROM Game WHERE GameId = @GameId";
            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                return conn.Execute(sql, new { GameId = id }) > 0;
            }
        }

        public Game FindById(int id)
        {
            const string sql = "SELECT GameId, GameTitle, IntroText, Health, Gold "
                   + "FROM Game "
                   + "WHERE GameId = @GameId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                return conn.Query<Game>(sql, new { GameId = id })
                    .FirstOrDefault();
            }
        }

        public Game Save(Game game)
        {
            if (game.GameId > 0)
            {
                return Update(game);
            }
            return Insert(game);
        }

        private Game Insert(Game game)
        {
            const string sql = "INSERT INTO Game (GameTitle, IntroText, Health, Gold) "
                   + "VALUES (@GameTitle, @IntroText, @Health, @Gold); "
                   + "SELECT SCOPE_IDENTITY()";

            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                game.GameId = conn.Query<int>(sql, game).First();
            }
            return game;
        }

        private Game Update(Game game)
        {
            const string sql = "UPDATE Game SET "
                + "GameTitle = @GameTitle, "
                + "IntroText = @IntroText, "
                + "Health = @Health, "
                + "Gold = @Gold "
                + "WHERE GameId = @GameId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                if (conn.Execute(sql, game) > 0)
                {
                    return game;
                }
            }

            return null;
        }
    }
}
