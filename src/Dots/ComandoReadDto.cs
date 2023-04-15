using System.ComponentModel.DataAnnotations;

namespace CommandsApi.Dots
{
    public class ComandoReadDto
    {
        public int Id { get; set; }
        public string HowTo { get; set; }
        public string Platform { get; set; }
        public string CommandLine { get; set; }
    }
}
