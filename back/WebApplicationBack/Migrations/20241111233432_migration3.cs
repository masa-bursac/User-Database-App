using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationBack.Migrations
{
    public partial class migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAEAACcQAAAAEAzjJqJFLYnuSy+pPFLyVi+aFpl96enyKanRGZxDNwJJDy/qf2+FDuO8f4T7sdfWog==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "admin");
        }
    }
}
