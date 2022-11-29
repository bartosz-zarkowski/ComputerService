using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerService.Migrations
{
    /// <inheritdoc />
    public partial class OrdersRenameColumnReceivedAtToCompletedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReceivedAt",
                table: "Orders",
                newName: "CompletedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CompletedAt",
                table: "Orders",
                newName: "ReceivedAt");
        }
    }
}
