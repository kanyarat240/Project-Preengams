
using Application.Features.Profile;
using Application.Features.User;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public class RegisterController : BaseController
{

    [AllowAnonymous]
    [HttpPost("submit")]
    public Task<bool> RegisterAccount([FromBody] RegisterAccount.Command body) => Mediator.Send(body);

    [AllowAnonymous]
    [HttpPost("updatePassword")]
    public Task<bool> UpdatePassword([FromBody] UpdatePassword.Command request) => Mediator.Send(request);


    //public Task<RegisterAccountDTO?> RegisterAccount([FromBody] RegisterTrainee.Command request) => Mediator.Send(request);


}