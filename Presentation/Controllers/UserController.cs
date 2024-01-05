using System;
using Application.Features.LineMessaging;
using Application.Features.Profile;
using Application.Features.User;
using Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public class UserController : BaseController
{
    public UserController()
    {
    }

    [AllowAnonymous]
    [HttpPost("register/trainee")]
    public Task<bool> RegisterForTrainee([FromBody] RegisterTrainee.Command request) => Mediator.Send(request);

    [AllowAnonymous]
    [HttpGet("profile")]
    public Task<UserProfileDTO?> GetProfile() => Mediator.Send(new GetProfile.Query());

    [AllowAnonymous]
    [HttpPost("postprofile")]
    public Task<bool> PostProfile([FromBody] PostProfile.Command request) => Mediator.Send(request);


    //[AllowAnonymous]
    //[HttpPost("submit")]
    //public Task<bool> RegisterAccount([FromBody] RegisterAccount.Command body) => Mediator.Send(body);
}