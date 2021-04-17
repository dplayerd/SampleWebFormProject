using CoreProject.Helpers;
using CoreProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreProject.Managers
{
    public class ProductManager : DBBase
    {
        public void CreateProduct(ProductModel model)
        {
            if (this.HasProductName(model.Caption))
            {
                throw new Exception($"Product [{model.Caption}] has been created.");
            }

            string dbCommandText =
                $@" INSERT INTO Products 
                        (ID, ProductType, Caption, Price, Body, IsEnabled, CreateDate, Creator) 
                    VALUES 
                        (@ID, @ProductType, @Caption, @Price, @Body, @IsEnabled, @CreateDate, @Creator);
                ";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@ID", Guid.NewGuid()),
                new SqlParameter("@ProductType", model.ProductType),
                new SqlParameter("@Caption", model.Caption),
                new SqlParameter("@Price", model.Price),
                new SqlParameter("@Body", model.Body),
                new SqlParameter("@IsEnabled", model.IsEnabled),
                new SqlParameter("@CreateDate", model.CreateDate),
                new SqlParameter("@Creator", model.Creator)
            };

            this.ExecuteNonQuery(dbCommandText, parameters);
        }

        public void UpdateProduct(ProductModel model)
        {
            string dbCommandText =
                $@" UPDATE Products
                    SET 
                        ProductType = @ProductType, 
                        Caption = @Caption, 
                        Price = @Price, 
                        Body = @Body, 
                        IsEnabled = @IsEnabled, 
                        CreateDate = @CreateDate, 
                        Creator =  @Creator,
                        ModifyDate = @ModifyDate,	
                        Modifier   = @Modifier	
                    WHERE
                        ID = @ID
                ";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@ID", model.ID),
                new SqlParameter("@ProductType", model.ProductType),
                new SqlParameter("@Caption", model.Caption),
                new SqlParameter("@Price", model.Price),
                new SqlParameter("@Body", model.Body),
                new SqlParameter("@IsEnabled", model.IsEnabled),
                new SqlParameter("@CreateDate", model.CreateDate),
                new SqlParameter("@Creator", model.Creator),
                new SqlParameter("@ModifyDate", model.ModifyDate),
                new SqlParameter("@Modifier", model.Modifier)
            };

            this.ExecuteNonQuery(dbCommandText, parameters);
        }

        public void DeleteProduct(Guid id)
        {
            string dbCommandText =
                $@" DELETE Products
                    WHERE ID = @ID
                ";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@ID", id)
            };

            this.ExecuteNonQuery(dbCommandText, parameters);
        }

        public List<ProductModel> GetProducts(
            out int totalSize, int currentPage = 1, int pageSize = 10)
        {
            string query =
                $@" 
                SELECT TOP {10} * FROM
                (
                    SELECT 
                        ROW_NUMBER() OVER(ORDER BY ID) AS RowNumber,
                        *
                    FROM Products
                ) AS TempT
                WHERE RowNumber > {pageSize * (currentPage - 1)}
                ORDER BY ID
            ";

            string countQuery =
                $@" SELECT 
                    COUNT(ID)
                FROM Products
            ";

            List<SqlParameter> dbParameters = new List<SqlParameter>();
            var dt = this.GetDataTable(query, dbParameters);
            List<ProductModel> list = new List<ProductModel>();

            foreach (DataRow dr in dt.Rows)
            {
                ProductModel model = new ProductModel();

                model.ID = (Guid)dr["ID"];
                model.ProductType = (int)dr["ProductType"];
                model.Caption = (string)dr["Caption"];
                model.Price = (decimal)dr["Price"];
                model.Body = (string)dr["Body"];
                model.IsEnabled = (bool)dr["IsEnabled"];
                model.CreateDate = (DateTime)dr["CreateDate"];
                model.Creator = (Guid)dr["Creator"];
                model.ModifyDate = dr["ModifyDate"] as DateTime?;
                model.Modifier = dr["Modifier"] as Guid?;

                list.Add(model);
            }


            // 算總數並回傳
            int? totalSize2 = this.GetScale(countQuery, dbParameters) as int?;
            totalSize = (totalSize2.HasValue) ? totalSize2.Value : 0;

            return list;
        }

        private bool HasProductName(string name)
        {
            return false;
        }

        public ProductModel GetProduct(Guid id)
        {
            return null;
        }


        public string GetProductName(int productType)
        {
            switch (productType)
            {
                case 1:
                    return "農機";
                case 2:
                    return "門禁系統";
                case 3: 
                    return "電池";
                default:
                    return "";
            }
        }
    }
}
