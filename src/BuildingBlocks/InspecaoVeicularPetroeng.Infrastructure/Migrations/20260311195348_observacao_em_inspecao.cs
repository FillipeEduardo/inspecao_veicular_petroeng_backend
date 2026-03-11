using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InspecaoVeicularPetroeng.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class observacao_em_inspecao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "observacao",
                table: "inspecoes",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "observacao",
                table: "inspecoes");
        }
    }
}
