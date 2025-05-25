using System.ComponentModel.DataAnnotations;

namespace SETCBusAPI.Models
{
    public class BusRoute
    {
        [Key]
        public Guid RouteID { get; set; }
        public string RouteCode { get; set; } = string.Empty;
        public string Depot { get; set; } = string.Empty;
        public string Origin { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public string ServiceType { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
