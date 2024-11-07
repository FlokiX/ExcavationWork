using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TelegramClone.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddRelated2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "UserIds",
                table: "Chats");

            migrationBuilder.AddColumn<Guid>(
                name: "User1Id",
                table: "Chats",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "User2Id",
                table: "Chats",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User1Id",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "User2Id",
                table: "Chats");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Chats",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserIds",
                table: "Chats",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
