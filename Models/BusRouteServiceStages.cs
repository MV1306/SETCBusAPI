using System.ComponentModel.DataAnnotations;

namespace SETCBusAPI.Models
{
    public class BusRouteServiceStages
    {
        [Key]
        public Guid StageID { get; set; }
        public Guid ServiceID { get; set; }
        public string StageCity { get; set; } = string.Empty;
        public string StageName { get; set; } = string.Empty;
        public string StageFlag {  get; set; } = string.Empty;
        public string StageTime { get; set; } = string.Empty;
        public int StageOrder { get; set; }
        public int Distance { get; set; }
        public bool IsActive { get; set; }
    }
}
