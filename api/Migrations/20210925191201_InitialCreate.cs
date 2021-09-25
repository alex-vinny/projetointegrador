using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Perfil = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Idade = table.Column<int>(type: "int", nullable: true),
                    Sexo = table.Column<int>(type: "int", nullable: true),
                    SerieEscolar = table.Column<int>(type: "int", nullable: true),
                    Disciplina = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Palavras",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Valor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dica = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoriaID = table.Column<int>(type: "int", nullable: true)
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
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TamanhoX = table.Column<int>(type: "int", nullable: false),
                    TamanhoY = table.Column<int>(type: "int", nullable: false),
                    Criacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AutorID = table.Column<int>(type: "int", nullable: true),
                    CategoriaID = table.Column<int>(type: "int", nullable: true)
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
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CruzadaID = table.Column<int>(type: "int", nullable: true),
                    PalavraID = table.Column<int>(type: "int", nullable: true),
                    PosicaoX = table.Column<int>(type: "int", nullable: false),
                    PosicaoY = table.Column<int>(type: "int", nullable: false),
                    Orientacao = table.Column<int>(type: "int", nullable: false)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CruzadaItens");

            migrationBuilder.DropTable(
                name: "Cruzadas");

            migrationBuilder.DropTable(
                name: "Palavras");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
