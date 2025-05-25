using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SETCBusAPI.Data;
using SETCBusAPI.DTO;
using SETCBusAPI.Models;

namespace SETCBusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly SETCDbContext _context;

        public RouteController(SETCDbContext context) 
        {
            _context = context;
        }

        [HttpPost("/CreateRoute")]
        public async Task<CommonResult> CreateRoute([FromBody] CreateRoute route)
        {
            var res = new CommonResult();
            try
            {
                var existRoute = _context.BusRoutes.Where(x => x.RouteCode == route.RouteCode && x.IsActive == true).FirstOrDefault();

                if (existRoute == null)
                {
                    var newRoute = new BusRoute
                    {
                        RouteID = Guid.NewGuid(),
                        RouteCode = route.RouteCode,
                        //ServiceCode = route.ServiceCode,
                        Depot = route.Depot,
                        Origin = route.Origin,
                        Destination = route.Destination,
                        ServiceType = route.ServiceType,
                        Distance = route.Distance,
                        IsActive = true
                    };

                    _context.BusRoutes.Add(newRoute);

                    var sucess = await _context.SaveChangesAsync() > 0;
                    if (sucess)
                    {
                        res.ResponseCode = "200";
                        res.ResponseMessage = "Route created successfully";
                    }
                    else
                    {
                        res.ResponseCode = "205";
                        res.ResponseMessage = "Unable to create route. Try again later";
                    }
                }
                else
                {
                    res.ResponseCode = "205";
                    res.ResponseMessage = "Route already exists";
                }
            }
            catch(Exception ex)
            {
                res.ResponseCode = "503";
                res.ResponseMessage = "Unable to process your request. Please try again later";
            }

            return res;
        }
    }
}
