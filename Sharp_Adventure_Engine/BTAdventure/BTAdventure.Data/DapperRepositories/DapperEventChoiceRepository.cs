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
    public class DapperEventChoiceRepository : IEventChoiceRepository
    {
        private const string CONN_STRING_KEY = "BinaryTextAdventure";

        public IEnumerable<EventChoice> All()
        {
            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                return conn.Query<EventChoice>("SELECT EventChoiceId, SceneId, GenerationNumber, ImgUrl, EventName, StartText, PositiveText, NegativeText, PositiveRoute, NegativeRoute, PositiveButton, NegativeButton, PositiveSceneRoute, NegativeSceneRoute, PositiveEndingId, NegativeEndingId FROM EventChoice;");
            }
        }

        public bool Delete(int id)
        {
            const string sql = "DELETE FROM EventChoice WHERE EventChoiceId = @EventChoiceId";
            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                return conn.Execute(sql, new { EventChoiceId = id }) > 0;
            }
        }

        public EventChoice FindById(int? id)
        {
            const string sql = "SELECT EventChoiceId, SceneId, GenerationNumber, ImgUrl, EventName, StartText, PositiveText, NegativeText, PositiveRoute, NegativeRoute, PositiveButton, NegativeButton, PositiveSceneRoute, NegativeSceneRoute, PositiveEndingId, NegativeEndingId "
                   + "FROM EventChoice "
                   + "WHERE EventChoiceId = @EventChoiceId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                return conn.Query<EventChoice>(sql, new { EventChoiceId = id })
                    .FirstOrDefault();
            }
        }

        public EventChoice Save(EventChoice eventChoice)
        {
            if (eventChoice.EventChoiceId > 0)
            {
                return Update(eventChoice);
            }
            return Insert(eventChoice);
        }

        private EventChoice Insert(EventChoice eventChoice)
        {
            const string sql = "INSERT INTO EventChoice (SceneId, GenerationNumber, ImgUrl, EventName, StartText, PositiveText, NegativeText, PositiveRoute, NegativeRoute, PositiveButton, NegativeButton, PositiveSceneRoute, NegativeSceneRoute, PositiveEndingId, NegativeEndingId) "
                   + "VALUES (@SceneId, @GenerationNumber, @ImgUrl, @EventName, @StartText, @PositiveText, @NegativeText, @PositiveRoute, @NegativeRoute, @PositiveButton, @NegativeButton, @PositiveSceneRoute, @NegativeSceneRoute, @PositiveEndingId, @NegativeEndingId); "
                   + "SELECT SCOPE_IDENTITY()";

            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                eventChoice.EventChoiceId = conn.Query<int>(sql, eventChoice).First();
            }
            return eventChoice;
        }

        private EventChoice Update(EventChoice eventChoice)
        {
            const string sql = "UPDATE EventChoice SET "
                + "SceneId = @SceneId, "
                + "GenerationNumber = @GenerationNumber, "
                + "ImgUrl = @ImgUrl, "
                + "EventName = @EventName, "
                + "StartText = @StartText, "
                + "PositiveText = @PositiveText, "
                + "NegativeText = @NegativeText, "
                + "PositiveRoute = @PositiveRoute, "
                + "NegativeRoute = @NegativeRoute, "
                + "PositiveButton = @PositiveButton, "
                + "NegativeButton = @NegativeButton, "
                + "PositiveSceneRoute = @PositiveSceneRoute, "
                + "NegativeSceneRoute = @NegativeSceneRoute, "
                + "PositiveEndingId = @PositiveEndingId, "
                + "NegativeEndingId = @NegativeEndingId "
                + "WHERE EventChoiceId = @EventChoiceId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                if (conn.Execute(sql, eventChoice) > 0)
                {
                    return eventChoice;
                }
            }

            return null;
        }

        public void UpdateEndingIdPos(int eventChoiceId)
        {
            const string sql = "UPDATE EventChoice SET "
                + "PositiveEndingId = NULL "
                + "WHERE EventChoiceId = @EventChoiceId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                conn.Query<EventChoice>(sql, new { EventChoiceId = eventChoiceId});
            }
        }

        public void UpdateEndingIdNeg(int eventChoiceId)
        {
            const string sql = "UPDATE EventChoice SET "
                + "NegativeEndingId = NULL "
                + "WHERE EventChoiceId = @EventChoiceId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                conn.Query<EventChoice>(sql, new { EventChoiceId = eventChoiceId });
            }
        }

        public IEnumerable<EventChoice> FindBySceneId(int? id)
        {
            const string sql = "SELECT EventChoiceId, SceneId, GenerationNumber, ImgUrl, EventName, StartText, PositiveText, NegativeText, PositiveRoute, NegativeRoute, PositiveButton, NegativeButton, PositiveSceneRoute, NegativeSceneRoute, PositiveEndingId, NegativeEndingId "
                   + "FROM EventChoice "
                   + "WHERE SceneId = @SceneId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                return conn.Query<EventChoice>(sql, new { SceneId = id });
            }
        }

        
    }
}
