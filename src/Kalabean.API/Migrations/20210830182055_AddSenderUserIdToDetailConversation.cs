using Microsoft.EntityFrameworkCore.Migrations;

namespace Kalabean.API.Migrations
{
    public partial class AddSenderUserIdToDetailConversation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SenderUserId",
                table: "ConversationDetail",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ConversationDetail_SenderUserId",
                table: "ConversationDetail",
                column: "SenderUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversation_RequirementId",
                table: "Conversation",
                column: "RequirementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversation_Requirement_RequirementId",
                table: "Conversation",
                column: "RequirementId",
                principalTable: "Requirement",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationDetail_AspNetUsers_SenderUserId",
                table: "ConversationDetail",
                column: "SenderUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversation_Requirement_RequirementId",
                table: "Conversation");

            migrationBuilder.DropForeignKey(
                name: "FK_ConversationDetail_AspNetUsers_SenderUserId",
                table: "ConversationDetail");

            migrationBuilder.DropIndex(
                name: "IX_ConversationDetail_SenderUserId",
                table: "ConversationDetail");

            migrationBuilder.DropIndex(
                name: "IX_Conversation_RequirementId",
                table: "Conversation");

            migrationBuilder.DropColumn(
                name: "SenderUserId",
                table: "ConversationDetail");
        }
    }
}
