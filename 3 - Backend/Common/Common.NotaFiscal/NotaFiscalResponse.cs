using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.NotaFiscal
{
   public class NotaFiscalBody
   {

      public int tipo { get; set; }
      public string numero { get; set; }
      public string dataOperacao { get; set; }
      public Contato contato { get; set; }
      public Naturezaoperacao naturezaOperacao { get; set; }
      public int finalidade { get; set; }
      public int seguro { get; set; }
      public int despesas { get; set; }
      public int desconto { get; set; }
      public string observacoes { get; set; }
      public Item[] itens { get; set; }


      public class Contato
      {
         public string nome { get; set; }
         public string tipoPessoa { get; set; }
         public string numeroDocumento { get; set; }
         public string rg { get; set; }
         public int contribuinte { get; set; }
         public string telefone { get; set; }
         public string email { get; set; }
         public Endereco endereco { get; set; }
      }

      public class Endereco
      {
         public string endereco { get; set; }
         public string numero { get; set; }
         public string complemento { get; set; }
         public string bairro { get; set; }
         public string cep { get; set; }
         public string municipio { get; set; }
         public string uf { get; set; }
         public string pais { get; set; }
      }

      public class Naturezaoperacao
      {
         public int id { get; set; }
      }

      public class Item
      {
         public string codigo { get; set; }
         public string descricao { get; set; }
         public string unidade { get; set; }
         public int quantidade { get; set; }
         public double valor { get; set; }
         public string tipo { get; set; }
         public double pesoBruto { get; set; }
         public double pesoLiquido { get; set; }
         public string numeroPedidoCompra { get; set; }
         public string classificacaoFiscal { get; set; }
         public string cest { get; set; }
         public string codigoServico { get; set; }
         public int origem { get; set; }
         public string informacoesAdicionais { get; set; }
      }

   }
 
   public class NotaFiscalServicoBody
   {

   }
}
