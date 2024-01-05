//using System;
//using Application.Features.LineMessaging;

//using Application.Features.User;
//using Application.Features.InternshipForm;
//using Domain.DTOs;
//using MediatR;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//using Application.Features.RegisterInternForm;
//using System.Drawing;

//namespace Presentation.Controllers;

//public class InternshipFormController : BaseController
//{
//    [AllowAnonymous]
//    [HttpPost("postInternshipForm")]
//    public Task<bool> PostInternshipForm([FromBody] RegisterInternshipForm.Command request) => Mediator.Send(request);


//    }