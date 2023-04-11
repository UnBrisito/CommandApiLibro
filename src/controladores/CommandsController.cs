using Microsoft.AspNetCore.Mvc;

namespace comandapivs.controladores
{

    [Route("api/[Controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "this", "is", "hard", "coded" };
        }
    }

}
