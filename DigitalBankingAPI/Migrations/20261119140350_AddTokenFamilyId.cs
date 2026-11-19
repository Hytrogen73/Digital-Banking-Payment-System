using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalBankingAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTokenFamilyId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TokenFamilyId",
                table: "RefreshTokens",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TokenFamilyId",
                table: "RefreshTokens");
        }
    }
}
