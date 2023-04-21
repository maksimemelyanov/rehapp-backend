using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RehApp.Data.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedAppealEntityAndAddedCreationDatesInDescriptionEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "ReviewsDesc",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "PostsDesc",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "ExercisesParamsDesc",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "ExercisesDesc",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "DiariesDesc",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "DescriptionTypes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "ApplicationUsersDesc",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "AppealsDesc",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Appeals",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Appeals",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Appeals",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appeals_AuthorId",
                table: "Appeals",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appeals_AspNetUsers_AuthorId",
                table: "Appeals",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appeals_AspNetUsers_AuthorId",
                table: "Appeals");

            migrationBuilder.DropIndex(
                name: "IX_Appeals_AuthorId",
                table: "Appeals");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "ReviewsDesc");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "PostsDesc");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "ExercisesParamsDesc");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "ExercisesDesc");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "DiariesDesc");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "DescriptionTypes");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "ApplicationUsersDesc");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "AppealsDesc");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Appeals");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Appeals");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Appeals");
        }
    }
}
