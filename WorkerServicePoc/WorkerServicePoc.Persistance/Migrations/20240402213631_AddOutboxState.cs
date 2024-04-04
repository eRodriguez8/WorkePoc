using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkerServicePoc.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddOutboxState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "OutboxMessages",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "OutboxMessages");
        }
    }
}
