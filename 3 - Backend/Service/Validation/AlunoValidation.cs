using Domain.Entities;

namespace Service.Validation
{
    public class AlunoValidation
    {
        public bool ValidateModel(Aluno model, out List<string> Errorlist)
        {
            bool _valid = true;
            Errorlist = new List<string>();

            

            return _valid;
        }
    }
}
