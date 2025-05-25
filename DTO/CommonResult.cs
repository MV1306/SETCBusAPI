namespace SETCBusAPI.DTO
{
    public class CommonResult
    {
        public string ResponseCode { get; set; } = string.Empty;
        public string ResponseMessage { get; set; } = string.Empty;
        public dynamic? ResultSet { get; set; }
    }
}
