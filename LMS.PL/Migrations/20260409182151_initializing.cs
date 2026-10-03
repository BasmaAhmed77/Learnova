using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.PL.Migrations
{
    /// <inheritdoc />
    public partial class initializing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isVideo",
                table: "Lessons");

            migrationBuilder.AddColumn<string>(
                name: "videoID",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "imageURL",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "videoID",
                table: "Lessons");

            migrationBuilder.AddColumn<bool>(
                name: "isVideo",
                table: "Lessons",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "imageURL",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
