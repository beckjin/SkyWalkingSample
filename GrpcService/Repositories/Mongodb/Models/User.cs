using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace GrpcService.Repositories.Mongodb.Models
{
    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        public ObjectId _id { get; set; }

        [BsonElement("uid")]
        public string UserId { get; set; }

        [BsonElement("name")]
        public string UserName { get; set; }
    }
}
