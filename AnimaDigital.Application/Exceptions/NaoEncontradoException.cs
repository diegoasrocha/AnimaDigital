using System;
using System.Collections.Generic;
using System.Text;

namespace AnimaDigital.Application.Exceptions
{
    public class NaoEncontradoException : Exception
    {
        public NaoEncontradoException(string mensagem) : base(mensagem) { }

        public NaoEncontradoException(string mensagem, Exception inner) : base(mensagem, inner) { }
    }
}
