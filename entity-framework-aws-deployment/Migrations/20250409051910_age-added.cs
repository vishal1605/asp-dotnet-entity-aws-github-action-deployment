using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace entity_framework_aws_deployment.Migrations
{
    /// <inheritdoc />
    public partial class ageadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Age",
                table: "UserDetails",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "UserDetails");
        }
    }
}
