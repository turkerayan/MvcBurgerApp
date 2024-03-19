using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCGrup2.Migrations
{
    /// <inheritdoc />
    public partial class mig5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Product_BurgerId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Product_DrinkId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Product_ExtraMatId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Carts_CartId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Carts_BurgerId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_DrinkId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_ExtraMatId",
                table: "Carts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_CartId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "ExtraMats");

            migrationBuilder.AlterColumn<int>(
                name: "ExtraMatId",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DrinkId",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BurgerId",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExtraMats",
                table: "ExtraMats",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Burgers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Burgers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CartExtraMat",
                columns: table => new
                {
                    CartsId = table.Column<int>(type: "int", nullable: false),
                    ExtraMatsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartExtraMat", x => new { x.CartsId, x.ExtraMatsId });
                    table.ForeignKey(
                        name: "FK_CartExtraMat_Carts_CartsId",
                        column: x => x.CartsId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartExtraMat_ExtraMats_ExtraMatsId",
                        column: x => x.ExtraMatsId,
                        principalTable: "ExtraMats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Drinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drinks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BurgerCart",
                columns: table => new
                {
                    BurgersId = table.Column<int>(type: "int", nullable: false),
                    CartsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BurgerCart", x => new { x.BurgersId, x.CartsId });
                    table.ForeignKey(
                        name: "FK_BurgerCart_Burgers_BurgersId",
                        column: x => x.BurgersId,
                        principalTable: "Burgers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BurgerCart_Carts_CartsId",
                        column: x => x.CartsId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartDrink",
                columns: table => new
                {
                    CartsId = table.Column<int>(type: "int", nullable: false),
                    DrinksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartDrink", x => new { x.CartsId, x.DrinksId });
                    table.ForeignKey(
                        name: "FK_CartDrink_Carts_CartsId",
                        column: x => x.CartsId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartDrink_Drinks_DrinksId",
                        column: x => x.DrinksId,
                        principalTable: "Drinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BurgerCart_CartsId",
                table: "BurgerCart",
                column: "CartsId");

            migrationBuilder.CreateIndex(
                name: "IX_CartDrink_DrinksId",
                table: "CartDrink",
                column: "DrinksId");

            migrationBuilder.CreateIndex(
                name: "IX_CartExtraMat_ExtraMatsId",
                table: "CartExtraMat",
                column: "ExtraMatsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BurgerCart");

            migrationBuilder.DropTable(
                name: "CartDrink");

            migrationBuilder.DropTable(
                name: "CartExtraMat");

            migrationBuilder.DropTable(
                name: "Burgers");

            migrationBuilder.DropTable(
                name: "Drinks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExtraMats",
                table: "ExtraMats");

            migrationBuilder.RenameTable(
                name: "ExtraMats",
                newName: "Product");

            migrationBuilder.AlterColumn<int>(
                name: "ExtraMatId",
                table: "Carts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DrinkId",
                table: "Carts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BurgerId",
                table: "Carts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Product",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_BurgerId",
                table: "Carts",
                column: "BurgerId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_DrinkId",
                table: "Carts",
                column: "DrinkId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ExtraMatId",
                table: "Carts",
                column: "ExtraMatId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CartId",
                table: "Product",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Product_BurgerId",
                table: "Carts",
                column: "BurgerId",
                principalTable: "Product",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Product_DrinkId",
                table: "Carts",
                column: "DrinkId",
                principalTable: "Product",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Product_ExtraMatId",
                table: "Carts",
                column: "ExtraMatId",
                principalTable: "Product",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Carts_CartId",
                table: "Product",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id");
        }
    }
}
