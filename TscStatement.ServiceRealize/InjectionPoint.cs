using System;
using System.Collections.Generic;
using TscStatement.Abstract.IService;
using TscStatement.Abstract.Models;

namespace TscStatement.ServiceRealize
{
    public class InjectionPoint
    {
        private readonly IOrderInfoService _orderInfoService;
        private IEnumerable<OrderInfo> _orderInfos;
        public InjectionPoint(IOrderInfoService orderInfoService)
        {
            _orderInfoService = orderInfoService;
        }

        public void T1()
        {
            string sql =
                @"SELECT 类别 AS Category, 单号 AS OrderNumber, '客户名称' AS CustomerName, 摘要 AS Summary, 日期 AS OrderDateTime, 结存金额 AS PreviousBalance, 发出金额 AS IssuedAmount, 减少金额 AS PaymentAmount, 结存金额 AS CurrentBalance FROM [唐三彩下沙保利湾店$]";

            _orderInfos = _orderInfoService.GetOrderInfos(sql);


            //foreach (OrderInfo order in _orderInfos)
            //{
            //    Console.WriteLine(order.OrderDateTime?.Date.ToShortDateString());
            //}
        }
    }
}