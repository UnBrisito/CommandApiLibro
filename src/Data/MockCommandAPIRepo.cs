using CommandsApi.Models;

namespace CommandsApi.Data
{
    public class MockCommandAPIRepo : ICommandsAPIRepo
    {
        public void CreateCommand(Comando cmd)
        {
            throw new NotImplementedException();
        }

        public void DeleteCommand(Comando cmd)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Comando> GetAllCommands()
        {
            var commands = new List<Comando>
 {
 new Comando{
 Id=0, HowTo="How to generate a migration",
 CommandLine="dotnet ef migrations add <Name of Migration>",
 Platform=".Net Core EF"},
 new Comando{
 Id=1, HowTo="Run Migrations",
 CommandLine="dotnet ef database update",
 Platform=".Net Core EF"},
 new Comando{
 Id=2, HowTo="List active migrations",
 CommandLine="dotnet ef migrations list",
 Platform=".Net Core EF"}
 };
            return commands;
        }

        public Comando GetCommandById(int id)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateCommand(Comando cmd)
        {
            throw new NotImplementedException();
        }
    }
}
