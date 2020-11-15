using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessCare.Data.Migrations
{
    public partial class AddCommentUserUsername : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserUsername",
                table: "Comments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserUsername",
                table: "Comments");
        }
    }
}
