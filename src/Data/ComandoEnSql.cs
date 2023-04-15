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
            if (cmd == null) throw new ArgumentNullException(nameof(cmd));
            _context.MisComandos.Add(cmd);
        }

        public void DeleteCommand(Comando cmd)
        {
            if (cmd == null) throw new ArgumentNullException(nameof(cmd));
            _context.MisComandos.Remove(cmd);
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
            return _context.SaveChanges() >=0 ;
        }

        public void UpdateCommand(Comando cmd)
        {
        }
    }
}
