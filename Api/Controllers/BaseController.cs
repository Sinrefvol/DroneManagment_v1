using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BaseController : ControllerBase
    {

        //Wrappers for mediator.send, gets the mediator service upon request and dispatches provided command.
        protected Task DispatchAsync(IRequest command)
        {
            var mediator = HttpContext.RequestServices.GetRequiredService<IMediator>();
            return mediator.Send(command, HttpContext.RequestAborted);
        }

        protected Task<TResult> DispatchAsync<TResult>(IRequest<TResult> command)
        {
            var mediator = HttpContext.RequestServices.GetRequiredService<IMediator>();
            return mediator.Send(command, HttpContext.RequestAborted);
        }
    }
}
