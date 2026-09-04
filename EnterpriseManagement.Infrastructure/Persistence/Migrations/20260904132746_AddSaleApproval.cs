using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnterpriseManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSaleApproval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedAt",
                table: "Sales",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ApprovedBy",
                table: "Sales",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ApprovedBy",
                table: "Sales",
                column: "ApprovedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Employees_ApprovedBy",
                table: "Sales",
                column: "ApprovedBy",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Employees_ApprovedBy",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_ApprovedBy",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "Sales");
        }
    }
}
