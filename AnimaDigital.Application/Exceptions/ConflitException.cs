using System; 

namespace AnimaDigital.Application.Exceptions
{
    public class ConflitException : Exception
    {
        public ConflitException(string mensagem) : base(mensagem) { }

        public ConflitException(string mensagem, Exception inner) : base(mensagem, inner) { }
    }
}
