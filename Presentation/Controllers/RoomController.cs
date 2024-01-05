
using Application.Features.Room;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class RoomController : BaseController
    {
        [AllowAnonymous]
        [HttpGet("getRoom")]
        public Task<List<RoomDTO>> FindTeam() => Mediator.Send(new GetRoom.Query());






    }
}
