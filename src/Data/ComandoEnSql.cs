using CommandsApi.Models;

namespace CommandsApi.Data
{
    public class ComandoEnSql : ICommandsAPIRepo
    {
        private readonly ComandoContext _context;

        public ComandoEnSql(ComandoContext context)
        {
            _context = context;
        }
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
            return _context.MisComandos.ToList();
        }

        public Comando GetCommandById(int id)
        {
            return _context.MisComandos.FirstOrDefault(p => p.Id==id);
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
