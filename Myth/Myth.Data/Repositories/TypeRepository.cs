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
    public class TypeRepository : ITypeRepository
    {
        private const string CONN_STRING = "DefaultConnection";

        public IEnumerable<CreatureType> All()
        {
            const string sql = "SELECT TypeId, TypeName, Species, TypeDescription, FootprintType FROM CreatureType;";
            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Query<CreatureType>(sql);
            }
        }

        public CreatureType FindByCreatureId(int id)
        {
            const string sql = "SELECT t.TypeId, t.TypeName, t.Species, t.TypeDescription, t.FootprintType FROM CreatureType t " +
                "INNER JOIN Creature c ON c.TypeId = t.TypeId " +
                "WHERE c.CreatureId = @CreatureId;";
            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Query<CreatureType>(sql, new { CreatureId = id }).FirstOrDefault();
            }

        }

        public CreatureType FindById(int id)
        {
            const string sql = "SELECT TypeId, TypeName, Species, TypeDescription, FootprintType FROM CreatureType " +
                "WHERE TypeId = @TypeId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Query<CreatureType>(sql, new { TypeId = id }).FirstOrDefault();
            }
        }

        public bool Delete(int id) //REMOVE?
        {
            const string sql = "DELETE FROM CreatureType WHERE TypeId = @TypeId";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Execute(sql, new { TypeId = id }) > 0;
            }
        }

        public CreatureType Save(CreatureType type)
        {
            if (type.TypeId > 0)
            {
                return Update(type);
            }
            else
            {
                return Insert(type);
            }
        }

        private CreatureType Insert(CreatureType creatureType)
        {
            const string sql = "INSERT INTO CreatureType (TypeId, TypeName, Species, TypeDescription, FootprintType) " +
                "VALUES (@TypeId, @TypeName, @Species, @TypeDescription, @FootprintType);" +
                "SELECT SCOPE_IDENTITY()";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                creatureType.TypeId = conn.Query<int>(sql, creatureType).First();
            }
            return creatureType;
        }

        private CreatureType Update(CreatureType type)
        {
            const string sql = "UPDATE CreatureType SET " +
                "TypeId = @TypeId, " +
                "TypeName = @TypeName, " +
                "Species = @Species, " +
                "TypeDescription = @TypeDescription," +
                "FootprintType = @FootprintType;";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                if (conn.Execute(sql, type) > 0)
                {
                    return type;
                }
            }
            return null;
        }
    }
}
