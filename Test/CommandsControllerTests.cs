using AutoMapper;
using comandapivs.Controllers;
using CommandsApi.Data;
using CommandsApi.Dots;
using CommandsApi.Models;
using CommandsApi.Profiles;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class CommandsControllerTests : IDisposable
    {
        Mock<ICommandsAPIRepo> mockRepo;
        ComandosProfile realProfile;
        MapperConfiguration configuration;
        IMapper mapper;
        public CommandsControllerTests()
        {
            mockRepo = new Mock<ICommandsAPIRepo>();
            realProfile = new ComandosProfile();
            configuration = new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            mapper = new Mapper(configuration);
        }

        public void Dispose()
        {
            mockRepo = null;
            realProfile = null;
            configuration = null;
            mapper = null;
        }

        [Fact]
        public void GetCommandItems_ReturnsOk_WhenDBIsEmpty()
        {
            
            mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetComandos(0));

            var controller = new CommandsController(mockRepo.Object, mapper);

            var resultado = controller.Get();

            Assert.IsType<OkObjectResult>(resultado.Result);
        }

        public void GetAllCommands_ReturnsOneItem_WhenDBHasOneResource()
        {
            mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetComandos(1));
            var controller = new CommandsController(mockRepo.Object, mapper);

            var resultado = controller.Get();

            var okResult = resultado.Result as OkObjectResult;

            var comandos = okResult.Value as List<ComandoReadDto>;

            Assert.Single(comandos);
        }

        [Fact]
        public void GetCommandItems_ReturnsOk_WhenDBHasOneResource()
        {

            mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetComandos(1));

            var controller = new CommandsController(mockRepo.Object, mapper);

            var resultado = controller.Get();

            Assert.IsType<OkObjectResult>(resultado.Result);
        }

        [Fact]
        public void GetAllCommands_ReturnsCorrectType_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo.Setup(repo =>
            repo.GetAllCommands()).Returns(GetComandos(1));
            var controller = new CommandsController(mockRepo.Object, mapper);
            //Act
            var result = controller.Get();
            //Assert
            Assert.IsType<ActionResult<IEnumerable<ComandoReadDto>>>(result);
        }
        //Faltan tests, volver a página 307
        //Falso origen de los datos
        private List<Comando> GetComandos(int num)
        {
            var comandos = new List<Comando>();
            if (num > 0)
            {
                comandos.Add(new Comando
                {
                    Id = 0,
                    HowTo = "How to generate a migration",
                    CommandLine = "dotnet ef migrations add <Name of Migration>",
                    Platform = ".Net Core EF"
                });
            }
            return comandos;
        }
    }
}
