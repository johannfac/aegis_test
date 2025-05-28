using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AegisTest.Migrations
{
    /// <inheritdoc />
    public partial class AddGetAllProductWithDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE FUNCTION GetAllProductWithDescription()
RETURNS TABLE
AS
RETURN
(
    WITH ProductCTE AS
    (
        SELECT p.Id, p.Name
        FROM Product p
        WHERE p.Id IN (SELECT pd.ProductId FROM ProductDescription pd)
    )
    
    SELECT * FROM ProductCTE
)
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION GetAllProductWithDescription");
        }
    }
}
