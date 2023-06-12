namespace FleetForceAPI.DTO
{
    public class BaseDTO
    {
        /// <summary>
        /// TraceId for tracking the API request
        /// </summary>
        public string TraceId { get; set; }

        /// <summary>
        /// Track the Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Track the Application Name
        /// </summary>
        public string ApplicationName { get; set; }
    }
}
