using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace antecipacao_recebivel.Migrations
{
    /// <inheritdoc />
    public partial class CriandoEditando : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cnpj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    faturamentoMensal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ramo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Empresas");
        }
    }
}
