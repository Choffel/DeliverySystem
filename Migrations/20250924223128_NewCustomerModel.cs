using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DeliverySystem.Migrations
{
    /// <inheritdoc />
    public partial class NewCustomerModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the existing Orders table primary key
            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            // Drop the old Id column
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Orders");

            // Add new Id column of type uuid with default value
            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Orders",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()");

            // Re-add the primary key constraint
            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            // Drop the UUID primary key
            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            // Drop the UUID column
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Orders");

            // Add back the integer identity column
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Orders",
                type: "integer",
                nullable: false)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            // Re-add the primary key constraint
            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");
        }
    }
}
