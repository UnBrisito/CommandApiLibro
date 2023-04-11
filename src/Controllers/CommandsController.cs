using Microsoft.AspNetCore.Mvc;
using CommandsApi.Data;
using CommandsApi.Models;

namespace comandapivs.Controllers
{

    [Route("api/[Controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandsAPIRepo _repository;
        public CommandsController(ICommandsAPIRepo repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Comando>> Get()
        {
            var comandos = _repository.GetAllCommands();
            return Ok(comandos);
        }
        [HttpGet("{id}")]
        public ActionResult<Comando> GetById(int id)
        {
            var comandoSolicitado = _repository.GetCommandById(id);
            if (comandoSolicitado == null)
            {
                return NotFound();
            }
            return Ok(comandoSolicitado);
        }
    }

}
