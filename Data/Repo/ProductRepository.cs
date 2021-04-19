using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProductAPI.Controllers.Resources;
using ProductAPI.Interfaces;
using ProductApp.Data;
using ProductApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.Data.Repo
{
    public class ProductRepository : IProductRepository
    {
        private IDbConnection db;
        public ProductRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("ProductAppCon"));
        }

        public async Task<Product> CreateProduct(Product product)
        {
            //var sql = "INSERT INTO Products(Name,Description,ImagePath,Type,Expiry,Date,Price,LastUpdatedBy,ProductCategoryId) VALUES(@Name,@Description,@ImagePath,@Type,@Expiry,@Date,@Price,@LastUpdatedBy,@ProductCategoryId)"
            //            + "SELECT CAST(SCOPE IDENTITY() as int)";
            //var id = await db.QueryAsync<int>(sql, new { @Name = product.Name, @Description = product.Description, @ImagePath = product.ImagePath, @Type = product.Type, @Expiry = product.Expiry, @Date = product.Date, @Price = product.Price, @LastUpdatedBy = product.LastUpdatedBy, @ProductCategoryId = product.ProductCategoryId });
            //product.Id = id.Single();

            //return product;

            var sql = "INSERT INTO Products(Name,Description,ImagePath,Type,Expiry,Date,Price,LastUpdatedBy,ProductCategoryId) VALUES(@Name,@Description,@ImagePath,@Type,@Expiry,@Date,@Price,@LastUpdatedBy,@ProductCategoryId)";
            await db.ExecuteAsync(sql, new { @Name = product.Name, @Description = product.Description, @ImagePath = product.ImagePath, @Type = product.Type, @Expiry = product.Expiry, @Date = product.Date, @Price = product.Price, @LastUpdatedBy = product.LastUpdatedBy, @ProductCategoryId = product.ProductCategoryId });
            
            return product;
        }

        public async void DeleteProduct(int id)
        {
            var sql = "DELETE FROM Products WHERE Id=@Id";
            await db.ExecuteAsync(sql, new { @Id = id });
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var sql = "SELECT * FROM Products";
            var products = await db.QueryAsync<Product>(sql);

            return products.ToList();
        }

        public async Task<Product> GetProductbyId(int id)
        {
            var sql = "SELECT * FROM Products WHERE Id=@Id";
            var product = await db.QueryAsync<Product>(sql, new { @Id = id });

            return product.Single();
        }

        public async Task<IEnumerable<ProductCategory>> GetProductCategories()
        {
            var sql = "SELECT * FROM ProductCategories";
            var productCategories = await db.QueryAsync<ProductCategory>(sql);

            return productCategories.ToList();
        }

        public async Task<Product> UpdateProduct(int id, Product product)
        {
            var sql = "UPDATE Products SET Name=@Name,Description=@Description,ImagePath=@ImagePath,Type=@Type,Expiry=@Expiry,Date=@Date,Price=@Price,LastUpdatedBy=@LastUpdatedBy,ProductCategoryId=@ProductCategoryId WHERE Id=@Id";
            await db.ExecuteAsync(sql, new { @Name = product.Name, @Description = product.Description, @ImagePath = product.ImagePath, @Type = product.Type, @Expiry = product.Expiry, @Date = product.Date, @Price = product.Price, @LastUpdatedBy = product.LastUpdatedBy, @ProductCategoryId = product.ProductCategoryId, @Id=product.Id });
            
            return product;
        }
    }
}
