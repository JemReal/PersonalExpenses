using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonalExpenses.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataforFrequenciesandCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Abbr", "CategoyImageUrl", "Name" },
                values: new object[,]
                {
                    { new Guid("1083f4c1-51a5-4506-934d-23145054fd7b"), "CLOTH", "https://images.pexels.com/photos/3812433/pexels-photo-3812433.jpeg", "Clothing" },
                    { new Guid("9aa916c3-97f4-4906-9214-6755ee0023ff"), "UTILS", "https://images.pexels.com/photos/2898199/pexels-photo-2898199.jpeg", "Utilities" },
                    { new Guid("c4dd6eb2-51c9-4174-86ba-3a56cbe9a05f"), "FOOD", "https://www.food-safety.com/ext/resources/fsm/cache/file/26EC8DA4-CFD1-4437-897750814E836EBE.png", "Food" },
                    { new Guid("d880b23f-8fab-4b1a-af6f-1a2603b266ed"), "TRANSP", "https://images.pexels.com/photos/210182/pexels-photo-210182.jpeg", "Transportation" }
                });

            migrationBuilder.InsertData(
                table: "Frequencies",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1b358da8-c0d7-4e6a-933c-a7568a687a5d"), "Weekly" },
                    { new Guid("24413659-47c5-4fba-8ace-6ef37060b9e5"), "Daily" },
                    { new Guid("341bb587-72ed-4231-85c7-82c436080fbc"), "Monthly" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("1083f4c1-51a5-4506-934d-23145054fd7b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9aa916c3-97f4-4906-9214-6755ee0023ff"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c4dd6eb2-51c9-4174-86ba-3a56cbe9a05f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d880b23f-8fab-4b1a-af6f-1a2603b266ed"));

            migrationBuilder.DeleteData(
                table: "Frequencies",
                keyColumn: "Id",
                keyValue: new Guid("1b358da8-c0d7-4e6a-933c-a7568a687a5d"));

            migrationBuilder.DeleteData(
                table: "Frequencies",
                keyColumn: "Id",
                keyValue: new Guid("24413659-47c5-4fba-8ace-6ef37060b9e5"));

            migrationBuilder.DeleteData(
                table: "Frequencies",
                keyColumn: "Id",
                keyValue: new Guid("341bb587-72ed-4231-85c7-82c436080fbc"));
        }
    }
}
