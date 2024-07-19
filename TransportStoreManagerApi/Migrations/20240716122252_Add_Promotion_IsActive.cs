using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransportStoreManagerApi.Migrations
{
    /// <inheritdoc />
    public partial class Add_Promotion_IsActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ProductPromotions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ProductPromotions");
        }
    }
}
