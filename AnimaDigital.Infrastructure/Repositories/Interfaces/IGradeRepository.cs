using AnimaDigital.Domain.Models; 
using System.Collections.Generic;  

namespace AnimaDigital.Infrastructure.Repositories.Interfaces
{
    public interface IGradeRepository : IRepository<Grade>
    {
        Grade GetByIdIncludeAlunos(int CodGrade);
        Grade GetByIdIncludeProfessorAlunos(int CodGrade);
        Grade GetSubGrade(Grade grade);
        IEnumerable<Grade> GetSubGrades(Grade grade);
    }
}
