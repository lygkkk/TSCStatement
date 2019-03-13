using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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

            return DataTableToList(dt);
        }

        private IEnumerable<OrderInfo> DataTableToList(DataTable dataTable)
        {
            List<OrderInfo> list = new List<OrderInfo>();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                list.Add(new OrderInfo
                {
                    Category = dataRow["类别"].ToString(),
                    OrderNumber = dataRow["单号"].ToString(),
                    CustomerName = dataRow["单位名称"].ToString(),
                    Summary = dataRow["摘要"].ToString(),
                    OrderDateTime = Convert.ToDateTime(dataRow["日期"].ToString()),
                    PreviousBalance = Convert.ToDouble(dataRow["结存金额"]),
                    IssuedAmount = Convert.ToDouble(dataRow["发出金额"]),
                    PaymentAmount = Convert.ToDouble(dataRow["减少金额"]),
                    CurrentBalance = Convert.ToDouble(dataRow["结存金额"]),
                });
            }

            return list;

        }
    }
}