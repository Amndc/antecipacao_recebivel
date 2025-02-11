using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace antecipacao_recebivel.Migrations
{
    /// <inheritdoc />
    public partial class NewOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Empresas",
                newName: "idempresa");

            migrationBuilder.RenameColumn(
                name: "faturamentoMensal",
                table: "Empresas",
                newName: "faturamento");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Empresas",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "faturamento",
                table: "Empresas",
                newName: "faturamentoMensal");
        }
    }
}
