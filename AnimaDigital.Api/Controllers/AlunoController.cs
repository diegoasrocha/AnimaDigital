using System; 
using AnimaDigital.Application.Contextos.NovoAlunoContexto;
using AnimaDigital.Application.DTOs;
using AnimaDigital.Application.Exceptions;
using AnimaDigital.Domain.Models; 
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnimaDigital.Api.Controllers
{
    [Route("school/aluno")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly IMediator mediator;

        public AlunoController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Realiza o cadastro de um novo aluno.
        /// </summary>
        /// <param name="aluno"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public ActionResult<AlunoDTO> Post(AlunoDTO aluno)
        {
            try
            {
                if (!ValidaDigitoCPF.ValidaCPF(aluno.Cpf))
                    throw new ValidacaoException("Campo cpf inválido!");

                if (aluno.Ra <= 0)
                    throw new ValidacaoException("Campo RA deve ser maior que 0!");

                NovoAlunoCommand command = new NovoAlunoCommand()
                {
                    Usuario = new Usuario(
                        aluno.Cpf, aluno.Email,
                        aluno.Nome, aluno.Login,
                        aluno.Senha, new Aluno(aluno.Cpf, aluno.Ra)
                    )
                };

                aluno = this.mediator.Send(command).Result;

                return new CreatedResult("", aluno);
            }
            catch (Exception ex)
            {
                if (ex.InnerException is ConflitException)
                    return Conflict(new { Erros = new string[] { ex.InnerException?.Message ?? ex.Message } });
                else if (ex.InnerException is NaoEncontradoException)
                    return NotFound(new { Erros = new string[] { ex.InnerException?.Message ?? ex.Message } });
                else
                    return UnprocessableEntity(new { Erros = new string[] { ex.InnerException?.Message ?? ex.Message } });
            }
        }
    }
}