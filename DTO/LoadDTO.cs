namespace FleetForceAPI.DTO
{
    public class LoadDTO : BaseDTO
    {
        public string Id { get; set; }

        public string Number { get; set; }

        public string Customer { get; set; }

        public string LoadType { get; set; }

        public List<string> Stops { get; set; }
    }
}
