namespace SETCBusAPI.DTO
{
    public class CreateRoute
    {
        public string RouteCode { get; set; } = string.Empty;
        //public string ServiceCode { get; set; } = string.Empty;
        public string Depot { get; set; } = string.Empty;
        public string Origin { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public string ServiceType { get; set; } = string.Empty;
        public int Distance { get; set; }
    }
}
