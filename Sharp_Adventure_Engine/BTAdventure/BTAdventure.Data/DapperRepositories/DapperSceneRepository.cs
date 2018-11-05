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
    public class DapperSceneRepository : ISceneRepository
    {
        private const string CONN_STRING_KEY = "BinaryTextAdventure";

        public IEnumerable<Scene> All()
        {
            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                return conn.Query<Scene>("SELECT SceneId, GameId, IsStart, SceneName FROM Scene;");
            }
        }

        public bool Delete(int id)
        {
            const string sql = "DELETE FROM Scene WHERE SceneId = @SceneId";
            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                return conn.Execute(sql, new { SceneId = id }) > 0;
            }
        }

        public Scene FindById(int id)
        {
            const string sql = "SELECT SceneId, GameId, IsStart, SceneName "
                   + "FROM Scene "
                   + "WHERE SceneId = @SceneId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                return conn.Query<Scene>(sql, new { SceneId = id })
                    .FirstOrDefault();
            }
        }

        public Scene FindById(int? id)
        {
            const string sql = "SELECT SceneId, GameId, IsStart, SceneName "
                   + "FROM Scene "
                   + "WHERE SceneId = @SceneId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                return conn.Query<Scene>(sql, new { SceneId = id })
                    .FirstOrDefault();
            }
        }

        public IEnumerable<Scene> FindByGameId(int id)
        {
            const string sql = "SELECT SceneId, GameId, IsStart, SceneName "
                   + "FROM Scene "
                   + "WHERE GameId = @GameId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                return conn.Query<Scene>(sql, new { GameId = id });
            }
        }

        public Scene FindSceneByCharacterId(int id)
        {
            throw new NotImplementedException();
        }

        public Scene Save(Scene scene)
        {
            if (scene.SceneId > 0)
            {
                return Update(scene);
            }
            return Insert(scene);
        }

        private Scene Insert(Scene scene)
        {
            const string sql = "INSERT INTO Scene (SceneName, isStart, GameId) "
                   + "VALUES (@SceneName, @isStart, @GameId); "
                   + "SELECT SCOPE_IDENTITY()";

            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                scene.SceneId = conn.Query<int>(sql, scene).First();
            }
            return scene;
        }

        private Scene Update(Scene scene)
        {
            const string sql = "UPDATE Scene SET "
                + "GameId = @GameId, "
                + "IsStart = @IsStart, "
                + "SceneName = @SceneName "
                + "WHERE SceneId = @SceneId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING_KEY))
            {
                if (conn.Execute(sql, scene) > 0)
                {
                    return scene;
                }
            }

            return null;
        }
    }
}
