namespace FleetForceAPI.Models
{
    public class ApiResponse<T>
    {
        public string TraceId { get; set; }
        public bool IsSuccessful { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
