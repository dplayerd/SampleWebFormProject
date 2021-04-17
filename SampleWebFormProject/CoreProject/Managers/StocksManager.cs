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
    public class StocksManager : DBBase
    {
        public StockModel GetStock(Guid productID)
        {
            string queryString =
                $@" SELECT *
                    FROM Stocks
                    WHERE ProductID = @id
                ";

            List<SqlParameter> dbParameters = new List<SqlParameter>();
            dbParameters.Add(new SqlParameter("@id", productID));

            var dt = this.GetDataTable(queryString, dbParameters);

            //----- 檢查值 -----
            if (dt == null || dt.Rows.Count == 0)
                return null;
            //----- 檢查值 -----


            StockModel model = new StockModel();
            DataRow dr = dt.Rows[0];

            model.ID = (Guid)dr["ID"];
            model.CurrentQty = (int)dr["CurrentQty"];
            model.LockedQty = (int)dr["LockedQty"];

            return model;
        }


        public void UpdateStock(StockModel model)
        {
            if (model.CurrentQty < model.LockedQty)
                throw new ArgumentException(" CurrentQty should more than LockedQty. ");


            string dbCommandText =
                $@" UPDATE Stocks
                    SET 
                        CurrentQty = @CurrentQty, 
                        LockedQty = @LockedQty	
                    WHERE
                        ID = @ID
                ";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@ID", model.ID),
                new SqlParameter("@CurrentQty", model.CurrentQty),
                new SqlParameter("@LockedQty", model.LockedQty),
            };

            this.ExecuteNonQuery(dbCommandText, parameters);
        }
    }
}
