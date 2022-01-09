using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopOnline.Data.Migrations
{
    public partial class UpdateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_Product_IdProduct",
                table: "OrderDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductType_Brand_IdBrand",
                table: "ProductType");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "FavoriteProduct");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropIndex(
                name: "IX_ProductType_IdBrand",
                table: "ProductType");

            migrationBuilder.DropColumn(
                name: "IdBrand",
                table: "ProductType");

            migrationBuilder.RenameColumn(
                name: "IdProduct",
                table: "OrderDetail",
                newName: "IdProductDetail");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetail_IdProduct",
                table: "OrderDetail",
                newName: "IX_OrderDetail_IdProductDetail");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductType",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "ProductDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ProductDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_ProductDetail_IdProductDetail",
                table: "OrderDetail",
                column: "IdProductDetail",
                principalTable: "ProductDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_ProductDetail_IdProductDetail",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "ProductDetail");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ProductDetail");

            migrationBuilder.RenameColumn(
                name: "IdProductDetail",
                table: "OrderDetail",
                newName: "IdProduct");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetail_IdProductDetail",
                table: "OrderDetail",
                newName: "IX_OrderDetail_IdProduct");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductType",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "IdBrand",
                table: "ProductType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCustomer = table.Column<int>(type: "int", nullable: false),
                    IdProductDetail = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteProduct_Customer_IdCustomer",
                        column: x => x.IdCustomer,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteProduct_ProductDetail_IdProductDetail",
                        column: x => x.IdProductDetail,
                        principalTable: "ProductDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProductDetail = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_ProductDetail_IdProductDetail",
                        column: x => x.IdProductDetail,
                        principalTable: "ProductDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductType_IdBrand",
                table: "ProductType",
                column: "IdBrand");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteProduct_IdCustomer",
                table: "FavoriteProduct",
                column: "IdCustomer");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteProduct_IdProductDetail",
                table: "FavoriteProduct",
                column: "IdProductDetail");

            migrationBuilder.CreateIndex(
                name: "IX_Product_IdProductDetail",
                table: "Product",
                column: "IdProductDetail");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_Product_IdProduct",
                table: "OrderDetail",
                column: "IdProduct",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductType_Brand_IdBrand",
                table: "ProductType",
                column: "IdBrand",
                principalTable: "Brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
