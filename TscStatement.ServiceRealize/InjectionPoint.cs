using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using TscStatement.Abstract.IService;
using TscStatement.Abstract.Models;
using NPOI.XSSF.UserModel;
using TscStatement.Common;

namespace TscStatement.ServiceRealize
{
    public class InjectionPoint
    {
        private readonly IOrderInfoService _orderInfoService;
        //private IEnumerable<OrderInfo> _orderInfos;
        public InjectionPoint(IOrderInfoService orderInfoService)
        {
            _orderInfoService = orderInfoService;
        }

        public void T1(string[] fileStrings)
        {
            List<OrderInfo> orderInfos = new List<OrderInfo>();
            foreach (string file in fileStrings)
            {
                StringBuilder str = new StringBuilder();
                string companyName = Path.GetFileNameWithoutExtension(file);
                str.Append($@"SELECT A.类别 AS Category, A.单号 AS OrderNumber, '{companyName}' AS CustomerName, A.摘要 AS Summary, A.日期 AS OrderDateTime, ");
                str.Append(@"(0) AS PreviousBalance, A.发出金额 AS IssuedAmount, A.减少金额 AS PaymentAmount, A.结存金额 AS CurrentBalance ");
                str.Append($@"FROM [Excel 12.0;Database={file}].[{companyName}$] AS A ");
                str.Append("WHERE 日期 <> null");


                orderInfos.AddRange(_orderInfoService.GetOrderInfos(str.ToString()));

            }

            WriteExcel(orderInfos);

        }

        private bool WriteExcel(List<OrderInfo> orderInfos)
        {
            if (!orderInfos.Any()) return false;
            string[] header = new[] {"类别", "单号", "单位名称", "摘要", "日期", "上期余额", "本期出库金额", "本期回款金额", "期末余额"};
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("明细");
            IRow row = sheet.CreateRow(0);

            //标题写入 垂直水平居中
            for (int i = 0; i < header.Length; i++)
            {
                ICell cell = row.CreateCell(i);
                cell.SetCellValue(header[i]);
                cell.CellStyle = NpoiStyle.HorizontalVerticalCenter(workbook);
            }

            //数据写入
            for (int i = 0; i < orderInfos.Count; i++)
            {
                row = sheet.CreateRow(i + 1);
                row.CreateCell(0).SetCellValue(orderInfos[i].Category);
                row.CreateCell(1).SetCellValue(orderInfos[i].OrderNumber);
                row.CreateCell(2).SetCellValue(orderInfos[i].CustomerName);
                row.CreateCell(3).SetCellValue(orderInfos[i].Summary);
                row.CreateCell(4).SetCellValue(new SimpleDateFormat("yyyy-MM-dd").Format(orderInfos[i].OrderDateTime.Value.Date));
                row.CreateCell(5).SetCellValue(orderInfos[i].PreviousBalance.Value);
                row.CreateCell(6).SetCellValue(orderInfos[i].IssuedAmount.Value);
                row.CreateCell(7).SetCellValue(orderInfos[i].PaymentAmount.Value);
                row.CreateCell(8).SetCellValue(orderInfos[i].CurrentBalance.Value);
            }

            //row = sheet.CreateRow(sheet.LastRowNum + 1);
            

            //背景色填充
            for (int i = 0; i < sheet.LastRowNum - 1; i++)
            {
                row = sheet.GetRow(i);
                if (row.Cells[2].StringCellValue == "")
                {
                    for (int j = 0; j < row.LastCellNum; j++)
                    {
                        row.Cells[j].CellStyle = NpoiStyle.FillCellBackgroundColor(workbook);
                    }
                }
            }

            FileStream fileStream = File.Create(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\唐三彩对账单.xlsx");

            workbook.Write(fileStream);
            return true;
        }

    }
}