using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class addBankEntitys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Bank_BankId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_BankUser_AspNetUsers_UserId",
                table: "BankUser");

            migrationBuilder.DropForeignKey(
                name: "FK_BankUser_Bank_BankId",
                table: "BankUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankUser",
                table: "BankUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bank",
                table: "Bank");

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

            migrationBuilder.RenameTable(
                name: "BankUser",
                newName: "BankUsers");

            migrationBuilder.RenameTable(
                name: "Bank",
                newName: "Banks");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "AspNetUsers",
                newName: "Nationality");

            migrationBuilder.RenameIndex(
                name: "IX_BankUser_UserId",
                table: "BankUsers",
                newName: "IX_BankUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BankUser_BankId",
                table: "BankUsers",
                newName: "IX_BankUsers_BankId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankUsers",
                table: "BankUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Banks",
                table: "Banks",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "81a84915-a40d-4898-9348-6e4f6a31777f", "3", "CAMGIRL", "CASHIER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9fb04d7b-4d88-46a2-8be0-c36209122126", "2", "USER", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cb243814-5f9d-41b3-8c26-b0c534ad5b86", "1", "ADMIN", "ADMIN" });

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Banks_BankId",
                table: "Accounts",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BankUsers_AspNetUsers_UserId",
                table: "BankUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BankUsers_Banks_BankId",
                table: "BankUsers",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Banks_BankId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_BankUsers_AspNetUsers_UserId",
                table: "BankUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_BankUsers_Banks_BankId",
                table: "BankUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankUsers",
                table: "BankUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Banks",
                table: "Banks");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "81a84915-a40d-4898-9348-6e4f6a31777f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9fb04d7b-4d88-46a2-8be0-c36209122126");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cb243814-5f9d-41b3-8c26-b0c534ad5b86");

            migrationBuilder.RenameTable(
                name: "BankUsers",
                newName: "BankUser");

            migrationBuilder.RenameTable(
                name: "Banks",
                newName: "Bank");

            migrationBuilder.RenameColumn(
                name: "Nationality",
                table: "AspNetUsers",
                newName: "Location");

            migrationBuilder.RenameIndex(
                name: "IX_BankUsers_UserId",
                table: "BankUser",
                newName: "IX_BankUser_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BankUsers_BankId",
                table: "BankUser",
                newName: "IX_BankUser_BankId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankUser",
                table: "BankUser",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bank",
                table: "Bank",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Bank_BankId",
                table: "Accounts",
                column: "BankId",
                principalTable: "Bank",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BankUser_AspNetUsers_UserId",
                table: "BankUser",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BankUser_Bank_BankId",
                table: "BankUser",
                column: "BankId",
                principalTable: "Bank",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
