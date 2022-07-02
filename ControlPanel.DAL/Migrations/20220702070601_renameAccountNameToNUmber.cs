using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlPanel.DAL.Migrations
{
    public partial class renameAccountNameToNUmber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccountName",
                table: "UserAccounts",
                newName: "AccountNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccountNumber",
                table: "UserAccounts",
                newName: "AccountName");
        }
    }
}
