using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FleetForceAPI.DTO
{
    public class TruckDTO
    {
        [BsonRepresentation(BsonType.String)]
        [BsonElement("id")]
        //[Required]
        public string Id { get; set; }
        [BsonElement("number")]
        public string Number { get; set; }
        [BsonElement("company")]
        public string Company { get; set; }
        [BsonElement("assetOwner")]
        public string AssetOwner { get; set; }
        [BsonElement("as400CoverageName")]
        public string As400CoverageName { get; set; }
        [BsonElement("coveringManager")]
        public string CoveringManager { get; set; }
        [BsonElement("managerNetworkName")]
        public string ManagerNetworkName { get; set; }
        [BsonElement("status")]
        public string Status { get; set; }
        [BsonElement("ignitionStatus")]
        public string IgnitionStatus { get; set; }
        [BsonElement("isTrailerSkirt")]
        public bool IsTrailerSkirt { get; set; }
        [BsonElement("isInShop")]
        public string IsInShop { get; set; }
        [BsonElement("truckInShopDescription")]
        public string TruckInShopDescription { get; set; }
    }
}
