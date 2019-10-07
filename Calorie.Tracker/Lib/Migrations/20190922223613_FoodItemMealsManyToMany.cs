using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lib.Migrations
{
    public partial class FoodItemMealsManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_FoodItems_FoodItemId",
                table: "Meals");

            migrationBuilder.DropIndex(
                name: "IX_Meals_FoodItemId",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "FoodItemId",
                table: "Meals");

            migrationBuilder.CreateTable(
                name: "FoodItemMeal",
                columns: table => new
                {
                    MealId = table.Column<Guid>(nullable: false),
                    FoodItemId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodItemMeal", x => new { x.MealId, x.FoodItemId });
                    table.ForeignKey(
                        name: "FK_FoodItemMeal_FoodItems_FoodItemId",
                        column: x => x.FoodItemId,
                        principalTable: "FoodItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodItemMeal_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodItemMeal_FoodItemId",
                table: "FoodItemMeal",
                column: "FoodItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodItemMeal");

            migrationBuilder.AddColumn<Guid>(
                name: "FoodItemId",
                table: "Meals",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meals_FoodItemId",
                table: "Meals",
                column: "FoodItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_FoodItems_FoodItemId",
                table: "Meals",
                column: "FoodItemId",
                principalTable: "FoodItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
