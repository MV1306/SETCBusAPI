using SETCBusAPI.Data;
using SETCBusAPI.Models;

namespace SETCBusAPI.Methods
{
    public class CommonServices
    {
        private readonly SETCDbContext _context;

        public CommonServices(SETCDbContext context)
        {
            _context = context;
        }

        public async Task saveAPILog(string URL, string controller, string method, string errorCode, string errorMessage, string response)
        {
            var apiLog = new ApiLog
            {
                LogID = Guid.NewGuid(),
                URL = URL,
                Controller = controller,
                Method = method,
                ErrorCode = errorCode,
                ErrorMessage = errorMessage,
                Response = response,
                CreatedDt = DateTime.Now
            };

            _context.ApiLogs.Add(apiLog);

            await _context.SaveChangesAsync();
        }
    }
}
