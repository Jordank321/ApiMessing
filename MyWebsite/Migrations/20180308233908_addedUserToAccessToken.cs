using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyWebsite.Migrations
{
    public partial class addedUserToAccessToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "AccessTokens",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccessTokens_UserId",
                table: "AccessTokens",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessTokens_Users_UserId",
                table: "AccessTokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessTokens_Users_UserId",
                table: "AccessTokens");

            migrationBuilder.DropIndex(
                name: "IX_AccessTokens_UserId",
                table: "AccessTokens");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AccessTokens");
        }
    }
}
