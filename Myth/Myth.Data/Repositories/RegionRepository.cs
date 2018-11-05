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
    public class RegionRepository : IRegionRepository
    {
        private const string CONN_STRING = "DefaultConnection";

        public IEnumerable<Region> All()
        {
            const string sql = "SELECT RegionId, CountryAbbr, CountryFull, RegionLat, RegionLong FROM Region;";
            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Query<Region>(sql);
            }
        }

        public Region FindById(int id)
        {
            const string sql = "SELECT RegionId, CountryAbbr, CountryFull, RegionLat, RegionLong FROM Region " +
                "WHERE RegionId = @RegionId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Query<Region>(sql, new { RegionId = id }).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            const string sql = "DELETE FROM Region WHERE RegionId = @RegionId";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Execute(sql, new { RegionId = id }) > 0;
            }
        }

        public Region Save(Region region)
        {
            if (region.RegionId > 0)
            {
                return Update(region);
            }
            else
            {
                return Insert(region);
            }
        }

        private Region Insert(Region region)
        {
            const string sql = "INSERT INTO Region (RegionId, CountryAbbr, CountryFull, RegionLat, RegionLong) " +
                "VALUES (@RegionId, @CountryAbbr, @CountryFull, @RegionLat, @RegionLong);" +
                "SELECT SCOPE_IDENTITY()";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                region.RegionId = conn.Query<int>(sql, region).First();
            }
            return region;
        }

        private Region Update(Region region)
        {
            const string sql = "UPDATE Region SET " +
                "RegionId = @RegionId, " +
                "CountryAbbr = @CountryAbbr, " +
                "CountryFull = @CountryFull, " +
                "RegionLat = @RegionLat, " +
                "RegionLong = @RegionLong;";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                if (conn.Execute(sql, region) > 0)
                {
                    return region;
                }
            }
            return null;
        }
    }
}
