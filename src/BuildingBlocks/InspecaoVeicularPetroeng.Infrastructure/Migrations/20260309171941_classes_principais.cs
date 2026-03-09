using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InspecaoVeicularPetroeng.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class classes_principais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "evidencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_evidencias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "itens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_itens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "status_vistoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_status_vistoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "veiculos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Placa = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    Ano = table.Column<int>(type: "integer", nullable: false),
                    Modelo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_veiculos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "vistorias",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Data = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    QuilometragemVeiculo = table.Column<int>(type: "integer", nullable: false),
                    Observacao = table.Column<string>(type: "text", nullable: true),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    VeiculoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vistorias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_vistorias_status_vistoria_StatusId",
                        column: x => x.StatusId,
                        principalTable: "status_vistoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_vistorias_veiculos_VeiculoId",
                        column: x => x.VeiculoId,
                        principalTable: "veiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fotos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NomeArquivo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Extensao = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    EvidenciaId = table.Column<int>(type: "integer", nullable: false),
                    VistoriaId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fotos_evidencias_EvidenciaId",
                        column: x => x.EvidenciaId,
                        principalTable: "evidencias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_fotos_vistorias_VistoriaId",
                        column: x => x.VistoriaId,
                        principalTable: "vistorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "inspecoes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VistoriaId = table.Column<long>(type: "bigint", nullable: false),
                    ItemId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inspecoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_inspecoes_itens_ItemId",
                        column: x => x.ItemId,
                        principalTable: "itens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inspecoes_vistorias_VistoriaId",
                        column: x => x.VistoriaId,
                        principalTable: "vistorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_evidencias_Nome",
                table: "evidencias",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_fotos_EvidenciaId",
                table: "fotos",
                column: "EvidenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_fotos_VistoriaId",
                table: "fotos",
                column: "VistoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_inspecoes_ItemId",
                table: "inspecoes",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_inspecoes_VistoriaId",
                table: "inspecoes",
                column: "VistoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_itens_Nome",
                table: "itens",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_status_vistoria_Nome",
                table: "status_vistoria",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_veiculos_Placa",
                table: "veiculos",
                column: "Placa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vistorias_StatusId",
                table: "vistorias",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_vistorias_VeiculoId",
                table: "vistorias",
                column: "VeiculoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fotos");

            migrationBuilder.DropTable(
                name: "inspecoes");

            migrationBuilder.DropTable(
                name: "evidencias");

            migrationBuilder.DropTable(
                name: "itens");

            migrationBuilder.DropTable(
                name: "vistorias");

            migrationBuilder.DropTable(
                name: "status_vistoria");

            migrationBuilder.DropTable(
                name: "veiculos");
        }
    }
}
