using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpDeskPro.Migrations
{
    /// <inheritdoc />
    public partial class AddLanguageCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LanguageCode",
                table: "Users",
                type: "TEXT",
                maxLength: 5,
                nullable: false,
                defaultValue: "en");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LanguageCode",
                table: "Users");
        }
    }
}
