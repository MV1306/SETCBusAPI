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
                Console.Write(ex.ToString());
            }

            return res;
        }

        [HttpPost("/CreateRouteService")]
        public async Task<CommonResult> CreateRouteService([FromBody] CreateRouteService service)
        {
            var res = new CommonResult();
            try
            {
                var existservice = _context.BusRouteServices.Where(x => x.ServiceCode == service.ServiceCode && x.IsActive == true).FirstOrDefault();

                if (existservice == null)
                {
                    var routeId = _context.BusRoutes.Where(x => x.RouteCode == service.RouteCode).Select(x => x.RouteID).FirstOrDefault();

                    var newservice = new BusRouteService
                    {
                        ServiceID = Guid.NewGuid(),
                        RouteID = routeId,
                        ServiceCode = service.ServiceCode,
                        From = service.From,
                        To = service.To,
                        DepartureTime = service.DepartureTime,
                        ArrivalTime = service.ArrivalTime,
                        IsActive = true
                    };

                    _context.BusRouteServices.Add(newservice);

                    var sucess = await _context.SaveChangesAsync() > 0;
                    if (sucess)
                    {
                        res.ResponseCode = "200";
                        res.ResponseMessage = "Route Service created successfully";
                    }
                    else
                    {
                        res.ResponseCode = "205";
                        res.ResponseMessage = "Unable to create route service. Try again later";
                    }
                }
                else
                {
                    res.ResponseCode = "205";
                    res.ResponseMessage = "Route Service already exists";
                }
            }
            catch (Exception ex) 
            {
                res.ResponseCode = "503";
                res.ResponseMessage = "Unable to process your request. Please try again later";
                Console.Write(ex.ToString());
            }          
            
            return res;
        }
     }
}
