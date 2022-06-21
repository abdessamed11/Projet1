using Microsoft.EntityFrameworkCore.Migrations;

namespace Projet1.Migrations
{
    public partial class add_articless : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_articles_formations_formationId",
                table: "tb_articles");

            migrationBuilder.RenameColumn(
                name: "formationId",
                table: "tb_articles",
                newName: "formation_Id");

            migrationBuilder.RenameIndex(
                name: "IX_tb_articles_formationId",
                table: "tb_articles",
                newName: "IX_tb_articles_formation_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_articles_formations_formation_Id",
                table: "tb_articles",
                column: "formation_Id",
                principalTable: "formations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_articles_formations_formation_Id",
                table: "tb_articles");

            migrationBuilder.RenameColumn(
                name: "formation_Id",
                table: "tb_articles",
                newName: "formationId");

            migrationBuilder.RenameIndex(
                name: "IX_tb_articles_formation_Id",
                table: "tb_articles",
                newName: "IX_tb_articles_formationId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_articles_formations_formationId",
                table: "tb_articles",
                column: "formationId",
                principalTable: "formations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
