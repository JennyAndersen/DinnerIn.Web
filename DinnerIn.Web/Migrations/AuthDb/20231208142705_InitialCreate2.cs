using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DinnerIn.Web.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8692abcc-1349-4abf-b49e-11379f2c5669",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "14ee2023-cb4c-4d71-bfc4-244af44ed71f", "AQAAAAIAAYagAAAAEIYSOyALALgyMBAixl6a2BJvjDviRHuzB2StPctzNjTP1eAG9flEGphZoKycQ51nzA==", "cdbe1ac8-b634-49ef-8590-c417686c82b6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8692abcc-1349-4abf-b49e-11379f2c5669",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0db3e721-4687-4326-a53c-81233c499687", "AQAAAAIAAYagAAAAEFFzX/5aGBuxnmYZ+GKsL1Z2KYQn0zjZPBgXqO2n5x4gqQSwsJM//aLnSZn4tqsn1A==", "fd7de232-13e3-4eb9-a17e-e491b7c0c7ce" });
        }
    }
}
