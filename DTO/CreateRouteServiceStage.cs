namespace SETCBusAPI.DTO
{
    public class CreateRouteServiceStage
    {
        public string ServiceCode { get; set; } = string.Empty;
        public List<ServiceStages> ServiceStages { get; set; }
    }

    public class ServiceStages
    {
        public string StageCity { get; set; } = string.Empty;
        public string StageName { get; set; } = string.Empty;
        public string StageFlag { get; set; } = string.Empty;
        public string StageTime { get; set; } = string.Empty;
        public int StageOrder { get; set; }
        public int Distance { get; set; }
    }
}
