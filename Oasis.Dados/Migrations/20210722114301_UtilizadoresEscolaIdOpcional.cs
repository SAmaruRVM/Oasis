using Microsoft.EntityFrameworkCore.Migrations;

namespace Oasis.Dados.Migrations
{
    public partial class UtilizadoresEscolaIdOpcional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "EscolaId",
                table: "Utilizadores",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "TiposUtilizador",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "63966261-fab5-4183-9049-9b9994e3812d");

            migrationBuilder.UpdateData(
                table: "TiposUtilizador",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "44b36502-04b7-4792-a459-49d8307f8633");

            migrationBuilder.UpdateData(
                table: "TiposUtilizador",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "0528a216-9497-47fd-8fa1-c61f49c3bef1");

            migrationBuilder.UpdateData(
                table: "TiposUtilizador",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "2efd5efe-3449-46b8-a5b3-e5420d1bebf0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "EscolaId",
                table: "Utilizadores",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
    }
}
