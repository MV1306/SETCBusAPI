namespace SETCBusAPI.DTO
{
    public class CreateRouteService
    {
        public string RouteCode { get; set; } = string.Empty;
        public string ServiceCode { get; set; } = string.Empty;
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public string DepartureTime { get; set; } = string.Empty;
        public string ArrivalTime { get; set; } = string.Empty;
    }
}
