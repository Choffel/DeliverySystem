using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliverySystem.Migrations
{
    public partial class updateorderandcouriermodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Сначала убираем лишние FK/Index, связанные с временным CustomerId1
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId1",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CustomerId1",
                table: "Orders");

            // Удаляем столбец AddressId (он больше не нужен)
            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Orders");

            // === Конвертация CustomerId (int -> uuid) ===
            // Мы не можем сделать прямой ALTER int -> uuid (ошибка 42804), поэтому создаём временный столбец
            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId_tmp",
                table: "Orders",
                type: "uuid",
                nullable: true);

            // Копируем значения из существующего столбца CustomerId1 (который уже uuid), если он был
            migrationBuilder.Sql(@"UPDATE ""Orders"" SET ""CustomerId_tmp"" = ""CustomerId1"" WHERE ""CustomerId1"" IS NOT NULL;");

            // Удаляем старые столбцы CustomerId1 и CustomerId (int)
            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Orders");

            // Переименовываем временный в основной и ставим NOT NULL (если нужны NOT NULL, проверяем что не осталось NULL)
            migrationBuilder.Sql(@"UPDATE ""Orders"" SET ""CustomerId_tmp"" = gen_random_uuid() WHERE ""CustomerId_tmp"" IS NULL;");
            migrationBuilder.RenameColumn(
                name: "CustomerId_tmp",
                table: "Orders",
                newName: "CustomerId");

            // === CourierId (int -> uuid?) ===
            // Проще удалить и создать заново (старые связи теряем). Если важны — доработай вручную.
            migrationBuilder.DropColumn(
                name: "CourierId",
                table: "Orders");
            migrationBuilder.AddColumn<Guid>(
                name: "CourierId",
                table: "Orders",
                type: "uuid",
                nullable: true);

            // Добавляем новые нужные столбцы, если их ещё нет
            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrderNumber",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Product",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime?> (
                name: "UpdatedAt",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: true);

            // Индексы
            migrationBuilder.CreateIndex(
                name: "IX_Orders_CourierId",
                table: "Orders",
                column: "CourierId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            // FK связи
            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Couriers_CourierId",
                table: "Orders",
                column: "CourierId",
                principalTable: "Couriers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Откат: упрощённо восстановим старую схему (int CustomerId/CourierId)
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Couriers_CourierId",
                table: "Orders");
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CourierId",
                table: "Orders");
            migrationBuilder.DropIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryAddress",
                table: "Orders");
            migrationBuilder.DropColumn(
                name: "OrderNumber",
                table: "Orders");
            migrationBuilder.DropColumn(
                name: "Product",
                table: "Orders");
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Orders");
            migrationBuilder.DropColumn(
                name: "CourierId",
                table: "Orders");
            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CourierId",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId1",
                table: "Orders",
                type: "uuid",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId1",
                table: "Orders",
                column: "CustomerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId1",
                table: "Orders",
                column: "CustomerId1",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
