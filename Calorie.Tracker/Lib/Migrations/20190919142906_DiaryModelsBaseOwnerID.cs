using Microsoft.EntityFrameworkCore.Migrations;

namespace Lib.Migrations
{
    public partial class DiaryModelsBaseOwnerID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiaryEntries_Users_OwnedById",
                table: "DiaryEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Users_OwnedById",
                table: "Meals");

            migrationBuilder.AlterColumn<string>(
                name: "OwnedById",
                table: "Meals",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "OwnedById",
                table: "DiaryEntries",
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OwnedById",
                table: "Meals",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "OwnedById",
                table: "DiaryEntries",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_DiaryEntries_Users_OwnedById",
                table: "DiaryEntries",
                column: "OwnedById",
                principalTable: "Users",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Users_OwnedById",
                table: "Meals",
                column: "OwnedById",
                principalTable: "Users",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
