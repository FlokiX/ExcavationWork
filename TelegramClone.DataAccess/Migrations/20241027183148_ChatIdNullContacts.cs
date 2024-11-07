using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TelegramClone.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChatIdNullContacts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Contacts_ChatId",
                table: "Contacts");

            migrationBuilder.AlterColumn<Guid>(
                name: "ChatId",
                table: "Contacts",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ChatId",
                table: "Contacts",
                column: "ChatId",
                unique: true,
                filter: "[ChatId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Contacts_ChatId",
                table: "Contacts");

            migrationBuilder.AlterColumn<Guid>(
                name: "ChatId",
                table: "Contacts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ChatId",
                table: "Contacts",
                column: "ChatId",
                unique: true);
        }
    }
}
