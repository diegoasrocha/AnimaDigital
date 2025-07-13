using AnimaDigital.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AnimaDigital.Infrastructure.DBConfiguration
{
    public class AnimaContext : DbContext
    {
        public AnimaContext() { }

        public AnimaContext(DbContextOptions<AnimaContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseInMemoryDatabase();
            //=> optionsBuilder.UseSqlServer(DatabaseConnection.ConnectionConfiguration.GetConnectionString("DefaultConnection") );

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(builder => {
                builder.HasKey(p => p.Cpf);

                builder.Property(p => p.Cpf).ValueGeneratedNever().HasMaxLength(11);
                builder.Property(p => p.Email).HasMaxLength(150).IsRequired();
                builder.Property(p => p.Nome).HasMaxLength(150).IsRequired();
                builder.Property(p => p.Login).HasMaxLength(20).IsRequired();
                builder.Property(p => p.Senha).HasMaxLength(15).IsRequired();
            });

            modelBuilder.Entity<Aluno>(builder => {
                builder.HasKey(p => new { p.Cpf, p.Ra }); 

                builder.HasOne(p => p.Usuario).WithOne(p => p.Aluno);
            });

            modelBuilder.Entity<Professor>(builder => {
                builder.HasKey(p => new { p.Cpf, p.CodFuncionario });
                builder.HasIndex(i => i.Cpf).IsUnique();
                builder.HasIndex(i => i.CodFuncionario).IsUnique();

                builder.HasOne(p => p.Usuario).WithOne(p => p.Professor);
            });

            modelBuilder.Entity<Grade>(builder => {
                builder.HasKey(p => p.CodGrade);

                builder.Property(p => p.CodGrade).ValueGeneratedNever();
                builder.Property(p => p.Curso).HasMaxLength(150).IsRequired();
                builder.Property(p => p.Disciplina).HasMaxLength(100).IsRequired();
                builder.Property(p => p.Turma).HasMaxLength(20).IsRequired();
                builder.Property(p => p.CodFuncionario).IsRequired();

                builder.HasOne(p => p.Professor).WithMany(p => p.Grades).HasForeignKey(p => new { p.Cpf, p.CodFuncionario });
            });

            modelBuilder.Entity<AlunoGrade>(builder => {
                builder.HasKey(p => new { p.Cpf, p.Ra, p.CodGrade });

                builder.Property(p => p.Cpf).ValueGeneratedNever();
                builder.Property(p => p.Ra).ValueGeneratedNever();
                builder.Property(p => p.CodGrade).ValueGeneratedNever();

                builder.HasOne(p => p.Aluno).WithMany(p => p.AlunoGrades).HasForeignKey(p => new { p.Cpf, p.Ra }).OnDelete(DeleteBehavior.Restrict);
                builder.HasOne(p => p.Grade).WithMany(p => p.GradeAlunos).HasForeignKey(p => p.CodGrade).OnDelete(DeleteBehavior.Restrict);
                 
            });
        }
    }
}
