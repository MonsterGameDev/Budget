using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Budget.Api.Migrations
{
    public partial class addedPostingLine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostingLines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 100, nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    SubAccountId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostingLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostingLines_SubAccounts_SubAccountId",
                        column: x => x.SubAccountId,
                        principalTable: "SubAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostingLines_SubAccountId",
                table: "PostingLines",
                column: "SubAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostingLines");
        }
    }
}
