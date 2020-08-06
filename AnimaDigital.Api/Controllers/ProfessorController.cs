using System;
using AnimaDigital.Application.Contextos.ConsultarProfessorSalarioContexto;
using AnimaDigital.Application.Contextos.NovoProfessorContexto;
using AnimaDigital.Application.DTOs;
using AnimaDigital.Application.Exceptions;
using AnimaDigital.Domain.Models; 
using MediatR; 
using Microsoft.AspNetCore.Mvc;

namespace AnimaDigital.Api.Controllers
{
    [Route("school/professor")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProfessorController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Realiza o cadastro de um novo professor.
        /// </summary>
        /// <param name="professor"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public ActionResult<ProfessorDTO> Post(ProfessorDTO professor)
        {
            try
            {
                if (!ValidaDigitoCPF.ValidaCPF(professor.Cpf))
                    throw new ValidacaoException("Campo cpf inválido!");

                if (professor.Codigo <= 0)
                    throw new ValidacaoException("Campo CodFuncionario deve ser maior que 0!");

                NovoProfessorCommand command = new NovoProfessorCommand()
                {
                    Usuario = new Usuario(
                        professor.Cpf, professor.Email,
                        professor.Nome, professor.Login,
                        professor.Senha, new Professor(professor.Cpf, professor.Codigo)
                    )
                };

                professor = this.mediator.Send(command).Result;

                return new CreatedResult("", professor);
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

        /// <summary>
        /// Recupera informações de um professor, incluindo salário, total de grades e total de alunos.
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public ActionResult<ProfessorSalarioDTO> Get(string cpf)
        {
            try
            {
                if (!ValidaDigitoCPF.ValidaCPF(cpf))
                    throw new ValidacaoException("Campo cpf inválido!");
                 
                ConsultaProfessorSalarioQuery command = new ConsultaProfessorSalarioQuery(cpf);

                var professor = this.mediator.Send(command).Result;

                return new CreatedResult("", professor);
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