
namespace AnimaDigital.Application.Contextos.ConsultarProfessorSalarioContexto
{
    internal static class CalcularSalarioProfessor
    {
        private const decimal SalarioBase = 1200.00m;
        private const decimal BonusBase = 50.00m;
        private const decimal MaxAlunosGrade = 10.0m;

        public static decimal CalcularSalario(int qtdAlunos, int qtdGrades )
            => (qtdAlunos / MaxAlunosGrade * qtdGrades) * BonusBase + SalarioBase; 
    }
}
