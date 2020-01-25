using ACBC.Buss;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACBC.Common
{
    public class MangoDBHelp
    {
        public int getTotalPrice()
        {
            var client = new MongoClient(Global.MangoDB);
            var database = client.GetDatabase("dolldb");
            var collection = database.GetCollection<BsonDocument>("v_total_allrec_money");
            var document = collection.Find(new BsonDocument()).FirstOrDefault();
            int price = 0;
            int.TryParse(document["sumPrice"].ToString(), out price);
            return price;
        }
        public List<LeaderBoard> GetLeaderBoardListAll()
        {
            var client = new MongoClient(Global.MangoDB);
            var database = client.GetDatabase("dolldb");
            var collection = database.GetCollection<BsonDocument>("v_max_allrec_money");
            var documents = collection.Find(new BsonDocument()).ToList();

            List<LeaderBoard> list = new List<LeaderBoard>();
            var cursor = collection.Find(new BsonDocument()).ToCursor();
            foreach (var document in cursor.ToEnumerable())
            {
                LeaderBoard al = new LeaderBoard();

                //al.id = Convert.ToInt32(dr[0]);
                string name = Utils.FilterChar(document["_id"].ToString());
                if (name.Length >= 7)
                {
                    al.nickname = name.Substring(0, 5) + "...";
                }
                else
                {
                    al.nickname = name;
                }
                al.money = document["sumPrice"].ToString();
                list.Add(al);
            }
            return list;
        }

    }
}
