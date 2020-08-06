using System; 

namespace AnimaDigital.Application.Exceptions
{
    public class ValidacaoException : Exception
    { 
        public ValidacaoException(string mensagem) : base(mensagem) { }

        public ValidacaoException(string mensagem, Exception inner) : base(mensagem, inner) { } 
    }
}
