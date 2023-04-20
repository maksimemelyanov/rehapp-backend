using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RehApp.Data.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDescriptionModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppealsDesc_Appeals_AppealId",
                table: "AppealsDesc");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsersDesc_AspNetUsers_ApplicationUserId",
                table: "ApplicationUsersDesc");

            migrationBuilder.DropForeignKey(
                name: "FK_DiariesDesc_Diaries_DiaryId",
                table: "DiariesDesc");

            migrationBuilder.DropForeignKey(
                name: "FK_ExercisesDesc_Exercises_ExerciseId",
                table: "ExercisesDesc");

            migrationBuilder.DropForeignKey(
                name: "FK_ExercisesParamsDesc_ExercisesParams_ExerciseParamsId",
                table: "ExercisesParamsDesc");

            migrationBuilder.DropForeignKey(
                name: "FK_PostsDesc_Posts_PostId",
                table: "PostsDesc");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewsDesc_Reviews_ReviewId",
                table: "ReviewsDesc");

            migrationBuilder.RenameColumn(
                name: "ReviewId",
                table: "ReviewsDesc",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewsDesc_ReviewId",
                table: "ReviewsDesc",
                newName: "IX_ReviewsDesc_ParentId");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "PostsDesc",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_PostsDesc_PostId",
                table: "PostsDesc",
                newName: "IX_PostsDesc_ParentId");

            migrationBuilder.RenameColumn(
                name: "ExerciseParamsId",
                table: "ExercisesParamsDesc",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_ExercisesParamsDesc_ExerciseParamsId",
                table: "ExercisesParamsDesc",
                newName: "IX_ExercisesParamsDesc_ParentId");

            migrationBuilder.RenameColumn(
                name: "ExerciseId",
                table: "ExercisesDesc",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_ExercisesDesc_ExerciseId",
                table: "ExercisesDesc",
                newName: "IX_ExercisesDesc_ParentId");

            migrationBuilder.RenameColumn(
                name: "DiaryId",
                table: "DiariesDesc",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_DiariesDesc_DiaryId",
                table: "DiariesDesc",
                newName: "IX_DiariesDesc_ParentId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "ApplicationUsersDesc",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUsersDesc_ApplicationUserId",
                table: "ApplicationUsersDesc",
                newName: "IX_ApplicationUsersDesc_ParentId");

            migrationBuilder.RenameColumn(
                name: "AppealId",
                table: "AppealsDesc",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_AppealsDesc_AppealId",
                table: "AppealsDesc",
                newName: "IX_AppealsDesc_ParentId");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Posts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Code",
                table: "DescriptionTypes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_AppealsDesc_Appeals_ParentId",
                table: "AppealsDesc",
                column: "ParentId",
                principalTable: "Appeals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUsersDesc_AspNetUsers_ParentId",
                table: "ApplicationUsersDesc",
                column: "ParentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiariesDesc_Diaries_ParentId",
                table: "DiariesDesc",
                column: "ParentId",
                principalTable: "Diaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExercisesDesc_Exercises_ParentId",
                table: "ExercisesDesc",
                column: "ParentId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExercisesParamsDesc_ExercisesParams_ParentId",
                table: "ExercisesParamsDesc",
                column: "ParentId",
                principalTable: "ExercisesParams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostsDesc_Posts_ParentId",
                table: "PostsDesc",
                column: "ParentId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewsDesc_Reviews_ParentId",
                table: "ReviewsDesc",
                column: "ParentId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppealsDesc_Appeals_ParentId",
                table: "AppealsDesc");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsersDesc_AspNetUsers_ParentId",
                table: "ApplicationUsersDesc");

            migrationBuilder.DropForeignKey(
                name: "FK_DiariesDesc_Diaries_ParentId",
                table: "DiariesDesc");

            migrationBuilder.DropForeignKey(
                name: "FK_ExercisesDesc_Exercises_ParentId",
                table: "ExercisesDesc");

            migrationBuilder.DropForeignKey(
                name: "FK_ExercisesParamsDesc_ExercisesParams_ParentId",
                table: "ExercisesParamsDesc");

            migrationBuilder.DropForeignKey(
                name: "FK_PostsDesc_Posts_ParentId",
                table: "PostsDesc");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewsDesc_Reviews_ParentId",
                table: "ReviewsDesc");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "DescriptionTypes");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "ReviewsDesc",
                newName: "ReviewId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewsDesc_ParentId",
                table: "ReviewsDesc",
                newName: "IX_ReviewsDesc_ReviewId");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "PostsDesc",
                newName: "PostId");

            migrationBuilder.RenameIndex(
                name: "IX_PostsDesc_ParentId",
                table: "PostsDesc",
                newName: "IX_PostsDesc_PostId");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "ExercisesParamsDesc",
                newName: "ExerciseParamsId");

            migrationBuilder.RenameIndex(
                name: "IX_ExercisesParamsDesc_ParentId",
                table: "ExercisesParamsDesc",
                newName: "IX_ExercisesParamsDesc_ExerciseParamsId");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "ExercisesDesc",
                newName: "ExerciseId");

            migrationBuilder.RenameIndex(
                name: "IX_ExercisesDesc_ParentId",
                table: "ExercisesDesc",
                newName: "IX_ExercisesDesc_ExerciseId");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "DiariesDesc",
                newName: "DiaryId");

            migrationBuilder.RenameIndex(
                name: "IX_DiariesDesc_ParentId",
                table: "DiariesDesc",
                newName: "IX_DiariesDesc_DiaryId");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "ApplicationUsersDesc",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUsersDesc_ParentId",
                table: "ApplicationUsersDesc",
                newName: "IX_ApplicationUsersDesc_ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "AppealsDesc",
                newName: "AppealId");

            migrationBuilder.RenameIndex(
                name: "IX_AppealsDesc_ParentId",
                table: "AppealsDesc",
                newName: "IX_AppealsDesc_AppealId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppealsDesc_Appeals_AppealId",
                table: "AppealsDesc",
                column: "AppealId",
                principalTable: "Appeals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUsersDesc_AspNetUsers_ApplicationUserId",
                table: "ApplicationUsersDesc",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiariesDesc_Diaries_DiaryId",
                table: "DiariesDesc",
                column: "DiaryId",
                principalTable: "Diaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExercisesDesc_Exercises_ExerciseId",
                table: "ExercisesDesc",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExercisesParamsDesc_ExercisesParams_ExerciseParamsId",
                table: "ExercisesParamsDesc",
                column: "ExerciseParamsId",
                principalTable: "ExercisesParams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostsDesc_Posts_PostId",
                table: "PostsDesc",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewsDesc_Reviews_ReviewId",
                table: "ReviewsDesc",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
