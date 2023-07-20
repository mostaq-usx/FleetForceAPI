using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace FleetForceAPI.DTO
{
    public class DriverDTO : BaseDTO 
    {
        public string Id { get; set; }

        public string Name { get; set; }
       
        //public DateTime LoadDate { get; set; }
        
        //public string Customer { get; set; }

        //public int Load { get; set; }
    }
}
