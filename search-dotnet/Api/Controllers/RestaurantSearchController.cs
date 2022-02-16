using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Application.ViewModels;
using Application;
using System.Collections.Generic;

namespace Api.Controllers
{
    public class ServerError
    {
        public string Id { get; }
        public string Message { get; }
        public string ErrorId { get;  }
    }

    [Route("api/search")]
    [ApiController]
    public class RestaurantSearchController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RestaurantSearchController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServerError))]
        public async Task<IEnumerable<RestaurantViewModel>> Get(string id, CancellationToken ct)
        {
            return await _mediator.Send(new FindRestaurantsByCodeQuery(id), ct);
        }
    }
}
