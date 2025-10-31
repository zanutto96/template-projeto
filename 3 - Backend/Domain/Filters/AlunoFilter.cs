using Domain.Entities;
using System; 

namespace Domain.Filters
{
    public class AlunoFilter : FilterBase
    {
        public int? AlunoFormacaoId { get; set; }
        public int AlunoId { get; set; }
        public int? AlunoStatusId { get; set; }
        public string? AreaAtuacaoEmpresa { get; set; }
        public int? AreaAtuacaoId { get; set; }
        public string? Bairro { get; set; }
        public string? CargoQueOcupa { get; set; }
        public string? CEP { get; set; }
        public string? Comentario { get; set; }
        public string CPF { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataNascimento { get; set; }
        public bool? EhEmail { get; set; }
        public bool? EhExAluno { get; set; }
        public bool? EhFalecido { get; set; }
        public bool? EhMalaDireta { get; set; }
        public bool? EhMkt { get; set; }
        public bool? EhSMS { get; set; }
        public string Email { get; set; }
        public string? Endereco { get; set; }
        public string? EnderecoComplemento { get; set; }
        public string? EnderecoNumero { get; set; }
        public string? Estado { get; set; }
        public int? EstadoCivilId { get; set; }
        public string? LocalNascimento { get; set; }
        public int? NivelFormacaoId { get; set; }
        public string NomeCompleto { get; set; }
        public string? NomeCracha { get; set; }
        public string? NomeEmpresaOndeTrabalha { get; set; }
        public string? NomeMae { get; set; }
        public string? NomePai { get; set; }
        public string? NomeSocial { get; set; }
        public string? RG { get; set; }
        public int? SexoId { get; set; }
        public string? TelCelular { get; set; }
        public string? TelComercial { get; set; }
        public string? TelResidencial { get; set; }
        public bool? TemWhatsApp { get; set; }
        public string? URLFotoAluno { get; set; }
        public string? URLInstagran { get; set; }
        public string? URLLinkedin { get; set; }
        public int? UsuarioAlteracaoId { get; set; }
        public int? UsuarioCadastroId { get; set; }
        public int? UsuarioId { get; set; }
        // public virtual NivelFormacao NivelFormacao { get; set; }
        public virtual AlunoStatus? AlunoStatus { get; set; }
        // public virtual AreaAtuacao AreaAtuacao { get; set; }
        // public virtual stadoCivil EstadoCivil { get; set; }
        // public virtual Sexo Sexo { get; set; }
        // public virtual Usuario Usuario { get; set; }

    }
}
