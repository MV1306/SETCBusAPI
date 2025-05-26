using System.ComponentModel.DataAnnotations;

namespace SETCBusAPI.Models
{
    public class ApiLog
    {
        [Key]
        public Guid LogID { get; set; }
        public string URL { get; set; }
        public string Controller {  get; set; } = string.Empty;
        public string Method {  get; set; } = string.Empty;
        public string ErrorCode {  get; set; } = string.Empty;
        public string ErrorMessage {  get; set; } = string.Empty;
        public string Response { get; set; } = string.Empty;
        public DateTime CreatedDt { get; set; }
    }
}
