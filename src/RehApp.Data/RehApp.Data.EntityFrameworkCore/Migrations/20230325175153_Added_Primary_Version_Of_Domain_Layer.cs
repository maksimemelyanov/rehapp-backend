using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RehApp.Data.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class AddedPrimaryVersionOfDomainLayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExtAuthInfo_AspNetUsers_ApplicationUserId",
                table: "ExtAuthInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExtAuthInfo",
                table: "ExtAuthInfo");

            migrationBuilder.RenameTable(
                name: "ExtAuthInfo",
                newName: "ExtAuthInfos");

            migrationBuilder.RenameIndex(
                name: "IX_ExtAuthInfo_ApplicationUserId",
                table: "ExtAuthInfos",
                newName: "IX_ExtAuthInfos_ApplicationUserId");

            migrationBuilder.AddColumn<bool>(
                name: "Blocked",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "CityId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Verified",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExtAuthInfos",
                table: "ExtAuthInfos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Appeals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appeals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ShortName = table.Column<string>(type: "text", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: true),
                    Latitude = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DescriptionTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    BeginDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DescriptionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Diaries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diaries_AspNetUsers_PatientId",
                        column: x => x.PatientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiaryEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DiaryId = table.Column<Guid>(type: "uuid", nullable: false),
                    TrainingId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Pulse = table.Column<int>(type: "integer", nullable: false),
                    PressureUpperBloodPressure = table.Column<int>(name: "Pressure_UpperBloodPressure", type: "integer", nullable: false),
                    PressureLowerBloodPressure = table.Column<int>(name: "Pressure_LowerBloodPressure", type: "integer", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaryEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExercisesHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uuid", nullable: false),
                    ActionDescription = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExercisesHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExercisesParams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TrainingId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExercisesParams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExercisesParamsHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExerciseParamsId = table.Column<Guid>(type: "uuid", nullable: false),
                    ActionDescription = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExercisesParamsHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invitations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SenderId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecipientId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitations_AspNetUsers_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invitations_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SpecialistId = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    TransferAllowed = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_AspNetUsers_PatientId",
                        column: x => x.PatientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notes_AspNetUsers_SpecialistId",
                        column: x => x.SpecialistId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    Viewed = table.Column<bool>(type: "boolean", nullable: false),
                    RecipientId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Observations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    SpecialistId = table.Column<Guid>(type: "uuid", nullable: false),
                    BeginDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Observations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    SpecialistId = table.Column<Guid>(type: "uuid", nullable: false),
                    Evaluation = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_SpecialistId",
                        column: x => x.SpecialistId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trainings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DiaryId = table.Column<Guid>(type: "uuid", nullable: false),
                    SpecialistId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingsHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TrainingId = table.Column<Guid>(type: "uuid", nullable: false),
                    ActionDescription = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingsHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DescriptionValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DescriptionTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BeginDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DescriptionValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DescriptionValues_DescriptionTypes_DescriptionTypeId",
                        column: x => x.DescriptionTypeId,
                        principalTable: "DescriptionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppealsDesc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AppealId = table.Column<Guid>(type: "uuid", nullable: false),
                    DescriptionTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    DescriptionValueId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppealsDesc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppealsDesc_Appeals_AppealId",
                        column: x => x.AppealId,
                        principalTable: "Appeals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppealsDesc_DescriptionTypes_DescriptionTypeId",
                        column: x => x.DescriptionTypeId,
                        principalTable: "DescriptionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppealsDesc_DescriptionValues_DescriptionValueId",
                        column: x => x.DescriptionValueId,
                        principalTable: "DescriptionValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUsersDesc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    DescriptionTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    DescriptionValueId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsersDesc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUsersDesc_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUsersDesc_DescriptionTypes_DescriptionTypeId",
                        column: x => x.DescriptionTypeId,
                        principalTable: "DescriptionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUsersDesc_DescriptionValues_DescriptionValueId",
                        column: x => x.DescriptionValueId,
                        principalTable: "DescriptionValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiariesDesc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DiaryId = table.Column<Guid>(type: "uuid", nullable: false),
                    DescriptionTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    DescriptionValueId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiariesDesc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiariesDesc_DescriptionTypes_DescriptionTypeId",
                        column: x => x.DescriptionTypeId,
                        principalTable: "DescriptionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiariesDesc_DescriptionValues_DescriptionValueId",
                        column: x => x.DescriptionValueId,
                        principalTable: "DescriptionValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiariesDesc_Diaries_DiaryId",
                        column: x => x.DiaryId,
                        principalTable: "Diaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExercisesDesc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uuid", nullable: false),
                    DescriptionTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    DescriptionValueId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExercisesDesc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExercisesDesc_DescriptionTypes_DescriptionTypeId",
                        column: x => x.DescriptionTypeId,
                        principalTable: "DescriptionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExercisesDesc_DescriptionValues_DescriptionValueId",
                        column: x => x.DescriptionValueId,
                        principalTable: "DescriptionValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExercisesDesc_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExercisesParamsDesc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExerciseParamsId = table.Column<Guid>(type: "uuid", nullable: false),
                    DescriptionTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    DescriptionValueId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExercisesParamsDesc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExercisesParamsDesc_DescriptionTypes_DescriptionTypeId",
                        column: x => x.DescriptionTypeId,
                        principalTable: "DescriptionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExercisesParamsDesc_DescriptionValues_DescriptionValueId",
                        column: x => x.DescriptionValueId,
                        principalTable: "DescriptionValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExercisesParamsDesc_ExercisesParams_ExerciseParamsId",
                        column: x => x.ExerciseParamsId,
                        principalTable: "ExercisesParams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostsDesc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    DescriptionTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    DescriptionValueId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostsDesc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostsDesc_DescriptionTypes_DescriptionTypeId",
                        column: x => x.DescriptionTypeId,
                        principalTable: "DescriptionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostsDesc_DescriptionValues_DescriptionValueId",
                        column: x => x.DescriptionValueId,
                        principalTable: "DescriptionValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostsDesc_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReviewsDesc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReviewId = table.Column<Guid>(type: "uuid", nullable: false),
                    DescriptionTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    DescriptionValueId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewsDesc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewsDesc_DescriptionTypes_DescriptionTypeId",
                        column: x => x.DescriptionTypeId,
                        principalTable: "DescriptionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReviewsDesc_DescriptionValues_DescriptionValueId",
                        column: x => x.DescriptionValueId,
                        principalTable: "DescriptionValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReviewsDesc_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CityId",
                table: "AspNetUsers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_AppealsDesc_AppealId",
                table: "AppealsDesc",
                column: "AppealId");

            migrationBuilder.CreateIndex(
                name: "IX_AppealsDesc_DescriptionTypeId",
                table: "AppealsDesc",
                column: "DescriptionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppealsDesc_DescriptionValueId",
                table: "AppealsDesc",
                column: "DescriptionValueId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsersDesc_ApplicationUserId",
                table: "ApplicationUsersDesc",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsersDesc_DescriptionTypeId",
                table: "ApplicationUsersDesc",
                column: "DescriptionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsersDesc_DescriptionValueId",
                table: "ApplicationUsersDesc",
                column: "DescriptionValueId");

            migrationBuilder.CreateIndex(
                name: "IX_DescriptionValues_DescriptionTypeId",
                table: "DescriptionValues",
                column: "DescriptionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Diaries_PatientId",
                table: "Diaries",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_DiariesDesc_DescriptionTypeId",
                table: "DiariesDesc",
                column: "DescriptionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DiariesDesc_DescriptionValueId",
                table: "DiariesDesc",
                column: "DescriptionValueId");

            migrationBuilder.CreateIndex(
                name: "IX_DiariesDesc_DiaryId",
                table: "DiariesDesc",
                column: "DiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_ExercisesDesc_DescriptionTypeId",
                table: "ExercisesDesc",
                column: "DescriptionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExercisesDesc_DescriptionValueId",
                table: "ExercisesDesc",
                column: "DescriptionValueId");

            migrationBuilder.CreateIndex(
                name: "IX_ExercisesDesc_ExerciseId",
                table: "ExercisesDesc",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExercisesParamsDesc_DescriptionTypeId",
                table: "ExercisesParamsDesc",
                column: "DescriptionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExercisesParamsDesc_DescriptionValueId",
                table: "ExercisesParamsDesc",
                column: "DescriptionValueId");

            migrationBuilder.CreateIndex(
                name: "IX_ExercisesParamsDesc_ExerciseParamsId",
                table: "ExercisesParamsDesc",
                column: "ExerciseParamsId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_RecipientId",
                table: "Invitations",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_SenderId",
                table: "Invitations",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_PatientId",
                table: "Notes",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_SpecialistId",
                table: "Notes",
                column: "SpecialistId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostsDesc_DescriptionTypeId",
                table: "PostsDesc",
                column: "DescriptionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PostsDesc_DescriptionValueId",
                table: "PostsDesc",
                column: "DescriptionValueId");

            migrationBuilder.CreateIndex(
                name: "IX_PostsDesc_PostId",
                table: "PostsDesc",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_AuthorId",
                table: "Reviews",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_SpecialistId",
                table: "Reviews",
                column: "SpecialistId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewsDesc_DescriptionTypeId",
                table: "ReviewsDesc",
                column: "DescriptionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewsDesc_DescriptionValueId",
                table: "ReviewsDesc",
                column: "DescriptionValueId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewsDesc_ReviewId",
                table: "ReviewsDesc",
                column: "ReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Cities_CityId",
                table: "AspNetUsers",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExtAuthInfos_AspNetUsers_ApplicationUserId",
                table: "ExtAuthInfos",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Cities_CityId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ExtAuthInfos_AspNetUsers_ApplicationUserId",
                table: "ExtAuthInfos");

            migrationBuilder.DropTable(
                name: "AppealsDesc");

            migrationBuilder.DropTable(
                name: "ApplicationUsersDesc");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "DiariesDesc");

            migrationBuilder.DropTable(
                name: "DiaryEntries");

            migrationBuilder.DropTable(
                name: "ExercisesDesc");

            migrationBuilder.DropTable(
                name: "ExercisesHistory");

            migrationBuilder.DropTable(
                name: "ExercisesParamsDesc");

            migrationBuilder.DropTable(
                name: "ExercisesParamsHistory");

            migrationBuilder.DropTable(
                name: "Invitations");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Observations");

            migrationBuilder.DropTable(
                name: "PostsDesc");

            migrationBuilder.DropTable(
                name: "ReviewsDesc");

            migrationBuilder.DropTable(
                name: "Trainings");

            migrationBuilder.DropTable(
                name: "TrainingsHistory");

            migrationBuilder.DropTable(
                name: "Appeals");

            migrationBuilder.DropTable(
                name: "Diaries");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "ExercisesParams");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "DescriptionValues");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "DescriptionTypes");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CityId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExtAuthInfos",
                table: "ExtAuthInfos");

            migrationBuilder.DropColumn(
                name: "Blocked",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Verified",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "ExtAuthInfos",
                newName: "ExtAuthInfo");

            migrationBuilder.RenameIndex(
                name: "IX_ExtAuthInfos_ApplicationUserId",
                table: "ExtAuthInfo",
                newName: "IX_ExtAuthInfo_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExtAuthInfo",
                table: "ExtAuthInfo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExtAuthInfo_AspNetUsers_ApplicationUserId",
                table: "ExtAuthInfo",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
