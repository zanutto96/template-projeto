using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.NotaFiscal
{
   public class NotaFiscalResponse
   {

      public Data? data { get; set; }
      public Error? error { get; set; }

      public class Data
      {
         public long id { get; set; }
         public int situacao { get; set; }
         public string numero { get; set; }
         public string serie { get; set; }
         public string xml { get; set; }
         public Contato contato { get; set; }
      }

      public class Contato
      {
         public string nome { get; set; }
      }

      public class Error
      {
         public string type { get; set; }
         public string message { get; set; }
         public string msg { get; set; }
         public string description { get; set; }
         public Field[] fields { get; set; }
      }

      public class Field
      {
         public string message { get; set; }
         public string msg { get; set; }
         public string element { get; set; }
         public FieldError[] collection { get; set; }

      }

      public class FieldError
      {
         public int index { get; set; }
         public int code { get; set; }
         public string msg { get; set; }
         public string element { get; set; }
         public string _namespace { get; set; }
      }


   }


   public class NotaFiscalServicoRequest
   {
      public string numero { get; set; }
      public string numeroRPS { get; set; }
      public string serie { get; set; }
      public string dataEmissao { get; set; }
      public Contato contato { get; set; }
      public string data { get; set; }
      public bool reterISS { get; set; }
      public double desconto { get; set; }
      public Servico[] servicos { get; set; }
   }

   public class Contato
   {
      public int id { get; set; }
      public string nome { get; set; }
      public string numeroDocumento { get; set; }
      public string email { get; set; }
      public string ie { get; set; }
      public string telefone { get; set; }
      public object im { get; set; }
      public Endereco endereco { get; set; }
      public int? contribuinte { get; set; }
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
   }

   public class Servico
   {
      public string codigo { get; set; }
      public string descricao { get; set; }
      public double valor { get; set; }
   }


   public class NotaFiscalProdutoRequest
   {
      public int tipo { get; set; }
      public string numero { get; set; }
      public string dataOperacao { get; set; }
      public Contato contato { get; set; }
      public NaturezaOperacao naturezaOperacao { get; set; }
      public int finalidade { get; set; }
      public double seguro { get; set; }
      public double despesas { get; set; }
      public double desconto { get; set; }
      public string observacoes { get; set; }
      public Item[] itens { get; set; }
   }

   public class NaturezaOperacao
   {
      public int id { get; set; }
   }

   public class Item
   {
      public string codigo { get; set; }
      public string descricao { get; set; }
      public string unidade { get; set; }
      public double quantidade { get; set; }
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
