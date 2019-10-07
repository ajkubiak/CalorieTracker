using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lib.Migrations
{
    public partial class DiaryModelsNewRelationship2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodItems_Meals_MealId",
                table: "FoodItems");

            migrationBuilder.DropIndex(
                name: "IX_FoodItems_MealId",
                table: "FoodItems");

            migrationBuilder.DropColumn(
                name: "MealId",
                table: "FoodItems");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "MealId",
                table: "FoodItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FoodItems_MealId",
                table: "FoodItems",
                column: "MealId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodItems_Meals_MealId",
                table: "FoodItems",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
