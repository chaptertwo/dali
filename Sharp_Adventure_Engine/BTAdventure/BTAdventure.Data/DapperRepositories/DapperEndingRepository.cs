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
    public class DapperEndingRepository : IEndingRepository
    {
        private const string CONN_STRING_KEY = "BinaryTextAdventure";

        public IEnumerable<Ending> All()
        {
            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                return conn.Query<Ending>("SELECT EndingId, GameId, EndingName, EndingText FROM Ending;");
            }
        }

        public bool Delete(int id)
        {
            const string sql = "DELETE FROM Ending WHERE EndingId = @EndingId";
            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                return conn.Execute(sql, new { EndingId = id }) > 0;
            }
        }

        public Ending FindById(int? id)
        {
            const string sql = "SELECT EndingId, GameId, EndingName, EndingText "
                   + "FROM Ending "
                   + "WHERE EndingId = @EndingId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                return conn.Query<Ending>(sql, new { EndingId = id })
                    .FirstOrDefault();
            }
        }

        public IEnumerable<Ending> FindEndingsByGameId(int id)
        {
            const string sql = "SELECT EndingId, GameId, EndingName, EndingText "
                   + "FROM Ending "
                   + "WHERE GameId = @GameId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                return conn.Query<Ending>(sql, new { GameId = id });
            }
        }

        public Ending Save(Ending ending)
        {
            if (ending.EndingId > 0)
            {
                return Update(ending);
            }
            return Insert(ending);
        }

        private Ending Insert(Ending ending)
        {
            const string sql = "INSERT INTO Ending (GameId, EndingName, EndingText) "
                   + "VALUES (@GameId, @EndingName, @EndingText); "
                   + "SELECT SCOPE_IDENTITY()";

            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                ending.EndingId = conn.Query<int>(sql, ending).First();
            }
            return ending;
        }

        private Ending Update(Ending ending)
        {
            const string sql = "UPDATE Ending SET "
                + "GameId = @GameId, "
                + "EndingName = @EndingName, "
                + "EndingText = @EndingText "
                + "WHERE EndingId = @EndingId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                if (conn.Execute(sql, ending) > 0)
                {
                    return ending;
                }
            }

            return null;
        }
    }
}
