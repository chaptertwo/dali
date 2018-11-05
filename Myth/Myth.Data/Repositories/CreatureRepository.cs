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
    public class CreatureRepository : ICreatureRepository
    {
        private const string CONN_STRING = "DefaultConnection";

        public IEnumerable<Creature> All()
        {
            const string sql = "SELECT CreatureId, CreatureName, NestId, TypeId, TraitId, Picture, CreatureLat, CreatureLong, CreatureDescription, CreatureIsRevealed, CreatureHasNest, CreatureIsPlaced FROM Creature;";
            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Query<Creature>(sql);
            }
        }

        public Creature FindById(int id)
        {
            const string sql = "SELECT CreatureId, CreatureName, NestId, TypeId, TraitId, Picture, CreatureLat, CreatureLong, CreatureDescription, CreatureIsRevealed, CreatureHasNest, CreatureIsPlaced FROM Creature " +
                "WHERE CreatureId = @CreatureId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Query<Creature>(sql, new { CreatureId = id }).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            const string sql = "DELETE FROM CreatureTrait WHERE CreatureId = @CreatureId; DELETE FROM Footprint WHERE CreatureId = @CreatureId; " +
                "DELETE FROM Creature WHERE CreatureId = @CreatureId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Execute(sql, new { CreatureId = id }) > 0;
            }
        }

        public Creature Save(Creature creature)
        {
            if(creature.CreatureId > 0)
            {
                return Update(creature);
            }
            else
            {
                return Insert(creature);
            }
        }

        private Creature Insert(Creature creature)
        {
            const string sql = "INSERT INTO Creature (CreatureName, NestId, TypeId, Picture, CreatureLat, CreatureLong, CreatureDescription, CreatureIsRevealed, CreatureHasNest, CreatureIsPlaced) " +
                "VALUES (@CreatureName, @NestId, @TypeId, @Picture, @CreatureLat, @CreatureLong, @CreatureDescription, @CreatureIsRevealed, @CreatureHasNest, @CreatureIsPlaced);" +
                "SELECT SCOPE_IDENTITY()";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                creature.CreatureId = conn.Query<int>(sql, creature).First();
            }
            return creature;
        }

        private Creature Update(Creature creature) //need to update creature table.. and possible insert into it if there are two...
        {
            const string sql = "UPDATE Creature SET " +
                "CreatureName = @CreatureName, " +
                "NestId = @NestId, " +
                "TypeId = @TypeId, " +
                "Picture = @Picture, " +
                "CreatureLat = @CreatureLat, " +
                "CreatureLong = @CreatureLong, " +
                "CreatureDescription = @CreatureDescription," +
                "CreatureHasNest = @CreatureHasNest, " +
                "CreatureIsPlaced = @CreatureIsPlaced " +
                "WHERE CreatureId = @CreatureId";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                conn.Execute(sql, creature);
            }
            return null;
        }

        public void UpdateCreatureNest(int creatureSelectedId, int nestId)
        {
            const string sql = "UPDATE Creature SET " +
                "NestId = @NestId, " +
                "CreatureHasNest = 1 " +
                "WHERE CreatureId = @CreatureId";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                conn.Execute(sql, new { CreatureId = creatureSelectedId, NestId = nestId });
            }
        }

        public void SaveFromMap(Creature creature)
        {
            const string sql = "UPDATE Creature SET " +
                "CreatureLat = @CreatureLat, " +
                "CreatureLong = @CreatureLong, " +
                "CreatureIsPlaced = @CreatureIsPlaced " +
                "WHERE CreatureId = @CreatureId";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                conn.Execute(sql, creature);
            }
        }
    }
}
