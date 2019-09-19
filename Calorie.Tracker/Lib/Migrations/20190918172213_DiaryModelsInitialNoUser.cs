using Microsoft.EntityFrameworkCore.Migrations;

namespace Lib.Migrations
{
    public partial class DiaryModelsInitialNoUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodItems_Users_OwnedById",
                table: "FoodItems");

            migrationBuilder.AlterColumn<string>(
                name: "OwnedById",
                table: "FoodItems",
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OwnedById",
                table: "FoodItems",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_FoodItems_Users_OwnedById",
                table: "FoodItems",
                column: "OwnedById",
                principalTable: "Users",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
