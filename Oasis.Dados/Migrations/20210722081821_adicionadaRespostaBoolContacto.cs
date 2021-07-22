using Microsoft.EntityFrameworkCore.Migrations;

namespace Oasis.Dados.Migrations
{
    public partial class adicionadaRespostaBoolContacto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Respondido",
                table: "Contactos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "TiposUtilizador",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "233e14ee-a2cd-40c5-b394-5164d188dac3");

            migrationBuilder.UpdateData(
                table: "TiposUtilizador",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "75affd6f-f6a8-428c-8232-7244ea2ce0b9");

            migrationBuilder.UpdateData(
                table: "TiposUtilizador",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "e1c71efd-40f0-46f4-8f83-8c625cfac4cf");

            migrationBuilder.UpdateData(
                table: "TiposUtilizador",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "484bdd9f-94d1-4899-aac5-b4d19f3c616d");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Respondido",
                table: "Contactos");

            migrationBuilder.UpdateData(
                table: "TiposUtilizador",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "2f840ea7-a2f1-4b77-a6dd-14555f74dbbb");

            migrationBuilder.UpdateData(
                table: "TiposUtilizador",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "7dbe7600-3bb5-41ef-9a32-1ac4afe6d140");

            migrationBuilder.UpdateData(
                table: "TiposUtilizador",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "30ae9d49-16ed-4044-89ab-1c7ed56edf97");

            migrationBuilder.UpdateData(
                table: "TiposUtilizador",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "738d6898-d72b-4c20-b4b0-7c6568d2859c");
        }
    }
}
