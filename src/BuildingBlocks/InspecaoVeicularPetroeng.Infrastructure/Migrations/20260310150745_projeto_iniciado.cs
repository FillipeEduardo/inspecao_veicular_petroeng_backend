using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InspecaoVeicularPetroeng.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class projeto_iniciado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "evidencias",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_evidencias", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "itens",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_itens", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "status_inspecao",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_status_inspecao", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "status_vistoria",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_status_vistoria", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    senha = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    nome_completo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    telefone = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    perfil = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_usuarios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "veiculos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    placa = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    ano = table.Column<int>(type: "integer", nullable: false),
                    modelo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_veiculos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vistorias",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    data = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    quilometragem_veiculo = table.Column<int>(type: "integer", nullable: false),
                    observacao = table.Column<string>(type: "text", nullable: true),
                    status_id = table.Column<int>(type: "integer", nullable: false),
                    veiculo_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_vistorias", x => x.id);
                    table.ForeignKey(
                        name: "FK_vistorias_status_vistoria_status_id",
                        column: x => x.status_id,
                        principalTable: "status_vistoria",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_vistorias_veiculos_veiculo_id",
                        column: x => x.veiculo_id,
                        principalTable: "veiculos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fotos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome_arquivo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    extensao = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    evidencia_id = table.Column<int>(type: "integer", nullable: false),
                    vistoria_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_fotos", x => x.id);
                    table.ForeignKey(
                        name: "FK_fotos_evidencias_evidencia_id",
                        column: x => x.evidencia_id,
                        principalTable: "evidencias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_fotos_vistorias_vistoria_id",
                        column: x => x.vistoria_id,
                        principalTable: "vistorias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "inspecoes",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    vistoria_id = table.Column<long>(type: "bigint", nullable: false),
                    item_id = table.Column<int>(type: "integer", nullable: false),
                    status_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_inspecoes", x => x.id);
                    table.ForeignKey(
                        name: "FK_inspecoes_itens_item_id",
                        column: x => x.item_id,
                        principalTable: "itens",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inspecoes_status_inspecao_status_id",
                        column: x => x.status_id,
                        principalTable: "status_inspecao",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inspecoes_vistorias_vistoria_id",
                        column: x => x.vistoria_id,
                        principalTable: "vistorias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_evidencias_nome",
                table: "evidencias",
                column: "nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_fotos_evidencia_id",
                table: "fotos",
                column: "evidencia_id");

            migrationBuilder.CreateIndex(
                name: "i_x_fotos_vistoria_id",
                table: "fotos",
                column: "vistoria_id");

            migrationBuilder.CreateIndex(
                name: "i_x_inspecoes_item_id",
                table: "inspecoes",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "i_x_inspecoes_status_id",
                table: "inspecoes",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "i_x_inspecoes_vistoria_id",
                table: "inspecoes",
                column: "vistoria_id");

            migrationBuilder.CreateIndex(
                name: "IX_itens_nome",
                table: "itens",
                column: "nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_status_inspecao_nome",
                table: "status_inspecao",
                column: "nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_status_vistoria_nome",
                table: "status_vistoria",
                column: "nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_email",
                table: "usuarios",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_veiculos_placa",
                table: "veiculos",
                column: "placa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_vistorias_status_id",
                table: "vistorias",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "i_x_vistorias_veiculo_id",
                table: "vistorias",
                column: "veiculo_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fotos");

            migrationBuilder.DropTable(
                name: "inspecoes");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "evidencias");

            migrationBuilder.DropTable(
                name: "itens");

            migrationBuilder.DropTable(
                name: "status_inspecao");

            migrationBuilder.DropTable(
                name: "vistorias");

            migrationBuilder.DropTable(
                name: "status_vistoria");

            migrationBuilder.DropTable(
                name: "veiculos");
        }
    }
}
