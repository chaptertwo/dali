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
    public class NestRepository : INestRepository
    {
        private const string CONN_STRING = "DefaultConnection";

        public IEnumerable<Nest> All()
        {
            const string sql = "SELECT NestId, RegionId, NestLat, NestLong, NestName, IsPlaced FROM Nest;";
            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Query<Nest>(sql);
            }
        }

        public Nest FindById(int id)
        {
            const string sql = "SELECT NestId, RegionId, NestLat, NestLong, NestName FROM Nest " +
                "WHERE NestId = @NestId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Query<Nest>(sql, new { NestId = id }).FirstOrDefault();
            }
        }

        public Nest FindByCreatureId(int id)
        {
            const string sql = "SELECT t.NestId, t.NestName, t.NestLat, t.NestLong FROM Nest t " +
                "INNER JOIN Creature c ON c.NestId = t.NestId " +
                "WHERE c.CreatureId = @CreatureId;";
            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Query<Nest>(sql, new { CreatureId = id }).FirstOrDefault();
            }

        }

        public bool Delete(int creatureId, int id)
        {
            const string sql = "UPDATE Creature SET NestId = NULL, CreatureHasNest = 0 " +
                "WHERE CreatureId = @CreatureId; " +
                "DELETE FROM Nest WHERE NestId = @NestId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Execute(sql, new { NestId = id, CreatureId = creatureId }) > 0;
            }
        }

        public Nest Save(Nest nest)
        {
            if (nest.NestId > 0)
            {
                return Update(nest);
            }
            else
            {
                return Insert(nest);
            }
        }

        private Nest Insert(Nest nest)
        {
            const string sql = "INSERT INTO Nest (NestLat, NestLong, NestName, IsPlaced) " +
                "VALUES (@NestLat, @NestLong, @NestName, @IsPlaced);" +
                "SELECT SCOPE_IDENTITY()";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                nest.NestId = conn.Query<int>(sql, nest).First();
            }
            return nest;
        }

        private Nest Update(Nest nest)
        {
            const string sql = "UPDATE Nest SET " +
                "NestLat = @NestLat, " +
                "NestLong = @NestLong, " +
                "NestName = @NestName, " +
                "IsPlaced = 0 " +
                "WHERE NestId = @NestId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                if (conn.Execute(sql, nest) > 0)
                {
                    return nest;
                }
            }
            return null;
        }
    }
}
