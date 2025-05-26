using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SETCBusAPI.Data;
using SETCBusAPI.DTO;
using SETCBusAPI.Methods;
using SETCBusAPI.Models;
using System.Text.Json;

namespace SETCBusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FetchController : ControllerBase
    {
        private readonly SETCDbContext _context;
        private readonly CommonServices _services;

        public FetchController(SETCDbContext context, CommonServices services)
        {
            _context = context;
            _services = services;
        }

        [HttpGet("/RouteDetails/{RouteCode}")]
        public async Task<CommonResult> GetRouteDetails(string RouteCode)
        {
            var currentUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}";

            var res = new CommonResult();
            try
            {
                var routeDtl = _context.BusRoutes.FirstOrDefault(x => x.RouteCode == RouteCode && x.IsActive == true);

                if (routeDtl == null)
                {
                    res.ResponseCode = "404";
                    res.ResponseMessage = "Route not found.";
                    _services.saveAPILog(currentUrl, "Fetch", "GetRouteDetails", res.ResponseCode, res.ResponseMessage, JsonSerializer.Serialize(res));
                    return res;
                }

                var routeServiceDtl = _context.BusRouteServices
                    .Where(x => x.RouteID == routeDtl.RouteID && x.IsActive == true)
                    .OrderBy(x => x.DepartureTime)
                    .ToList();

                // Constructing a list of services with their corresponding stages
                var serviceWithStages = routeServiceDtl.Select(service => new
                {
                    Service = service,
                    Stages = _context.BusRouteServiceStages
                                .Where(x => x.ServiceID == service.ServiceID && x.IsActive == true)
                                .OrderBy(x => x.StageOrder)
                                .ToList()
                }).ToList();

                var finalRes = new
                {
                    RouteDtl = routeDtl,
                    RouteServices = serviceWithStages
                };

                res.ResponseCode = "200";
                res.ResponseMessage = "Success";
                res.ResultSet = finalRes;
            }
            catch (Exception ex)
            {
                res.ResponseCode = "503";
                res.ResponseMessage = "Unable to process your request. Please try again later. " + ex.Message;
                Console.WriteLine(ex.Message);
            }

            _services.saveAPILog(currentUrl, "Fetch", "GetRouteDetails", res.ResponseCode, res.ResponseMessage, JsonSerializer.Serialize(res));
            return res;
        }
    }
}
