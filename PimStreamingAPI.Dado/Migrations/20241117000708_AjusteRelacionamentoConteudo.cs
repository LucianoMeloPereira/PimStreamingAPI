using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PimStreamingAPI.Dado.Migrations
{
    public partial class AjusteRelacionamentoConteudo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Remova a tentativa de excluir a chave estrangeira inexistente
            // migrationBuilder.DropForeignKey(
            //     name: "FK_Conteudos_Playlists_PlaylistID",
            //     table: "Conteudos"
            // );

            // Alterar a coluna para permitir valores nulos
            migrationBuilder.AlterColumn<int>(
                name: "PlaylistID",
                table: "Conteudos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: false);

            // Adicionar a chave estrangeira com comportamento 'Restrict' para evitar conflitos
            migrationBuilder.AddForeignKey(
                name: "FK_Conteudos_Playlists_PlaylistID",
                table: "Conteudos",
                column: "PlaylistID",
                principalTable: "Playlists",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conteudos_Playlists_PlaylistID",
                table: "Conteudos");

            migrationBuilder.AlterColumn<int>(
                name: "PlaylistID",
                table: "Conteudos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Conteudos_Playlists_PlaylistID",
                table: "Conteudos",
                column: "PlaylistID",
                principalTable: "Playlists",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
