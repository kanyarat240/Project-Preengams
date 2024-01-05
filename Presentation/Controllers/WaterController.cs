using Application.Features.Room;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class WaterController : BaseController
    {
        [AllowAnonymous]
        [HttpGet("getWater")]
        public Task<List<WaterDTO>> getWater() => Mediator.Send(new GetWater.Query());

        [AllowAnonymous]
        [HttpGet("getElectricity")]
        public Task<List<ElectricityDTO>> getElectricity() => Mediator.Send(new GetElectricity.Query());






    }
}
