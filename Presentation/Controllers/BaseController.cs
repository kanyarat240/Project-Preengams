using System;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BaseController : ControllerBase
{
    private IMediator? _mediator;

    protected IMediator Mediator
    {
        get
        {
            if (_mediator == null)
            {
                _mediator = HttpContext.RequestServices.GetService<IMediator>();
            }
            return _mediator;
        }
    }
}

