using System.ComponentModel.DataAnnotations.Schema;

namespace ProvaConceitoSignalR.Model
{
    [Table("Requisicao")]
    public class Requisicao
    {
        public int ano { get; set; }
        public int orderid { get; set; }
        public int Id { get; set; }
    }
}
