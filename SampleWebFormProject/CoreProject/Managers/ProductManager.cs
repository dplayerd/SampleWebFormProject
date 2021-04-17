using CoreProject.Helpers;
using CoreProject.Models;
using CoreProject.ViewModels;
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

            Guid productID = Guid.NewGuid();
            Guid stockID = Guid.NewGuid();

            string dbCommandText =
                $@" INSERT INTO Products 
                        (ID, ProductType, Caption, Price, Body, IsEnabled, CreateDate, Creator) 
                    VALUES 
                        (@ID, @ProductType, @Caption, @Price, @Body, @IsEnabled, @CreateDate, @Creator);

                    INSERT INTO Stocks 
                        (ID, ProductID, CurrentQty, LockedQty, CreateDate, Creator) 
                    VALUES 
                        (@SID, @ID, 0, 0, @CreateDate, @Creator);
                ";


            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@ID", productID),
                new SqlParameter("@SID", stockID),
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

        public List<ProductViewModel> GetAdminProducts(
            string caption, int? productType, decimal? minPrice, decimal? maxPrice,
            out int totalSize, int currentPage = 1, int pageSize = 10)
        {
            List<SqlParameter> dbParameters = new List<SqlParameter>();


            //----- Process filter conditions -----
            List<string> conditions = new List<string>();

            if (!string.IsNullOrEmpty(caption))
            {
                conditions.Add(" Products.Caption LIKE '%' + @caption + '%'");
                dbParameters.Add(new SqlParameter("@caption", caption));
            }

            if (productType.HasValue)
            {
                conditions.Add(" Products.ProductType = @productType");
                dbParameters.Add(new SqlParameter("@productType", productType.Value));
            }

            if (minPrice.HasValue && maxPrice.HasValue)
            {
                if (minPrice.Value > maxPrice.Value)
                {
                    decimal temp = minPrice.Value;
                    minPrice = maxPrice.Value;
                    maxPrice = temp;
                }

                conditions.Add(" Products.Price BETWEEN @minPrice AND @maxPrice ");
                dbParameters.Add(new SqlParameter("@minPrice", minPrice.Value));
                dbParameters.Add(new SqlParameter("@maxPrice", maxPrice.Value));
            }
            else if (minPrice.HasValue)
            {
                conditions.Add(" Products.Price >= @minPrice ");
                dbParameters.Add(new SqlParameter("@minPrice", minPrice.Value));
            }
            else if (maxPrice.HasValue)
            {
                conditions.Add(" Products.Price <= @maxPrice ");
                dbParameters.Add(new SqlParameter("@maxPrice", maxPrice.Value));
            }

            string filterConditions =
                (conditions.Count > 0)
                    ? (" WHERE " + string.Join(" AND ", conditions))
                    : string.Empty;
            //----- Process filter conditions -----


            string query =
                $@" 
                SELECT TOP {10} * FROM
                (
                    SELECT 
	                    ROW_NUMBER() OVER(ORDER BY Products.CreateDate DESC) AS RowNumber,
                        Products.*,
	                    Stocks.CurrentQty,
	                    Stocks.LockedQty,
	                    Accounts.Name AS ModifierAccount
                    FROM Products
                    JOIN Stocks
	                    ON Products.ID = Stocks.ProductID
                    LEFT JOIN Accounts
	                    ON Products.Modifier = Accounts.ID
                    {filterConditions}
                ) AS TempT
                WHERE RowNumber > {pageSize * (currentPage - 1)}
                ORDER BY TempT.CreateDate DESC
            ";

            string countQuery =
                $@" SELECT COUNT(Products.ID)
                    FROM Products
                    JOIN Stocks
	                    ON Products.ID = Stocks.ProductID
                    LEFT JOIN Accounts
	                    ON Products.Modifier = Accounts.ID
                    {filterConditions}
                ";

            var dt = this.GetDataTable(query, dbParameters);
            List<ProductViewModel> list = new List<ProductViewModel>();

            foreach (DataRow dr in dt.Rows)
            {
                ProductViewModel model = new ProductViewModel();

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

                model.CurrentQty = (int)dr["CurrentQty"];
                model.LockedQty = (int)dr["LockedQty"];
                model.ModifierAccount = dr["ModifierAccount"] as string;

                list.Add(model);
            }

            // 算總數並回傳
            int? totalSize2 = this.GetScale(countQuery, dbParameters) as int?;
            totalSize = (totalSize2.HasValue) ? totalSize2.Value : 0;

            return list;
        }


        public List<ProductModel> GetProducts(string caption, int? productType, decimal? minPrice, decimal? maxPrice)
        {
            List<SqlParameter> dbParameters = new List<SqlParameter>();


            //----- Process filter conditions -----
            List<string> conditions = new List<string>();

            if (!string.IsNullOrEmpty(caption))
            {
                conditions.Add(" Caption LIKE '%' + @caption + '%'");
                dbParameters.Add(new SqlParameter("@caption", caption));
            }

            if (productType.HasValue)
            {
                conditions.Add(" ProductType = @productType");
                dbParameters.Add(new SqlParameter("@productType", productType.Value));
            }

            if (minPrice.HasValue && maxPrice.HasValue)
            {
                if (minPrice.Value > maxPrice.Value)
                {
                    decimal temp = minPrice.Value;
                    minPrice = maxPrice.Value;
                    maxPrice = temp;
                }

                conditions.Add(" Price BETWEEN @minPrice AND @maxPrice ");
                dbParameters.Add(new SqlParameter("@minPrice", minPrice.Value));
                dbParameters.Add(new SqlParameter("@maxPrice", maxPrice.Value));
            }
            else if (minPrice.HasValue)
            {
                conditions.Add(" Price >= @minPrice ");
                dbParameters.Add(new SqlParameter("@minPrice", minPrice.Value));
            }
            else if (maxPrice.HasValue)
            {
                conditions.Add(" Price <= @maxPrice ");
                dbParameters.Add(new SqlParameter("@maxPrice", maxPrice.Value));
            }

            string filterConditions =
                (conditions.Count > 0)
                    ? (" AND " + string.Join(" AND ", conditions))
                    : string.Empty;
            //----- Process filter conditions -----


            string query =
                $@" SELECT  *
                    FROM Products
                    WHERE IsEnabled = 'TRUE'
                        {filterConditions}
                ";

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


            return list;
        }


        private bool HasProductName(string name)
        {
            return false;
        }

        public ProductModel GetProduct(Guid id)
        {
            string queryString =
                $@" SELECT *
                    FROM Products
                    WHERE ID = @id
                ";

            List<SqlParameter> dbParameters = new List<SqlParameter>();
            dbParameters.Add(new SqlParameter("@id", id));

            var dt = this.GetDataTable(queryString, dbParameters);

            //----- 檢查值 -----
            if (dt == null || dt.Rows.Count == 0)
                return null;
            //----- 檢查值 -----


            ProductModel model = new ProductModel();
            DataRow dr = dt.Rows[0];

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

            return model;
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
