using TscStatement.Abstract.IService;

namespace TscStatement.ServiceRealize
{
    public class InjectionPoint
    {
        private readonly IOrderInfoService _orderInfoService;
        public InjectionPoint(IOrderInfoService orderInfoService)
        {
            _orderInfoService = orderInfoService;
        }

        public void T1()
        {
            string sql =
                @"SELECT 类别 AS Category, 单号 AS OrderNumber, '客户名称' AS CustomerName, 摘要 AS Summary, 日期 AS OrderDateTime, FIRST(结存金额) AS PreviousBalance, 发出金额 AS IssuedAmount, 减少金额 AS PaymentAmount, 结存金额 AS CurrentBalance FROM [富阳6店$]";

            _orderInfoService.GetOrderInfos(sql);
        }
    }
}