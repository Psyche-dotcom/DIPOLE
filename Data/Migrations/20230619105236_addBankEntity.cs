using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class addBankEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "236ce830-88f4-4836-8843-a21c157b8266");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "44f0bb3e-1d13-4a48-b278-abf4af7650aa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "781b45fc-18b2-4feb-9ebc-59f42e8e163a");

            migrationBuilder.AlterColumn<long>(
                name: "AccountBalance",
                table: "Accounts",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "BankId",
                table: "Accounts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Bank",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bank", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BankId = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankUser_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankUser_Bank_BankId",
                        column: x => x.BankId,
                        principalTable: "Bank",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0faf2054-d515-40c0-a993-9bb4e34b5e21", "2", "USER", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a71cb713-74b3-4837-bb33-5080093d13a0", "3", "CAMGIRL", "CASHIER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c5ce4305-9b55-46b2-9c74-c3fb85c1c86f", "1", "ADMIN", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_BankId",
                table: "Accounts",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_BankUser_BankId",
                table: "BankUser",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_BankUser_UserId",
                table: "BankUser",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Bank_BankId",
                table: "Accounts",
                column: "BankId",
                principalTable: "Bank",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Bank_BankId",
                table: "Accounts");

            migrationBuilder.DropTable(
                name: "BankUser");

            migrationBuilder.DropTable(
                name: "Bank");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_BankId",
                table: "Accounts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0faf2054-d515-40c0-a993-9bb4e34b5e21");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a71cb713-74b3-4837-bb33-5080093d13a0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c5ce4305-9b55-46b2-9c74-c3fb85c1c86f");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "Accounts");

            migrationBuilder.AlterColumn<string>(
                name: "AccountBalance",
                table: "Accounts",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "236ce830-88f4-4836-8843-a21c157b8266", "1", "ADMIN", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "44f0bb3e-1d13-4a48-b278-abf4af7650aa", "2", "USER", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "781b45fc-18b2-4feb-9ebc-59f42e8e163a", "3", "CAMGIRL", "CASHIER" });
        }
    }
}
