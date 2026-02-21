using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace app.Migrations
{
    /// <inheritdoc />
    public partial class AddUserContentProgress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Content_Users_UserId",
                table: "Content");

            migrationBuilder.DropForeignKey(
                name: "FK_MembershipContent_Content_ContentId",
                table: "MembershipContent");

            migrationBuilder.DropForeignKey(
                name: "FK_MembershipContent_Membership_MembershipId",
                table: "MembershipContent");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscription_Membership_MembershipId",
                table: "Subscription");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscription_Users_UserId",
                table: "Subscription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subscription",
                table: "Subscription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MembershipContent",
                table: "MembershipContent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Membership",
                table: "Membership");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Content",
                table: "Content");

            migrationBuilder.RenameTable(
                name: "Subscription",
                newName: "Subscriptions");

            migrationBuilder.RenameTable(
                name: "MembershipContent",
                newName: "MembershipContents");

            migrationBuilder.RenameTable(
                name: "Membership",
                newName: "Memberships");

            migrationBuilder.RenameTable(
                name: "Content",
                newName: "Contents");

            migrationBuilder.RenameIndex(
                name: "IX_Subscription_UserId",
                table: "Subscriptions",
                newName: "IX_Subscriptions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Subscription_MembershipId",
                table: "Subscriptions",
                newName: "IX_Subscriptions_MembershipId");

            migrationBuilder.RenameIndex(
                name: "IX_MembershipContent_MembershipId",
                table: "MembershipContents",
                newName: "IX_MembershipContents_MembershipId");

            migrationBuilder.RenameIndex(
                name: "IX_MembershipContent_ContentId",
                table: "MembershipContents",
                newName: "IX_MembershipContents_ContentId");

            migrationBuilder.RenameIndex(
                name: "IX_Content_UserId",
                table: "Contents",
                newName: "IX_Contents_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subscriptions",
                table: "Subscriptions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MembershipContents",
                table: "MembershipContents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Memberships",
                table: "Memberships",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contents",
                table: "Contents",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UserContentProgresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ContentId = table.Column<int>(type: "integer", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserContentProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserContentProgresses_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserContentProgresses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserContentProgresses_ContentId",
                table: "UserContentProgresses",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserContentProgresses_UserId",
                table: "UserContentProgresses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Users_UserId",
                table: "Contents",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MembershipContents_Contents_ContentId",
                table: "MembershipContents",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MembershipContents_Memberships_MembershipId",
                table: "MembershipContents",
                column: "MembershipId",
                principalTable: "Memberships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Memberships_MembershipId",
                table: "Subscriptions",
                column: "MembershipId",
                principalTable: "Memberships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Users_UserId",
                table: "Subscriptions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Users_UserId",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_MembershipContents_Contents_ContentId",
                table: "MembershipContents");

            migrationBuilder.DropForeignKey(
                name: "FK_MembershipContents_Memberships_MembershipId",
                table: "MembershipContents");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Memberships_MembershipId",
                table: "Subscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Users_UserId",
                table: "Subscriptions");

            migrationBuilder.DropTable(
                name: "UserContentProgresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subscriptions",
                table: "Subscriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Memberships",
                table: "Memberships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MembershipContents",
                table: "MembershipContents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contents",
                table: "Contents");

            migrationBuilder.RenameTable(
                name: "Subscriptions",
                newName: "Subscription");

            migrationBuilder.RenameTable(
                name: "Memberships",
                newName: "Membership");

            migrationBuilder.RenameTable(
                name: "MembershipContents",
                newName: "MembershipContent");

            migrationBuilder.RenameTable(
                name: "Contents",
                newName: "Content");

            migrationBuilder.RenameIndex(
                name: "IX_Subscriptions_UserId",
                table: "Subscription",
                newName: "IX_Subscription_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Subscriptions_MembershipId",
                table: "Subscription",
                newName: "IX_Subscription_MembershipId");

            migrationBuilder.RenameIndex(
                name: "IX_MembershipContents_MembershipId",
                table: "MembershipContent",
                newName: "IX_MembershipContent_MembershipId");

            migrationBuilder.RenameIndex(
                name: "IX_MembershipContents_ContentId",
                table: "MembershipContent",
                newName: "IX_MembershipContent_ContentId");

            migrationBuilder.RenameIndex(
                name: "IX_Contents_UserId",
                table: "Content",
                newName: "IX_Content_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subscription",
                table: "Subscription",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Membership",
                table: "Membership",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MembershipContent",
                table: "MembershipContent",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Content",
                table: "Content",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Content_Users_UserId",
                table: "Content",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MembershipContent_Content_ContentId",
                table: "MembershipContent",
                column: "ContentId",
                principalTable: "Content",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MembershipContent_Membership_MembershipId",
                table: "MembershipContent",
                column: "MembershipId",
                principalTable: "Membership",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscription_Membership_MembershipId",
                table: "Subscription",
                column: "MembershipId",
                principalTable: "Membership",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscription_Users_UserId",
                table: "Subscription",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
