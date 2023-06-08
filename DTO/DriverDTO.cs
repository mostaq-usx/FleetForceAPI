using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace FleetForceAPI.DTO
{
    public class DriverDTO
    {
        [BsonRepresentation(BsonType.String)]
        [BsonElement("id")]
        public string Id { get; set; }
        [BsonElement("name")]
        //[Required]
        public string Name { get; set; }
        [BsonElement("loadDate")]
        public DateTime LoadDate { get; set; }
        [BsonElement("customer")]
        public string Customer { get; set; }
        [BsonElement("load")]
        public int Load { get; set; }
    }
}
