using NPOI.SS.UserModel;

namespace TscStatement.Common
{
    public static class NpoiStyle
    {
        #region 垂直、水平居中
        /// <summary>
        /// 单元格垂直、水平居中
        /// </summary>
        /// <param name="workbook">工作簿</param>
        /// <returns>返回一个单元格样式</returns>
        public static ICellStyle HorizontalVerticalCenter(IWorkbook workbook)
        {
            ICellStyle cellstyle = workbook.CreateCellStyle(); ;
            cellstyle.VerticalAlignment = VerticalAlignment.Center;
            cellstyle.Alignment = HorizontalAlignment.Center;
            return cellstyle;
        }

        #endregion

        #region 颜色填充
        /// <summary>
        /// 单元格背景颜色填充
        /// </summary>
        /// <param name="workbook">工作簿</param>
        /// <returns>返回一个单元格样式</returns>
        public static ICellStyle FillCellBackgroundColor(IWorkbook workbook)
        {
            ICellStyle cellStyle = workbook.CreateCellStyle();
            cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Tan.Index;
            cellStyle.FillPattern = FillPattern.SolidForeground;
            return cellStyle;
        }

        #endregion
    }
}