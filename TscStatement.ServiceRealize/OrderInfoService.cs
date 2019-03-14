using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using NPOI.SS.Formula.Functions;
using TscStatement.Abstract.IService;
using TscStatement.Abstract.Models;
using TscStatement.EntityFramework;

namespace TscStatement.ServiceRealize
{
    public class OrderInfoService : IOrderInfoService
    {
        private readonly DbContext _dbContext;

        public OrderInfoService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<OrderInfo> GetOrderInfos(string sql)
        {
            DataTable dt = new DataTable();

            using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(sql, _dbContext.Connection))
            {
                dataAdapter.Fill(dt);
            }

            //var tmp = dt.Rows[6][1].GetType();
            ////Type type = GetType(tmp);
            //Console.WriteLine(dt.Rows[6][1] == DBNull.Value);
            return DataTableToList(dt);
        }

        private IEnumerable<OrderInfo> DataTableToList(DataTable dataTable)
        {
            List<OrderInfo> list = new List<OrderInfo>();

            int rouCount = dataTable.Rows.Count - 1;
            for (int i = 0; i < rouCount; i++)
            {
                if (dataTable.Rows[i][1].Equals("") && i == 0)
                {
                    dataTable.Rows[i][5] = dataTable.Rows[0][8];
                    dataTable.Rows[i][2] = "";
                    dataTable.Rows[i][8] = 0;
                }
                else if (i < rouCount)
                {
                    dataTable.Rows[i][8] = 0;
                }
                
            }

            foreach (DataRow dataRow in dataTable.Rows)
            {
                list.Add(new OrderInfo
                {
                    Category = dataRow["Category"].ToString(),
                    OrderNumber = dataRow["OrderNumber"].ToString(),
                    CustomerName = dataRow["CustomerName"].ToString(),
                    Summary = dataRow["Summary"].ToString().Contains("结存") ? "" : dataRow["Summary"].ToString(),
                    OrderDateTime = dataRow["OrderDateTime"] == DBNull.Value ? (DateTime?)null : DateTime.Parse(dataRow["OrderDateTime"].ToString()),
                    PreviousBalance = dataRow["PreviousBalance"] == DBNull.Value ? (double?)null : Convert.ToDouble(dataRow["PreviousBalance"]),
                    IssuedAmount = dataRow["IssuedAmount"] == DBNull.Value ? (double?)null : Convert.ToDouble(dataRow["IssuedAmount"]),
                    PaymentAmount = dataRow["PaymentAmount"] == DBNull.Value ? (double?)null : Convert.ToDouble(dataRow["PaymentAmount"]),
                    CurrentBalance = dataRow["CurrentBalance"] == DBNull.Value ? (double?)null : Convert.ToDouble(dataRow["CurrentBalance"]),
                });
            }

            return list;

        }
    }
}