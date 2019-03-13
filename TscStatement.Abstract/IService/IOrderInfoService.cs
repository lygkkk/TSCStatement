using System.Collections.Generic;
using TscStatement.Abstract.Models;

namespace TscStatement.Abstract.IService
{
    public interface IOrderInfoService
    {
        IEnumerable<OrderInfo> GetOrderInfos(string sql);

    }
}