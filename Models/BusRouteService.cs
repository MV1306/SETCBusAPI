using System.ComponentModel.DataAnnotations;

namespace SETCBusAPI.Models
{
    public class BusRouteService
    {
        [Key]
        public Guid ServiceID { get; set; }
        public Guid RouteID { get; set; }
        public string ServiceCode { get; set; } = string.Empty;
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public string DepartureTime { get;set; } = string.Empty;
        public string ArrivalTime { get;set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
