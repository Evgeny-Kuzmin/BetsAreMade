using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System.Collections.Generic;

namespace BetsAreMade.DataContracts.Dbo.Users
{
    public abstract class BaseDbo
    {
        [BsonId]
        public ObjectId? _id { get; protected set; }
        public string Id => _id?.ToString();
        public void SetId(ObjectId id) 
        {
            this._id = id;
        }
    }
}
