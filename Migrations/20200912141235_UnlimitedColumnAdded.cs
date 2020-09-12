using Microsoft.EntityFrameworkCore.Migrations;

namespace omission.api.Migrations
{
    public partial class UnlimitedColumnAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "unlimited",
                table: "Users",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "unlimited",
                table: "Users");
        }
    }
}
