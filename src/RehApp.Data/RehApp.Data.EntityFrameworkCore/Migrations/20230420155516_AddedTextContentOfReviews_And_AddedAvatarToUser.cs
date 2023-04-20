using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RehApp.Data.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class AddedTextContentOfReviewsAndAddedAvatarToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Reviews",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Reviews",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "AspNetUsers");
        }
    }
}
