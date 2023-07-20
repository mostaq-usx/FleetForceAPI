using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace FleetForceAPI.Models
{
    public class Driver
    {
        [BsonRepresentation(BsonType.String)]
        [BsonElement("id")]
        //[Required]
        public string Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        //[BsonElement("loadDate")]
        //public DateTime LoadDate { get; set; }
        //[BsonElement("customer")]
        //public string Customer { get; set; }
        //[BsonElement("load")]
        //public int Load { get; set; }
    }
}
