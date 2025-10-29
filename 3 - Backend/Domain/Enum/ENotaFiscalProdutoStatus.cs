using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enum
{
   public enum ENotaFiscalProdutoStatus
   {
      Pendente = 1,
      Cancelada = 2,
      AguardandoRecibo = 3,
      Rejeitada = 4,
      Autorizada = 5,
      EmitidaDANFE = 6,
      Registrada = 7,
      AguardandoProtocolo = 8,
      Denegada = 9,
      ConsultaSituacao = 10,
      Bloqueada = 11
   }
}
