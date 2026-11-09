using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Umniah.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddGalleryText : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BannerTextAr",
                table: "SiteSettings",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BannerTextEn",
                table: "SiteSettings",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BannerTextAr",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "BannerTextEn",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "HeroTextAr",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "HeroTextEn",
                table: "SiteSettings");
        }
    }
}
