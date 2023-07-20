using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using FleetForceAPI.Models;

namespace FleetForceAPI.DTO
{
    public class TruckDTO : BaseDTO
    {
        //[Required]
        public string Id { get; set; }

        public string Number { get; set; }

        public string Driver { get; set; }

        public bool Availability { get; set; }

        public List<Load> Loads { get; set; }


        //[BsonElement("coveringManager")]
        //public string CoveringManager { get; set; }
        //[BsonElement("managerNetworkName")]
        //public string ManagerNetworkName { get; set; }
        //[BsonElement("status")]
        //public string Status { get; set; }
        //[BsonElement("ignitionStatus")]
        //public string IgnitionStatus { get; set; }
        //[BsonElement("isTrailerSkirt")]
        //public bool IsTrailerSkirt { get; set; }
        //[BsonElement("isInShop")]
        //public string IsInShop { get; set; }
        //[BsonElement("truckInShopDescription")]
        //public string TruckInShopDescription { get; set; }
    }
}
