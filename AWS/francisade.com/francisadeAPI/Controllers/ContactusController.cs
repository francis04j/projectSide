using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace francisadeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactusController : ControllerBase
    {
       
        [HttpGet("{id}")]
        public ContactusResponse Get(int id)
        {
            return new ContactusResponse() { Id = id, Email = "email@email.com", Message = "Test", Name = "Fracnis" };
        }
    }
}
