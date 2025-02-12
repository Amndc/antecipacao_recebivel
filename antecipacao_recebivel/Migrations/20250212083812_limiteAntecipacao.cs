using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace antecipacao_recebivel.Migrations
{
    /// <inheritdoc />
    public partial class limiteAntecipacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "datavencimento",
                table: "NotasFiscais",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<decimal>(
                name: "valor",
                table: "NotasFiscais",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "LimiteAntecipacao",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    faixaMin = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    faixaMax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    porcentagem = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LimiteAntecipacao", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LimiteAntecipacao");

            migrationBuilder.DropColumn(
                name: "valor",
                table: "NotasFiscais");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "datavencimento",
                table: "NotasFiscais",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
