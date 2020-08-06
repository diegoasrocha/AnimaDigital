using AnimaDigital.Domain.Models;
using AnimaDigital.Infrastructure.DBConfiguration;
using AnimaDigital.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore; 
using System.Collections.Generic;
using System.Linq;  

namespace AnimaDigital.Infrastructure.Repositories
{
    public class GradeRepository : Repository<Grade>, IGradeRepository
    {
        public GradeRepository(AnimaContext dbContext) : base(dbContext) { }

        public Grade GetByIdIncludeAlunos(int CodGrade)
            => dbSet.Include(g => g.GradeAlunos)
                        .ThenInclude(ga => ga.Aluno)
                    .Where(g => g.CodGrade == CodGrade).FirstOrDefault();

        public Grade GetByIdIncludeProfessorAlunos(int CodGrade)
            => dbSet.Include(g => g.Professor).ThenInclude(p => p.Usuario)
                    .Include(g => g.GradeAlunos).ThenInclude(ga => ga.Aluno).ThenInclude(a => a.Usuario)
                    .Where(g => g.CodGrade == CodGrade).FirstOrDefault();

        public Grade GetSubGrade(Grade grade)
            => dbSet.Include(g => g.GradeAlunos).ThenInclude(ga => ga.Aluno)
                    .Where(g => g.Turma == grade.Turma && g.Disciplina == grade.Disciplina && g.Curso == grade.Curso && g.GradeAlunos.Count < 10)
                    .OrderByDescending(g => g.GradeAlunos.Count).FirstOrDefault();

        public IEnumerable<Grade> GetSubGrades(Grade grade)
            => dbSet.Include(g => g.GradeAlunos).ThenInclude(ga => ga.Aluno)
                    .Where(g => g.Turma == grade.Turma && g.Disciplina == grade.Disciplina && g.Curso == grade.Curso)
                    .AsEnumerable();
    }
}
