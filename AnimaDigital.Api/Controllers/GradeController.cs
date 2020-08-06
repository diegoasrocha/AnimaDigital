using System;
using AnimaDigital.Application.Contextos.ConsultarGradeContexto;
using AnimaDigital.Application.Contextos.NovaGradeContexto;
using AnimaDigital.Application.DTOs;
using AnimaDigital.Application.Exceptions;
using AnimaDigital.Domain.Models;
using MediatR; 
using Microsoft.AspNetCore.Mvc;

namespace AnimaDigital.Api.Controllers
{
    [Route("school/grade")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly IMediator mediator;

        public GradeController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Realiza o cadastro de uma nova grade.
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public ActionResult<GradeDTO> Post(GradeDTO grade)
        {
            try
            {
                NovaGradeCommand command = new NovaGradeCommand()
                { 
                    Grade = new Grade(
                        grade.CodGrade, grade.Turma, 
                        grade.Disciplina, grade.Curso, 
                        grade.CodFuncionario
                    )
                };

                grade = this.mediator.Send(command).Result;

                return new CreatedResult("", grade);
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
        /// Recupera informações de uma grade, incluindo dados do professor e de alunos.
        /// </summary>
        /// <param name="codGrade"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public ActionResult<GradeProfessorAlunosDTO> Get(int codGrade)
        {
            try
            {
                ConsultaGradeQuery command = new ConsultaGradeQuery(codGrade); 

                var grade = this.mediator.Send(command).Result;

                return Ok(grade);
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