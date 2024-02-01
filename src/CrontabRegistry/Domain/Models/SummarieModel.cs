using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CrontabRegistry.Domain.Models
{
    public class SummarieModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string? Id { get; set; }

        [BsonElement("name")]
        public string? Name { get; set; }
    }
}
