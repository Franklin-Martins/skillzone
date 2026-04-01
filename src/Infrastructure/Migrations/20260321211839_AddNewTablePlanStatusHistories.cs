using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTablePlanStatusHistories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanStatusHistory_Plans_PlanId",
                table: "PlanStatusHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanStatusHistory",
                table: "PlanStatusHistory");

            migrationBuilder.RenameTable(
                name: "PlanStatusHistory",
                newName: "PlanStatusHistories");

            migrationBuilder.RenameIndex(
                name: "IX_PlanStatusHistory_PlanId",
                table: "PlanStatusHistories",
                newName: "IX_PlanStatusHistories_PlanId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PlanStatusHistories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OwnerEmail",
                table: "PlanStatusHistories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "PlanStatusHistories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanStatusHistories",
                table: "PlanStatusHistories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanStatusHistories_Plans_PlanId",
                table: "PlanStatusHistories",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanStatusHistories_Plans_PlanId",
                table: "PlanStatusHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanStatusHistories",
                table: "PlanStatusHistories");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "PlanStatusHistories");

            migrationBuilder.DropColumn(
                name: "OwnerEmail",
                table: "PlanStatusHistories");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "PlanStatusHistories");

            migrationBuilder.RenameTable(
                name: "PlanStatusHistories",
                newName: "PlanStatusHistory");

            migrationBuilder.RenameIndex(
                name: "IX_PlanStatusHistories_PlanId",
                table: "PlanStatusHistory",
                newName: "IX_PlanStatusHistory_PlanId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanStatusHistory",
                table: "PlanStatusHistory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanStatusHistory_Plans_PlanId",
                table: "PlanStatusHistory",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
