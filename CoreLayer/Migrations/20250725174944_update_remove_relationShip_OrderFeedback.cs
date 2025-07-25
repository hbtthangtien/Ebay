using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreLayer.Migrations
{
    /// <inheritdoc />
    public partial class update_remove_relationShip_OrderFeedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_OrderTable_OrderId",
                table: "Feedback");

            migrationBuilder.DropIndex(
                name: "IX_Feedback_OrderId",
                table: "Feedback");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Feedback");

            migrationBuilder.AddColumn<int>(
                name: "OrdersId",
                table: "Feedback",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_OrdersId",
                table: "Feedback",
                column: "OrdersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_OrderTable_OrdersId",
                table: "Feedback",
                column: "OrdersId",
                principalTable: "OrderTable",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_OrderTable_OrdersId",
                table: "Feedback");

            migrationBuilder.DropIndex(
                name: "IX_Feedback_OrdersId",
                table: "Feedback");

            migrationBuilder.DropColumn(
                name: "OrdersId",
                table: "Feedback");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Feedback",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_OrderId",
                table: "Feedback",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_OrderTable_OrderId",
                table: "Feedback",
                column: "OrderId",
                principalTable: "OrderTable",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
