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
    public class CreateController : ControllerBase
    {
        private readonly SETCDbContext _context;
        private readonly CommonServices _services;

        public CreateController(SETCDbContext context, CommonServices services)
        {
            _context = context;
            _services = services;
        }

        [HttpPost("/Route")]
        public async Task<CommonResult> CreateRoute([FromBody] CreateRoute route)
        {
            var currentUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}";
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
                res.ResponseMessage = "Unable to process your request. Please try again later. " + ex.Message.ToString();
                Console.Write(ex.Message.ToString());
            }

            _services.saveAPILog(currentUrl, "Create", "CreateRoute", res.ResponseCode, res.ResponseMessage, JsonSerializer.Serialize(res));
            return res;
        }

        [HttpPost("/RouteService")]
        public async Task<CommonResult> CreateRouteService([FromBody] CreateRouteService service)
        {
            var currentUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}";
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
                res.ResponseMessage = "Unable to process your request. Please try again later. " + ex.Message.ToString();
                Console.Write(ex.Message.ToString());
            }
            _services.saveAPILog(currentUrl, "Create", "CreateRouteService", res.ResponseCode, res.ResponseMessage, JsonSerializer.Serialize(res));
            
            return res;
        }

        [HttpPost("/RouteServiceStages")]
        public async Task<CommonResult> CreateRouteServiceStages([FromBody] CreateRouteServiceStage stages)
        {
            var currentUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}";
            var res = new CommonResult();

            try
            {
                var serviceID = _context.BusRouteServices.Where(x => x.ServiceCode == stages.ServiceCode && x.IsActive == true).Select(x => x.ServiceID).FirstOrDefault();

                var serviceStages = stages.ServiceStages;

                List<BusRouteServiceStages> stageList = new List<BusRouteServiceStages>();

                foreach(var stage in serviceStages)
                {
                    var existingStage = _context.BusRouteServiceStages.Where(x => x.ServiceID == serviceID && x.IsActive == true && x.StageCity.ToLower() == stage.StageCity.ToLower() && x.StageName.ToLower() == stage.StageName.ToLower()).FirstOrDefault();

                    if(existingStage == null)
                    {
                        var newStage = new BusRouteServiceStages
                        {
                            StageID = Guid.NewGuid(),
                            ServiceID = serviceID,
                            StageCity = stage.StageCity,
                            StageName = stage.StageName,
                            StageFlag = stage.StageFlag,
                            StageTime = stage.StageTime,
                            StageOrder = stage.StageOrder,
                            Distance = stage.Distance,
                            IsActive = true
                        };

                        stageList.Add(newStage);

                        newStage = null;
                    }
                    else
                    {
                        res.ResponseCode = "205";
                        res.ResponseMessage = "Service already contains the stage " + stage.StageName + " in " + stage.StageCity;
                        return res;
                    }
                }

                _context.BusRouteServiceStages.AddRange(stageList);

                var sucess = await _context.SaveChangesAsync() > 0;
                if (sucess)
                {
                    res.ResponseCode = "200";
                    res.ResponseMessage = "Route Service Stages created successfully";
                }
                else
                {
                    res.ResponseCode = "205";
                    res.ResponseMessage = "Unable to create route service stages. Try again later";
                }

            }
            catch(Exception ex)
            {
                res.ResponseCode = "503";
                res.ResponseMessage = "Unable to process your request. Please try again later. " + ex.Message.ToString();
                Console.Write(ex.Message.ToString());
            }
            _services.saveAPILog(currentUrl, "Create", "CreateRouteServiceStages", res.ResponseCode, res.ResponseMessage, JsonSerializer.Serialize(res));

            return res;
        }
     }
}
