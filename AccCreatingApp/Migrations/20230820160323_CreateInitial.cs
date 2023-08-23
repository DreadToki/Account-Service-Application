using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccCreatingApp.Migrations
{
    /// <inheritdoc />
    public partial class CreateInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Incident",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incident", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    IncidentName = table.Column<string>(type: "nvarchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Account_Incident_IncidentName",
                        column: x => x.IncidentName,
                        principalTable: "Incident",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Email);
                    table.ForeignKey(
                        name: "FK_Contact_Account_AccountName",
                        column: x => x.AccountName,
                        principalTable: "Account",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_IncidentName",
                table: "Account",
                column: "IncidentName");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_AccountName",
                table: "Contact",
                column: "AccountName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Incident");
        }
    }
}
