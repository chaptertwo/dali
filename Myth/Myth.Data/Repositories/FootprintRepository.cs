using Dapper;
using Myth.Domain.Interfaces;
using Myth.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myth.Data.Repositories
{
    public class FootprintRepository : IFootprintRepository
    {

        private const string CONN_STRING = "DefaultConnection";

        public IEnumerable<Footprint> All()
        {
            const string sql = "SELECT FootprintId, CreatureId, FootprintLat, FootprintLong, FootprintDate FROM Footprint;";
            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Query<Footprint>(sql);
            }
        }

        public Footprint FindById(int id)
        {
            const string sql = "SELECT FootprintId, CreatureId, FootprintLat, FootprintLong,  FootprintDate FROM Footprint " +
                "WHERE FootprintId = @FootprintId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Query<Footprint>(sql, new { FootprintId = id }).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            const string sql = "DELETE FROM Footprint WHERE FootprintId = @FootprintId";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Execute(sql, new { FootprintId = id }) > 0;
            }
        }

        public Footprint Generate(Footprint footprint)
        {
            const string sql = "INSERT INTO Footprint (CreatureId, FootprintLat, FootprintLong, FootPrintDate) " +
                "VALUES (@CreatureId, @FootprintLat, @FootprintLong, @FootprintDate);" +
                "SELECT SCOPE_IDENTITY()";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                footprint.FootprintId = conn.Query<int>(sql, footprint).First();
            }
            return footprint;
        }

        public Footprint Save(Footprint footprint)
        {
            if (footprint.FootprintId > 0)
            {
                return Update(footprint);
            }
            else
            {
                return Insert(footprint);
            }
        }

        private Footprint Insert(Footprint footprint)
        {
            const string sql = "INSERT INTO Footprint (FootprintId, CreatureId, FootprintLat, FootprintLong, FootprintDate) " +
                "VALUES (@FootprintId, @CreatureId, @FootprintLat, @FootprintLong,  @FootprintDate);" +
                "SELECT SCOPE_IDENTITY()";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                footprint.FootprintId = conn.Query<int>(sql, footprint).First();
            }
            return footprint;
        }

        private Footprint Update(Footprint footprint)
        {
            const string sql = "UPDATE Footprint SET " +
                "FootprintId = @FootprintId, " +
                "CreatureId = @CreatureId, " +
                "FootprintLat = @FootprintLat, " +
                "FootprintLong = @FootprintLong, " +
                "FootprintDate = @FootprintDate;";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                if (conn.Execute(sql, footprint) > 0)
                {
                    return footprint;
                }
            }
            return null;
        }

        public IEnumerable<Footprint> GetFootprintsByCreature(Creature creature)
        {
            const string sql = "SELECT c.CreatureId, f.FootprintId, f.FootprintDate, f.FootprintIsRevealed, f.FootprintLat, f.FootprintLong, " +
                "c.CreatureName, c.CreatureIsRevealed, c.CreatureLat, c.CreatureLong FROM Footprint f " +
                "FULL OUTER JOIN Creature c ON c.CreatureId = f.CreatureId " +
                "WHERE c.CreatureId = @CreatureId";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Query<Footprint>(sql, new { CreatureId = creature.CreatureId });
            }
        }
    }
}
