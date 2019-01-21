using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortnr.Api.Migrations
{
    public partial class Initial_Api : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parameters",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    Created = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    LastUpdated = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Urls",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    Created = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    LastUpdated = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UserId = table.Column<string>(nullable: true),
                    OriginalUrl = table.Column<string>(nullable: false),
                    Shortened = table.Column<string>(nullable: false),
                    Expiration = table.Column<int>(nullable: false),
                    Accesses = table.Column<int>(nullable: false),
                    LastAccess = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urls", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IDX_Parameter_Key",
                table: "Parameters",
                column: "Key",
                unique: true,
                filter: "[Key] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IDX_Url_Shortened",
                table: "Urls",
                column: "Shortened",
                unique: true);

            migrationBuilder.Sql(
@"CREATE TRIGGER [dbo].[Parameters_UPDATE] ON [dbo].[Parameters]
    AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF ((SELECT TRIGGER_NESTLEVEL()) > 1) RETURN;

    DECLARE @Id uniqueidentifier

    SELECT @Id = INSERTED.Id
    FROM INSERTED

    UPDATE dbo.Parameters
    SET LastUpdated = GETUTCDATE()
    WHERE Id = @Id
END");

            migrationBuilder.Sql(
@"CREATE TRIGGER [dbo].[Urls_UPDATE] ON [dbo].[Urls]
    AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF ((SELECT TRIGGER_NESTLEVEL()) > 1) RETURN;

    DECLARE @Id uniqueidentifier

    SELECT @Id = INSERTED.Id
    FROM INSERTED

    UPDATE dbo.Urls
    SET LastUpdated = GETUTCDATE()
    WHERE Id = @Id
END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS [dbo].[Urls_UPDATE]");
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS [dbo].[Parameters_UPDATE]");

            migrationBuilder.DropTable(
                name: "Parameters");

            migrationBuilder.DropTable(
                name: "Urls");
        }
    }
}
