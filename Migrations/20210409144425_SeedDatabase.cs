using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductAPI.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO ProductCategories (ProductCategoryName) VALUES ('Category1')");
            migrationBuilder.Sql("INSERT INTO ProductCategories (ProductCategoryName) VALUES ('Category2')");
            migrationBuilder.Sql("INSERT INTO ProductCategories (ProductCategoryName) VALUES ('Category3')");
            migrationBuilder.Sql("INSERT INTO ProductCategories (ProductCategoryName) VALUES ('Category4')");

            migrationBuilder.Sql("INSERT INTO Products (Name, ProductCategoryId, Date, Price) VALUES ('Category1-ProductA',(SELECT ID FROM ProductCategories WHERE ProductCategoryName='Category1'), GETDATE(), 0.0)");
            //migrationBuilder.Sql("INSERT INTO Products (Name, ProductCategoryId) VALUES ('Category1-ProductA',1)");
            migrationBuilder.Sql("INSERT INTO Products (Name, ProductCategoryId, Date, Price) VALUES ('Category1-ProductB',(SELECT ID FROM ProductCategories WHERE ProductCategoryName='Category1'), GETDATE(), 0.0)");
            migrationBuilder.Sql("INSERT INTO Products (Name, ProductCategoryId, Date, Price) VALUES ('Category1-ProductC',(SELECT ID FROM ProductCategories WHERE ProductCategoryName='Category1'), GETDATE(), 0.0)");

            migrationBuilder.Sql("INSERT INTO Products (Name, ProductCategoryId, Date, Price) VALUES ('Category2-ProductA',(SELECT ID FROM ProductCategories WHERE ProductCategoryName='Category2'), GETDATE(), 0.0)");
            migrationBuilder.Sql("INSERT INTO Products (Name, ProductCategoryId, Date, Price) VALUES ('Category2-ProductB',(SELECT ID FROM ProductCategories WHERE ProductCategoryName='Category2'), GETDATE(), 0.0)");
            migrationBuilder.Sql("INSERT INTO Products (Name, ProductCategoryId, Date, Price) VALUES ('Category2-ProductC',(SELECT ID FROM ProductCategories WHERE ProductCategoryName='Category2'), GETDATE(), 0.0)");

            migrationBuilder.Sql("INSERT INTO Products (Name, ProductCategoryId, Date, Price) VALUES ('Category3-ProductA',(SELECT ID FROM ProductCategories WHERE ProductCategoryName='Category3'), GETDATE(), 0.0)");
            migrationBuilder.Sql("INSERT INTO Products (Name, ProductCategoryId, Date, Price) VALUES ('Category3-ProductB',(SELECT ID FROM ProductCategories WHERE ProductCategoryName='Category3'), GETDATE(), 0.0)");
            migrationBuilder.Sql("INSERT INTO Products (Name, ProductCategoryId, Date, Price) VALUES ('Category3-ProductC',(SELECT ID FROM ProductCategories WHERE ProductCategoryName='Category3'), GETDATE(), 0.0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM ProductCategories WHERE ProductCategoryName IN ('Category1', 'Category2', 'Category3')");
        }
    }
}
