using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Seiun.Migrations
{
    /// <inheritdoc />
    public partial class MigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Article = table.Column<string>(type: "text", nullable: false),
                    ImageFileNames = table.Column<List<string>>(type: "text[]", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsPinned = table.Column<bool>(type: "boolean", nullable: false),
                    PinTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommentLike",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CommentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Action = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentLike", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    LikeCount = table.Column<int>(type: "integer", nullable: false),
                    DislikeCount = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PublicAnnouncements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    PublishTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AdminId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicAnnouncements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Replies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CommentId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentReplyId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Replies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TagEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserArticleStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LikedArticleId = table.Column<Guid>(type: "uuid", nullable: false),
                    LikedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserArticleStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    AvatarFileName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    UserName = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    NickName = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "bytea", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "bytea", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    IsBanned = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserWordRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    WordId = table.Column<Guid>(type: "uuid", nullable: false),
                    Stage = table.Column<int>(type: "integer", nullable: false),
                    NextReviewTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    WrongCount = table.Column<int>(type: "integer", nullable: false),
                    LastStudyTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWordRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WordEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WordText = table.Column<string>(type: "text", nullable: false),
                    Pronunciation = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Definition = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TagId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTag_TagEntity_TagId",
                        column: x => x.TagId,
                        principalTable: "TagEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTag_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagEntityWordEntity",
                columns: table => new
                {
                    TagsId = table.Column<Guid>(type: "uuid", nullable: false),
                    WordsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagEntityWordEntity", x => new { x.TagsId, x.WordsId });
                    table.ForeignKey(
                        name: "FK_TagEntityWordEntity_TagEntity_TagsId",
                        column: x => x.TagsId,
                        principalTable: "TagEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagEntityWordEntity_WordEntity_WordsId",
                        column: x => x.WordsId,
                        principalTable: "WordEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WordDistractor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DistractorText = table.Column<string>(type: "text", nullable: false),
                    Language = table.Column<string>(type: "text", nullable: false),
                    WordId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordDistractor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordDistractor_WordEntity_WordId",
                        column: x => x.WordId,
                        principalTable: "WordEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TagEntityWordEntity_WordsId",
                table: "TagEntityWordEntity",
                column: "WordsId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTag_TagId",
                table: "UserTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTag_UserId",
                table: "UserTag",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WordDistractor_WordId",
                table: "WordDistractor",
                column: "WordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "CommentLike");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "PublicAnnouncements");

            migrationBuilder.DropTable(
                name: "Replies");

            migrationBuilder.DropTable(
                name: "TagEntityWordEntity");

            migrationBuilder.DropTable(
                name: "UserArticleStatus");

            migrationBuilder.DropTable(
                name: "UserTag");

            migrationBuilder.DropTable(
                name: "UserWordRecords");

            migrationBuilder.DropTable(
                name: "WordDistractor");

            migrationBuilder.DropTable(
                name: "TagEntity");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WordEntity");
        }
    }
}
