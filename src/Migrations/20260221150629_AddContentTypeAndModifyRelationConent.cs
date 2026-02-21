using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace app.Migrations
{
    /// <inheritdoc />
    public partial class AddContentTypeAndModifyRelationConent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContentTypeId",
                table: "Contents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ContentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contents_ContentTypeId",
                table: "Contents",
                column: "ContentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_ContentTypes_ContentTypeId",
                table: "Contents",
                column: "ContentTypeId",
                principalTable: "ContentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_ContentTypes_ContentTypeId",
                table: "Contents");

            migrationBuilder.DropTable(
                name: "ContentTypes");

            migrationBuilder.DropIndex(
                name: "IX_Contents_ContentTypeId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "ContentTypeId",
                table: "Contents");
        }
    }
}
