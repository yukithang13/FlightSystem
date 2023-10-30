using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightSystem.Migrations
{
    public partial class createdbfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupInfo_DocumentInfos_DocumentInfoDocumentId",
                table: "GroupInfo");

            migrationBuilder.DropIndex(
                name: "IX_GroupInfo_DocumentInfoDocumentId",
                table: "GroupInfo");

            migrationBuilder.DropColumn(
                name: "DocumentInfoDocumentId",
                table: "GroupInfo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DocumentInfoDocumentId",
                table: "GroupInfo",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupInfo_DocumentInfoDocumentId",
                table: "GroupInfo",
                column: "DocumentInfoDocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupInfo_DocumentInfos_DocumentInfoDocumentId",
                table: "GroupInfo",
                column: "DocumentInfoDocumentId",
                principalTable: "DocumentInfos",
                principalColumn: "DocumentId");
        }
    }
}
