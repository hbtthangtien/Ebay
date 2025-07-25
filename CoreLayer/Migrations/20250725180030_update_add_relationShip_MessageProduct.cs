using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreLayer.Migrations
{
    /// <inheritdoc />
    public partial class update_add_relationShip_MessageProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Message",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Message_ProductId",
                table: "Message",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Product_ProductId",
                table: "Message",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_Product_ProductId",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_ProductId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Message");
        }
    }
}
