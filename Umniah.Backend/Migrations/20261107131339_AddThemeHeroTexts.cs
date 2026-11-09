using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Umniah.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddThemeHeroTexts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeroTextAr",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "HeroTextEn",
                table: "SiteSettings");

            migrationBuilder.AddColumn<DateTime>(
                name: "BannerExpiresAt",
                table: "SiteSettings",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GalleryThemeTexts",
                columns: table => new
                {
                    Theme = table.Column<int>(type: "integer", nullable: false),
                    HeroTextEn = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    HeroTextAr = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryThemeTexts", x => x.Theme);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GalleryThemeTexts");

            migrationBuilder.DropColumn(
                name: "BannerExpiresAt",
                table: "SiteSettings");

            migrationBuilder.AddColumn<string>(
                name: "HeroTextAr",
                table: "SiteSettings",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeroTextEn",
                table: "SiteSettings",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);
        }
    }
}
