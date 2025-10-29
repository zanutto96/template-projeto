using System; 

namespace Domain.Filters
{
    public class AlunoStatusFilter : FilterBase
    {
        public int AlunoStatusId { get; set; }
        public string? Descricao { get; set; }

    }
}
