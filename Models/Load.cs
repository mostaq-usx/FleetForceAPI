using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FleetForceAPI.Models
{
    public class Load
    {
        public string Id { get; set; }

        public string Number { get; set; }

        public string Customer { get; set; }

        public string LoadType { get; set; }

        public List<string> Stops { get; set; }
    }
}
