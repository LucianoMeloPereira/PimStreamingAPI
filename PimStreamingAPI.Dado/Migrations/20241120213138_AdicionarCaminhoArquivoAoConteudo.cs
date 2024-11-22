using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PimStreamingAPI.Dado.Migrations
{
    public partial class AdicionarCaminhoArquivoAoConteudo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CaminhoArquivo",
                table: "Conteudos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaminhoArquivo",
                table: "Conteudos");
        }
    }
}
