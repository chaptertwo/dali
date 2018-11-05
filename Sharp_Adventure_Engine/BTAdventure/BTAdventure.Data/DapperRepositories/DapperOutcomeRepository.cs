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
    public class DapperOutcomeRepository : IOutcomeRepository
    {
        private const string CONN_STRING_KEY = "BinaryTextAdventure";

        public IEnumerable<Outcome> All()
        {
            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                return conn.Query<Outcome>("SELECT OutcomeId, EventChoiceId, Positive, Health, Gold FROM Outcome;");
            }
        }

        public bool Delete(int id)
        {
            const string sql = "DELETE FROM Outcome WHERE OutcomeId = @OutcomeId";
            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                return conn.Execute(sql, new { OutcomeId = id }) > 0;
            }
        }

        public Outcome FindById(int id)
        {
            const string sql = "SELECT OutcomeId, EventChoiceId, Positive, Health, Gold "
                   + "FROM Outcome "
                   + "WHERE OutcomeId = @OutcomeId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                return conn.Query<Outcome>(sql, new { OutcomeId = id }).FirstOrDefault();
            }
        }

        public Outcome FindByEventChoice(int id)
        {
            const string sql = "SELECT OutcomeId, EventChoiceId, Positive, Health, Gold "
                   + "FROM Outcome "
                   + "WHERE EventChoiceId = @EventChoiceId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                return conn.Query<Outcome>(sql, new { EventChoiceId = id }).FirstOrDefault();
            }
        }

        public Outcome Save(Outcome outcome)
        {
            if (outcome.OutcomeId > 0)
            {
                return Update(outcome);
            }
            return Insert(outcome);
        }

        private Outcome Insert(Outcome outcome)
        {
            const string sql = "INSERT INTO Outcome (EventChoiceId, Positive, Health, Gold) "
                   + "VALUES (@EventChoiceId, @Positive, @Health, @Gold); "
                   + "SELECT SCOPE_IDENTITY()";

            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                outcome.OutcomeId = conn.Query<int>(sql, outcome).First();
            }
            return outcome;
        }

        private Outcome Update(Outcome outcome)
        {
            const string sql = "UPDATE Outcome SET "
                + "EventChoiceId = @EventChoiceId, "
                + "Positive = @Positive, "
                + "Health = @Health, "
                + "Gold = @Gold "
                + "WHERE OutcomeId = @OutcomeId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                if (conn.Execute(sql, outcome) > 0)
                {
                    return outcome;
                }
            }

            return null;
        }

        public IEnumerable<Outcome> FindOutcomeByEventChoiceId(int id)
        {
            const string sql = "SELECT OutcomeId, Positive, Health, Gold "
                   + "FROM Outcome "
                   + "WHERE EventChoiceId = @EventChoiceId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                return conn.Query<Outcome>(sql, new { EventChoiceId = id });
            }
        }

        public Outcome CheckOutComeStatus(bool posOrNeg, int id)
        {
            const string sql = "SELECT OutcomeId, EventChoiceId, Positive, Health, Gold "
                   + "FROM Outcome "
                   + "WHERE Positive = @Positive AND EventChoiceId = @EventChoiceId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                return conn.Query<Outcome>(sql, new { Positive = posOrNeg, EventChoiceId = id }).FirstOrDefault();
            }
        }
    }
}
