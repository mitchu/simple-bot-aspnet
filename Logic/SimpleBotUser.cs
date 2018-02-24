using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
namespace SimpleBot
{
    public class SimpleBotUser
    {
        static Dictionary<string, UserProfile> _perfil = new Dictionary<string, UserProfile>();
        static Repo.IUserRepo _UserRepo = new UserSqlRepo(System.Configuration.ConfigurationManager.AppSettings["conexao"]);
        public static string Reply(Message message)
        {
            MongoClient Cliente = new MongoClient();
            var db = Cliente.GetDatabase("teste");
            var col = db.GetCollection<BsonDocument>("testeTB");
            if (message.Text.ToUpper().StartsWith("Find(".ToUpper()))
            {
                var filtro = Builders<BsonDocument>.Filter.Eq("mesagem",message.Text.Substring(5,message.Text.Length-6));
                //var filtro = Builders<BsonDocument>.Filter.Text("mesagem", message.Text.Substring(5));
                var res = col.Find(filtro).ToList();
                if (res.Count > 0)
                {
                    return $"{message.User} disse achei: '{ res[0][3].ToString()}'";
                }
                else
                {
                    return $"{message.User} disse '{ "não encontrei"}'";
                }
                
            }
            else
            {
                string userId = message.Id;
                var perfil = GetProfile(userId);
                perfil.Visitas += 1;
                SetProfile(userId, perfil);



                _UserRepo.SalvarHistorico(message);
                return $"{message.User} disse '{message.Text}'";
            }            
            
            
        }

        public static UserProfile GetProfile(string id)
        {
            if (_perfil.ContainsKey(id))
                return _perfil[id];

            return new UserProfile
            {
                Id = id,
                Visitas = 0
            };
        }

        public static void SetProfile(string id, UserProfile profile)
        {
            _perfil[id] = profile;
        }
    }
}