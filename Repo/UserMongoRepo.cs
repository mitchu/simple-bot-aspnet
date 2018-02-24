using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBot.Repo
{
    public class UserMongoRepo : IUserRepo
    {
        public void SalvarHistorico(Message message)
        {
            var cliente = new MongoClient();
            var db = cliente.GetDatabase("bot");
            var col = db.GetCollection<BsonDocument>("historico");

            var doc = new BsonDocument
            {
                { "User", message.User },
                { "Text", message.Text }
            };

            col.InsertOne(doc);
        }
    }
}