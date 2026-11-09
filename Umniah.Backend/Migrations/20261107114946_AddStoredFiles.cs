using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Umniah.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddStoredFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Purchases_PurchaseId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ImageFile",
                table: "GalleryImages");

            migrationBuilder.AddColumn<Guid>(
                name: "StoredFileId",
                table: "GalleryImages",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "StoredFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Attempts = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastAttemptAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastError = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoredFiles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GalleryImages_StoredFileId",
                table: "GalleryImages",
                column: "StoredFileId");

            migrationBuilder.CreateIndex(
                name: "IX_StoredFiles_FileName",
                table: "StoredFiles",
                column: "FileName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StoredFiles_Status_CreatedOn",
                table: "StoredFiles",
                columns: new[] { "Status", "CreatedOn" });

            migrationBuilder.AddForeignKey(
                name: "FK_GalleryImages_StoredFiles_StoredFileId",
                table: "GalleryImages",
                column: "StoredFileId",
                principalTable: "StoredFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Purchases_PurchaseId",
                table: "Products",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GalleryImages_StoredFiles_StoredFileId",
                table: "GalleryImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Purchases_PurchaseId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "StoredFiles");

            migrationBuilder.DropIndex(
                name: "IX_GalleryImages_StoredFileId",
                table: "GalleryImages");

            migrationBuilder.DropColumn(
                name: "StoredFileId",
                table: "GalleryImages");

            migrationBuilder.AddColumn<string>(
                name: "ImageFile",
                table: "GalleryImages",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Purchases_PurchaseId",
                table: "Products",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
