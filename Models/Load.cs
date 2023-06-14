using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FleetForceAPI.Models
{
    public class Load
    {
        [BsonRepresentation(BsonType.String)]
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("number")]
        public string Number { get; set; }

        [BsonElement("customer")]
        public string Customer { get; set; }

        [BsonElement("loadType")]
        public string LoadType { get; set; }

        [BsonElement("stops")]
        public string Stops { get; set; }
    }
}
