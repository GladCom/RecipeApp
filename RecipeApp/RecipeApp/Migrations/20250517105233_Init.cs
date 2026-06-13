using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace RecipeApp.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    ImagePath = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<double>(type: "REAL", nullable: false),
                    Unit = table.Column<int>(type: "INTEGER", nullable: false),
                    Calories = table.Column<float>(type: "REAL", nullable: false),
                    Protein = table.Column<float>(type: "REAL", nullable: false),
                    Fat = table.Column<float>(type: "REAL", nullable: false),
                    Carbs = table.Column<float>(type: "REAL", nullable: false),
                    RecipeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredients_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_RecipeId",
                table: "Ingredients",
                column: "RecipeId");

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                  Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", Npgsql.EntityFrameworkCore.PostgreSQL.Metadata.NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                  Name = table.Column<string>(type: "text", nullable: true),
                  group_id = table.Column<int>(nullable: true),
                  StartDate = table.Column<DateTime>(nullable: true),
                  Content = table.Column<string>(type: "jsonb", nullable: true),
                  ImagePath = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                  table.PrimaryKey("PK_Menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dish",
                columns: table => new
                {
                  Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", Npgsql.EntityFrameworkCore.PostgreSQL.Metadata.NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                  Name = table.Column<string>(type: "text", nullable: true),
                  recipe_id = table.Column<int>(nullable: true),
                  group_id = table.Column<int>(nullable: true),
                  menu_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                  table.PrimaryKey("PK_Dish", x => x.Id);
                  table.ForeignKey(
                        name: "FK_Dish_Menu_menu_id",
                        column: x => x.menu_id,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dish_menu_id",
                table: "Dish",
                column: "menu_id");
    }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Ingredients");
            migrationBuilder.DropTable(name: "Recipes");
            migrationBuilder.DropTable(name: "Dish");
            migrationBuilder.DropTable(name: "Menu");
    }
    }
}
