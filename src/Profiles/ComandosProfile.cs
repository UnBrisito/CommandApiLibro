using AutoMapper;
using CommandsApi.Dots;
using CommandsApi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CommandsApi.Profiles
{
    public class ComandosProfile : Profile
    {
        public ComandosProfile()
        {
            CreateMap<Comando, ComandoReadDto>();
            CreateMap<ComandoCreateDto, Comando>();
            CreateMap<ComandoUpdateDto, Comando>();
            CreateMap<Comando, ComandoUpdateDto>();
        }
    }
}
