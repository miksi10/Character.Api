using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CharacterApi.Migrations
{
    /// <inheritdoc />
    public partial class addingConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Class_Name",
                table: "Class",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Class_Name",
                table: "Class");
        }
    }
}
