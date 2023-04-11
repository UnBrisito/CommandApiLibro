using System.Collections.Generic;
using CommandsApi.Models;
namespace CommandsApi.Data
{
    public interface ICommandsAPIRepo
    {
        bool SaveChanges();
        IEnumerable<Comando> GetAllCommands();
        Comando GetCommandById(int id);
        void CreateCommand(Comando cmd);
        void UpdateCommand(Comando cmd);
        void DeleteCommand(Comando cmd);

    }
}
