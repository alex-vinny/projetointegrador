using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ProjetoIntegrador.Api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descricao = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Nome = table.Column<string>(type: "text", nullable: true),
                    Senha = table.Column<string>(type: "text", nullable: true),
                    Perfil = table.Column<int>(type: "integer", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    Idade = table.Column<int>(type: "integer", nullable: true),
                    Sexo = table.Column<int>(type: "integer", nullable: true),
                    SerieEscolar = table.Column<int>(type: "integer", nullable: true),
                    Disciplina = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Palavras",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Valor = table.Column<string>(type: "text", nullable: true),
                    Dica = table.Column<string>(type: "text", nullable: true),
                    CategoriaID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Palavras", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Palavras_Categorias_CategoriaID",
                        column: x => x.CategoriaID,
                        principalTable: "Categorias",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cruzadas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TamanhoX = table.Column<int>(type: "integer", nullable: false),
                    TamanhoY = table.Column<int>(type: "integer", nullable: false),
                    Criacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    AutorID = table.Column<int>(type: "integer", nullable: true),
                    CategoriaID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cruzadas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cruzadas_Categorias_CategoriaID",
                        column: x => x.CategoriaID,
                        principalTable: "Categorias",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cruzadas_Usuarios_AutorID",
                        column: x => x.AutorID,
                        principalTable: "Usuarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CruzadaItens",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PalavraID = table.Column<int>(type: "integer", nullable: true),
                    PosicaoX = table.Column<int>(type: "integer", nullable: false),
                    PosicaoY = table.Column<int>(type: "integer", nullable: false),
                    Orientacao = table.Column<int>(type: "integer", nullable: false),
                    CruzadaID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CruzadaItens", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CruzadaItens_Cruzadas_CruzadaID",
                        column: x => x.CruzadaID,
                        principalTable: "Cruzadas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CruzadaItens_Palavras_PalavraID",
                        column: x => x.PalavraID,
                        principalTable: "Palavras",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sessoes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Inicio = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Fim = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CruzadaID = table.Column<int>(type: "integer", nullable: true),
                    Acertos = table.Column<int>(type: "integer", nullable: true),
                    UsuarioID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessoes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Sessoes_Cruzadas_CruzadaID",
                        column: x => x.CruzadaID,
                        principalTable: "Cruzadas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sessoes_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_Descricao",
                table: "Categorias",
                column: "Descricao",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CruzadaItens_CruzadaID",
                table: "CruzadaItens",
                column: "CruzadaID");

            migrationBuilder.CreateIndex(
                name: "IX_CruzadaItens_PalavraID",
                table: "CruzadaItens",
                column: "PalavraID");

            migrationBuilder.CreateIndex(
                name: "IX_Cruzadas_AutorID",
                table: "Cruzadas",
                column: "AutorID");

            migrationBuilder.CreateIndex(
                name: "IX_Cruzadas_CategoriaID",
                table: "Cruzadas",
                column: "CategoriaID");

            migrationBuilder.CreateIndex(
                name: "IX_Palavras_CategoriaID",
                table: "Palavras",
                column: "CategoriaID");

            migrationBuilder.CreateIndex(
                name: "IX_Palavras_Valor",
                table: "Palavras",
                column: "Valor",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sessoes_CruzadaID",
                table: "Sessoes",
                column: "CruzadaID");

            migrationBuilder.CreateIndex(
                name: "IX_Sessoes_UsuarioID",
                table: "Sessoes",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email_Nome",
                table: "Usuarios",
                columns: new[] { "Email", "Nome" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CruzadaItens");

            migrationBuilder.DropTable(
                name: "Sessoes");

            migrationBuilder.DropTable(
                name: "Palavras");

            migrationBuilder.DropTable(
                name: "Cruzadas");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
