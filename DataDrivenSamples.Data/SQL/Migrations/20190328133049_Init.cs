using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataDrivenSamples.Data.SQL.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    PartitionKey = table.Column<string>(nullable: true),
                    RowKey = table.Column<string>(nullable: true),
                    Timestamp = table.Column<DateTimeOffset>(nullable: false),
                    ETag = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
