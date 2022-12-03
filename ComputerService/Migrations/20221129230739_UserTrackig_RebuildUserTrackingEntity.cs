using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerService.Migrations
{
    /// <inheritdoc />
    public partial class UserTrackigRebuildUserTrackingEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTrackings_Users_UserId",
                table: "UserTrackings");

            migrationBuilder.DropIndex(
                name: "IX_UserTrackings_UserId",
                table: "UserTrackings");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserTrackings",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Action",
                table: "UserTrackings",
                newName: "TrackingActionType");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Date",
                table: "UserTrackings",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "ActionTargetId",
                table: "UserTrackings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "UserTrackings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "UserTrackings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "UserTrackings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTrackings",
                table: "UserTrackings",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTrackings",
                table: "UserTrackings");

            migrationBuilder.DropColumn(
                name: "ActionTargetId",
                table: "UserTrackings");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "UserTrackings");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "UserTrackings");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "UserTrackings");

            migrationBuilder.RenameColumn(
                name: "TrackingActionType",
                table: "UserTrackings",
                newName: "Action");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserTrackings",
                newName: "UserId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "UserTrackings",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.CreateIndex(
                name: "IX_UserTrackings_UserId",
                table: "UserTrackings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTrackings_Users_UserId",
                table: "UserTrackings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
