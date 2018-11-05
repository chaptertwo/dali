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
    public class TraitRepository : ITraitRepository
    {
        private const string CONN_STRING = "DefaultConnection";

        public IEnumerable<Trait> All()
        {
            const string sql = "SELECT TraitId, TraitName, TraitDescription FROM Trait;";
            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Query<Trait>(sql);
            }
        }

        public Trait FindById(int id)
        {
            const string sql = "SELECT TraitId, TraitName, TraitDescription FROM Trait " +
                "WHERE TraitId = @TraitId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Query<Trait>(sql, new { TraitId = id }).FirstOrDefault();
            }
        }

        public IEnumerable<Trait> FindManyByCreatureId(int id)
        {
            const string sql = "SELECT t.TraitId, t.TraitName, t.TraitDescription FROM Trait t " +
                "INNER JOIN CreatureTrait c ON c.TraitId = t.TraitId " +
                "WHERE c.CreatureId = @CreatureId;";
            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Query<Trait>(sql, new { CreatureId = id });
            }

        }

        public bool DeleteAllTraitsPerCreature(int creatureId)
        {
            const string sql = "DELETE FROM CreatureTrait WHERE CreatureId = @CreatureId;";
            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Execute(sql, new { CreatureId = creatureId }) > 0;
            }
        }

        public bool Delete(int id)
        {
            const string sql = "DELETE FROM Trait WHERE TraitId = @TraitId";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Execute(sql, new { TraitId = id }) > 0;
            }
        }

        public Trait Save(Trait trait)
        {
            if (trait.TraitId > 0)
            {
                return Update(trait);
            }
            else
            {
                return Insert(trait);
            }
        }

        private Trait Insert(Trait trait)
        {
            const string sql = "INSERT INTO Trait (TraitName, TraitDescription) " +
                "VALUES (@TraitName, @TraitDescription);" +
                "SELECT SCOPE_IDENTITY()";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                trait.TraitId = conn.Query<int>(sql, trait).First();
            }
            return trait;
        }

        private Trait Update(Trait trait)
        {
            const string sql = "UPDATE Trait SET " +
                "TraitId = @TraitId, " +
                "TraitName = @TraitName, " +
                "TraitDescription = @TraitDescription;";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                if (conn.Execute(sql, trait) > 0)
                {
                    return trait;
                }
            }
            return null;
        }


        public bool InsertByCreatureId(int traitId, int creatureId)
        {
            const string sql = "INSERT INTO CreatureTrait (TraitId, CreatureId) " +
                "VALUES (@TraitId, @CreatureId);" +
                "SELECT SCOPE_IDENTITY()";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Execute(sql, new { TraitId = traitId, CreatureId = creatureId }) > 0;
            }
            return false;
        }

        public bool UpdateByCreatureId(int traitId, int creatureId)
        {
            const string sql = "UPDATE CreatureTrait SET " +
                "TraitId = @TraitId " +
                "WHERE CreatureId = @CreatureId";
             

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Execute(sql, new { TraitId = traitId, CreatureId = creatureId }) > 0;
            }
            return false;
        }

        
    }
}
