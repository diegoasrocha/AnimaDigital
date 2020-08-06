using Microsoft.EntityFrameworkCore.Migrations;

namespace AnimaDigital.Infrastructure.Migrations
{
    public partial class Tabelas_Iniciais : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Cpf = table.Column<string>(maxLength: 11, nullable: false),
                    Email = table.Column<string>(maxLength: 150, nullable: false),
                    Nome = table.Column<string>(maxLength: 150, nullable: false),
                    Login = table.Column<string>(maxLength: 20, nullable: false),
                    Senha = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Cpf);
                });

            migrationBuilder.CreateTable(
                name: "Aluno",
                columns: table => new
                {
                    Cpf = table.Column<string>(maxLength: 11, nullable: false),
                    Ra = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aluno", x => new { x.Cpf, x.Ra });
                    table.ForeignKey(
                        name: "FK_Aluno_Usuario_Cpf",
                        column: x => x.Cpf,
                        principalTable: "Usuario",
                        principalColumn: "Cpf",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Professor",
                columns: table => new
                {
                    Cpf = table.Column<string>(maxLength: 11, nullable: false),
                    CodFuncionario = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professor", x => new { x.Cpf, x.CodFuncionario });
                    table.UniqueConstraint("AK_Professor_CodFuncionario_Cpf", x => new { x.CodFuncionario, x.Cpf });
                    table.ForeignKey(
                        name: "FK_Professor_Usuario_Cpf",
                        column: x => x.Cpf,
                        principalTable: "Usuario",
                        principalColumn: "Cpf",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Grade",
                columns: table => new
                {
                    CodGrade = table.Column<int>(nullable: false),
                    Curso = table.Column<string>(maxLength: 150, nullable: false),
                    Disciplina = table.Column<string>(maxLength: 100, nullable: false),
                    Turma = table.Column<string>(maxLength: 20, nullable: false),
                    Cpf = table.Column<string>(maxLength: 11, nullable: false),
                    CodFuncionario = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grade", x => x.CodGrade);
                    table.ForeignKey(
                        name: "FK_Grade_Professor_Cpf_CodFuncionario",
                        columns: x => new { x.Cpf, x.CodFuncionario },
                        principalTable: "Professor",
                        principalColumns: new[] { "Cpf", "CodFuncionario" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlunoGrade",
                columns: table => new
                {
                    Cpf = table.Column<string>(nullable: false),
                    Ra = table.Column<int>(nullable: false),
                    CodGrade = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunoGrade", x => new { x.Cpf, x.Ra, x.CodGrade });
                    table.UniqueConstraint("AK_AlunoGrade_CodGrade_Cpf_Ra", x => new { x.CodGrade, x.Cpf, x.Ra });
                    table.ForeignKey(
                        name: "FK_AlunoGrade_Grade_CodGrade",
                        column: x => x.CodGrade,
                        principalTable: "Grade",
                        principalColumn: "CodGrade",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlunoGrade_Aluno_Cpf_Ra",
                        columns: x => new { x.Cpf, x.Ra },
                        principalTable: "Aluno",
                        principalColumns: new[] { "Cpf", "Ra" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_Cpf",
                table: "Aluno",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grade_Cpf_CodFuncionario",
                table: "Grade",
                columns: new[] { "Cpf", "CodFuncionario" });

            migrationBuilder.CreateIndex(
                name: "IX_Professor_Cpf",
                table: "Professor",
                column: "Cpf",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlunoGrade");

            migrationBuilder.DropTable(
                name: "Grade");

            migrationBuilder.DropTable(
                name: "Aluno");

            migrationBuilder.DropTable(
                name: "Professor");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
