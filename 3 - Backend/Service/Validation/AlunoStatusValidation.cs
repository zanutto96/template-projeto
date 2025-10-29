using Domain.Entities;

namespace Service.Validation
{
    public class AlunoStatusValidation
    {
        public bool ValidateModel(AlunoStatus model, out List<string> Errorlist)
        {
            bool _valid = true;
            Errorlist = new List<string>();

            

            return _valid;
        }
    }
}
