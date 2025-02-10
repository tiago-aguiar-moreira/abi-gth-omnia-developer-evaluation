using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class AtualizaCamposCadastroVendas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanceledAt",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "IsCanceled",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "CanceledAt",
                table: "SaleItems");

            migrationBuilder.DropColumn(
                name: "IsCanceled",
                table: "SaleItems");

            migrationBuilder.AddColumn<short>(
                name: "Status",
                table: "Sales",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Sales");

            migrationBuilder.AddColumn<DateTime>(
                name: "CanceledAt",
                table: "Sales",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsCanceled",
                table: "Sales",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CanceledAt",
                table: "SaleItems",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsCanceled",
                table: "SaleItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
