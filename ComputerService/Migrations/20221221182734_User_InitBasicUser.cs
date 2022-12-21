using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerService.Migrations
{
    /// <inheritdoc />
    public partial class UserInitBasicUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "IF NOT EXISTS (SELECT Id FROM Users WHERE Id = '683a9aa8-3784-4e0f-a392-0b2dbd3b592f')"
                + "INSERT INTO Users (Id,CreatedAt,FirstName,LastName,Email,Password, PhoneNumber, IsActive, Role, Salt)"
                + $"VALUES ('683a9aa8-3784-4e0f-a392-0b2dbd3b592f', '{DateTimeOffset.Now}', 'User', 'Administrator', 'admin@mail.com', 'LlB2nDpZKpI6jfM6n0HEOIet+IDxu0P9dpLgR1MqpmCxGt2S', '756456783', 'true', 'Administrator', 'LlB2nDpZKpI6jfM6n0HEOA==')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}