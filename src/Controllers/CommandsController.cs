using Microsoft.AspNetCore.Mvc;
using CommandsApi.Data;
using CommandsApi.Models;
using AutoMapper;
using CommandsApi.Dots;
using System.Collections.Generic;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;

namespace comandapivs.Controllers
{

    [Route("api/[Controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandsAPIRepo _repository;
        private readonly IMapper _mapper;
        public CommandsController(ICommandsAPIRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<ComandoReadDto>> Get()
        {
            var comandos = _repository.GetAllCommands();
            return Ok(_mapper.Map<IEnumerable<ComandoReadDto>>(comandos));
        }
        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<ComandoReadDto> GetById(int id)
        {
            var comandoSolicitado = _repository.GetCommandById(id);
            if (comandoSolicitado == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ComandoReadDto> (comandoSolicitado));
        }

        [HttpPost]
        public ActionResult<ComandoReadDto> CreateCommand(ComandoCreateDto cmdCreate)
        {
            var comandoModel = _mapper.Map<Comando>(cmdCreate);
            _repository.CreateCommand(comandoModel);
            _repository.SaveChanges();

            var comandoReadDto = _mapper.Map<ComandoReadDto>(comandoModel);

            return CreatedAtRoute("GetCommandById", new {Id = comandoReadDto.Id}, comandoReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult<ComandoReadDto> UpdateCommand(ComandoUpdateDto CmdUpdate, int id)
        {
            var comandoRepo = _repository.GetCommandById(id);
            if (comandoRepo == null) return NotFound();

            _mapper.Map(CmdUpdate, comandoRepo);
            _repository.UpdateCommand(comandoRepo);
            _repository.SaveChanges();
            
            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<ComandoUpdateDto> patchDoc)
        {
            var comandoRepo = _repository.GetCommandById(id);
            if (comandoRepo == null) return NotFound();

            var comandoACambiar = _mapper.Map<ComandoUpdateDto>(comandoRepo);
            patchDoc.ApplyTo(comandoACambiar, ModelState);

            if (!TryValidateModel(comandoACambiar)) return ValidationProblem(ModelState);

            _mapper.Map(comandoACambiar, comandoRepo);
            _repository.UpdateCommand(comandoRepo);
            _repository.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var comandoRepo = _repository.GetCommandById(id);
            if (comandoRepo == null) return NotFound();

            _repository.DeleteCommand(comandoRepo);
            _repository.SaveChanges();
            return NoContent();
        }
    }

}
