using System; 
using AnimaDigital.Application.Contextos.NovaMatriculaContexto;
using AnimaDigital.Application.Contextos.RemoverMatriculaContexto;
using AnimaDigital.Application.DTOs;
using AnimaDigital.Application.Exceptions;
using AnimaDigital.Domain.Models;
using MediatR; 
using Microsoft.AspNetCore.Mvc;

namespace AnimaDigital.Api.Controllers
{
    [Route("school/matricula")]
    [ApiController]
    public class MatriculaController : ControllerBase
    {
        private readonly IMediator mediator;

        public MatriculaController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Matricula um aluno em uma grade informada.
        /// </summary>
        /// <param name="matricula"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public ActionResult<string> Post(MatriculaDTO matricula)
        {
            try
            {
                if(matricula.Ra <= 0)
                    throw new ValidacaoException("Campo RA deve ser maior que 0!");

                if(matricula.CodGrade <= 0)
                    throw new ValidacaoException("Campo codGrade deve ser maior que 0!");

                NovaMatriculaCommand command = new NovaMatriculaCommand()
                {
                    AlunoGrade = new AlunoGrade(matricula.CodGrade, matricula.Ra)
                };

                bool matriculou = this.mediator.Send(command).Result;

                if (matriculou)
                    return new CreatedResult("", new { mensagem = "Matricula realizada com sucesso!" });
                else 
                    return BadRequest("Não foi possível realizar a matricula!");
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
        /// Desmatricula um aluno de uma grade informada
        /// </summary>
        /// <param name="matricula"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        public ActionResult<string> Delete(MatriculaDTO matricula)
        {
            try
            {
                RemoverMatriculaCommand command = new RemoverMatriculaCommand()
                {
                    AlunoGrade = new AlunoGrade(matricula.CodGrade, matricula.Ra)
                };

                bool desmatriculou = this.mediator.Send(command).Result;

                if (desmatriculou)
                    return Ok(new { mensagem = "Matricula removida com sucesso!" });
                else
                    return BadRequest(new { Erros = new string[] { "Não foi possível remover a matricula!" } });
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