using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace BetsAreMade.DataContracts.Dbo.Users
{
    public class BetDbo : BaseDbo
    {
        public string Team { get; set; }
    }
}
