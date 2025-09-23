using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace DeliverySystem.Migrations
{
    /// <inheritdoc />
    public partial class NewModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Удаляем старый столбец Id, если он есть
            migrationBuilder.DropPrimaryKey(
                name: "PK_Couriers",
                table: "Couriers");
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Couriers");

            // Добавляем новый столбец Id типа uuid с дефолтным значением
            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Couriers",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()"
            );
            migrationBuilder.AddPrimaryKey(
                name: "PK_Couriers",
                table: "Couriers",
                column: "Id"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Откатываем изменения: удаляем uuid Id и возвращаем int Id с автоинкрементом
            migrationBuilder.DropPrimaryKey(
                name: "PK_Couriers",
                table: "Couriers");
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Couriers");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Couriers",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );
            migrationBuilder.AddPrimaryKey(
                name: "PK_Couriers",
                table: "Couriers",
                column: "Id"
            );
        }
    }
}
