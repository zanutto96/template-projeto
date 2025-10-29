using Common.Security.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.BasicExtensions
{
    public class AuditBasic
    {
        public DateTime? DataAlteracao { get; set; }
        public DateTime? DataCadastro { get; set; }
        public int? UsuarioAlteracaoId { get; set; }
        public int? UsuarioCadastroId { get; set; }


        public void UpdateAudit(ICurrentUser _currentUser)
        {
            try
            {
                this.DataAlteracao = DateTime.Now;
                var currentUser = _currentUser.GetSubjectId();
                this.UsuarioAlteracaoId = currentUser;
                if (this.UsuarioAlteracaoId == 0)
                {
                    this.UsuarioAlteracaoId = null;
                }

                if (!this.DataCadastro.HasValue) this.DataCadastro = this.DataAlteracao.Value;
                if (!this.UsuarioCadastroId.HasValue) this.UsuarioCadastroId = this.UsuarioAlteracaoId;
            }
            catch { }
        }
    }
}
