using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Core;
using Microsoft.Office.Interop;
using Excel = Microsoft.Office.Interop.Excel;

/* 
 * 添加COM引用Microsoft Excel 12.0 Object Library
 * 或直接引用dll（Interop.Microsoft.Office.Core.dll，Microsoft.Office.Interop.Excel.dll，Microsoft.Vbe.Interop.dll）
 */
namespace Utility
{
    /// <summary>
    /// Excel编辑类。
    /// </summary>
    public class ExcelEdit
    {
        /// <summary>
        /// 文件名。
        /// </summary>
        public string filename;
        /// <summary>
        /// 应用程序。
        /// </summary>
        public Excel.Application application;
        /// <summary>
        /// 工作薄集合。
        /// </summary>
        public Excel.Workbooks workbooks;
        /// <summary>
        /// 工作薄。
        /// </summary>
        public Excel.Workbook workbook;
        /// <summary>
        /// 工作底表集合。
        /// </summary>
        public Excel.Worksheets worksheets;
        /// <summary>
        /// 工作底表。
        /// </summary>
        public Excel.Worksheet worksheet;
        /// <summary>
        /// 构造方法。
        /// </summary>
        public ExcelEdit()
        {
        }
        /// <summary>
        /// 创建一个Excel对象。
        /// </summary>
        public void Create()
        {
            application = new Excel.Application();
            workbooks = application.Workbooks;
            workbook = workbooks.Add(true);
        }
        /// <summary>
        /// 打开一个Excel文件。
        /// </summary>
        /// <param name="FileName">文件名</param>
        public void Open(string FileName)
        {
            application = new Excel.Application();
            workbooks = application.Workbooks;
            workbook = workbooks.Add(FileName.Trim());
            filename = FileName;
        }
        /// <summary>
        /// 获取一个工作表。
        /// </summary>
        /// <param name="SheetName">工作表名</param>
        /// <returns>工作表</returns>
        public Excel.Worksheet GetSheet(string SheetName)
        {
            Excel.Worksheet s = (Excel.Worksheet)workbook.Worksheets[SheetName];
            return s;
        }
        /// <summary>
        /// 添加一个工作表。
        /// </summary>
        /// <param name="SheetName">工作表名</param>
        /// <returns>工作表</returns>
        public Excel.Worksheet AddSheet(string SheetName)
        {
            Excel.Worksheet s = (Excel.Worksheet)workbook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            s.Name = SheetName;
            return s;
        }
        /// <summary>
        /// 删除一个工作表。
        /// </summary>
        /// <param name="SheetName">表名</param>
        public void DelSheet(string SheetName)
        {
            ((Excel.Worksheet)workbook.Worksheets[SheetName]).Delete();
        }
        /// <summary>
        /// 重命名一个工作表一。
        /// </summary>
        /// <param name="OldSheetName">旧的表名</param>
        /// <param name="NewSheetName">新的表名</param>
        /// <returns></returns>
        public Excel.Worksheet ReNameSheet(string OldSheetName, string NewSheetName)
        {
            Excel.Worksheet s = (Excel.Worksheet)workbook.Worksheets[OldSheetName];
            s.Name = NewSheetName;
            return s;
        }
        /// <summary>
        /// 重命名一个工作表二。
        /// </summary>
        /// <param name="Sheet">旧的表名</param>
        /// <param name="NewSheetName">新的表名</param>
        /// <returns>工作表</returns>
        public Excel.Worksheet ReNameSheet(Excel.Worksheet Sheet, string NewSheetName)
        {
            Sheet.Name = NewSheetName;
            return Sheet;
        }
        /// <summary>
        ///设置单元格。
        /// </summary>
        /// <param name="ws">要设值的工作表</param>
        /// <param name="x"> X行</param>
        /// <param name="y">Y列</param>
        /// <param name="value">值</param>
        public void SetCellValue(Excel.Worksheet ws, int x, int y, object value)
        {
            ws.Cells[x, y] = value;
        }
        /// <summary>
        /// 设置单元格。
        /// </summary>
        /// <param name="ws">要设值的工作表的名称</param>
        /// <param name="x">X行</param>
        /// <param name="y">Y列</param>
        /// <param name="value">value 值</param>
        public void SetCellValue(string ws, int x, int y, object value)
        {
            GetSheet(ws).Cells[x, y] = value;
        }
        /// <summary>
        /// 设置一个单元格的属性。
        /// </summary>
        /// <param name="ws">工作表</param>
        /// <param name="Startx">开始X</param>
        /// <param name="Starty">开始Y</param>
        /// <param name="Endx">结束Y</param>
        /// <param name="Endy">结束X</param>
        /// <param name="size">字体大小</param>
        /// <param name="name">字体名称</param>
        /// <param name="color">字体颜色</param>
        /// <param name="HorizontalAlignment">对齐方式</param>
        public void SetCellProperty(Excel.Worksheet ws, int Startx, int Starty, int Endx, int Endy, int size, string name, Excel.Constants color, Excel.Constants HorizontalAlignment)
        {
            name = "宋体";
            size = 12;
            color = Excel.Constants.xlAutomatic;
            HorizontalAlignment = Excel.Constants.xlRight;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Name = name;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Size = size;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Color = color;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).HorizontalAlignment = HorizontalAlignment;
        }
        /// <summary>
        /// 设置单元格属性。
        /// </summary>
        /// <param name="wsn">工作表名称</param>
        /// <param name="Startx">开始x</param>
        /// <param name="Starty">开始Y</param>
        /// <param name="Endx"></param>
        /// <param name="Endy"></param>
        /// <param name="size">字体大小</param>
        /// <param name="name">字体名称</param>
        /// <param name="color">字体颜色</param>
        /// <param name="HorizontalAlignment"></param>
        public void SetCellProperty(string wsn, int Startx, int Starty, int Endx, int Endy, int size, string name, Excel.Constants color, Excel.Constants HorizontalAlignment)
        {
            name = "宋体";
            size = 12;
            color = Excel.Constants.xlAutomatic;
            HorizontalAlignment = Excel.Constants.xlRight;
            Excel.Worksheet ws = GetSheet(wsn);
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Name = name;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Size = size;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Color = color;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).HorizontalAlignment = HorizontalAlignment;
        }
        /// <summary>
        /// 合并单元格。
        /// </summary>
        /// <param name="ws">工作表</param>
        /// <param name="x1">x1</param>
        /// <param name="y1">y1</param>
        /// <param name="x2">x2</param>
        /// <param name="y2">y2</param>
        public void UniteCells(Excel.Worksheet ws, int x1, int y1, int x2, int y2)
        {
            ws.get_Range(ws.Cells[x1, y1], ws.Cells[x2, y2]).Merge(Type.Missing);
        }
        /// <summary>
        /// 合并单元格。
        /// </summary>
        /// <param name="ws">工作表名</param>
        /// <param name="x1">x1</param>
        /// <param name="y1">y1</param>
        /// <param name="x2">x2</param>
        /// <param name="y2">y2</param>
        public void UniteCells(string ws, int x1, int y1, int x2, int y2)
        {
            GetSheet(ws).get_Range(GetSheet(ws).Cells[x1, y1], GetSheet(ws).Cells[x2, y2]).Merge(Type.Missing);
        }
        /// <summary>
        /// 将内存中数据表格插入到Excel指定工作表的指定位置。
        /// </summary>
        /// <param name="dt">datatable</param>
        /// <param name="ws">工作表</param>
        /// <param name="startX">x</param>
        /// <param name="startY">y</param>
        public void InsertTable(System.Data.DataTable dt, string ws, int startX, int startY)
        {

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                for (int j = 0; j <= dt.Columns.Count - 1; j++)
                {
                    GetSheet(ws).Cells[startX + i, j + startY] = dt.Rows[i][j].ToString();
                }
            }
        }
        /// <summary>
        /// 将内存中数据表格插入到Excel指定工作表的指定位置二。
        /// </summary>
        /// <param name="dt">datatable</param>
        /// <param name="ws">工作表</param>
        /// <param name="startX">x</param>
        /// <param name="startY">y</param>
        public void InsertTable(System.Data.DataTable dt, Excel.Worksheet ws, int startX, int startY)
        {
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                for (int j = 0; j <= dt.Columns.Count - 1; j++)
                {
                    ws.Cells[startX + i, j + startY] = dt.Rows[i][j];
                }
            }
        }
        /// <summary>
        /// 插入图表操作。
        /// </summary>
        /// <param name="ChartType">图表样式</param>
        /// <param name="ws">工作表名</param>
        /// <param name="DataSourcesX1">x1</param>
        /// <param name="DataSourcesY1">y1</param>
        /// <param name="DataSourcesX2">x2</param>
        /// <param name="DataSourcesY2">y2</param>
        /// <param name="ChartDataType">以列或以行做表的维度</param>
        public void InsertActiveChart(Excel.XlChartType ChartType, string ws, int DataSourcesX1, int DataSourcesY1, int DataSourcesX2, int DataSourcesY2, Excel.XlRowCol ChartDataType)
        {
            workbook.Charts.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            {
                workbook.ActiveChart.ChartType = ChartType;
                workbook.ActiveChart.SetSourceData(GetSheet(ws).get_Range(GetSheet(ws).Cells[DataSourcesX1, DataSourcesY1], GetSheet(ws).Cells[DataSourcesX2, DataSourcesY2]), ChartDataType);
                workbook.ActiveChart.Location(Excel.XlChartLocation.xlLocationAsObject, ws);
            }
            workbook.Charts.get_Item(0);
        }
        /// <summary>
        /// 保存文档。
        /// </summary>
        /// <returns>是否保存成功</returns>
        public bool Save()
        {
            if (filename == "")
            {
                return false;
            }
            else
            {
                try
                {
                    workbook.Save();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 文档另存为。
        /// </summary>
        /// <param name="FileName">文件名</param>
        /// <returns>是否保存成功</returns>
        public bool SaveAs(object FileName)
        {
            try
            {
                workbook.SaveAs(FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 关闭一个Excel对象，销毁对象。
        /// </summary>
        public void Close()
        {
            workbook.Close(Type.Missing, Type.Missing, Type.Missing);
            workbooks.Close();
            application.Quit();
            workbook = null;
            workbooks = null;
            application = null;
            GC.Collect();
        }
        /*
        /// <summary>
        /// 插入图片操作一
        /// </summary>
        /// <param name="Filename">文件名</param>
        /// <param name="ws">工作表</param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        public void InsertPictures(string Filename, string ws, int x1, int y1, int x2, int y2)
        {
            GetSheet(ws).Shapes.AddPicture(Filename, MsoTriState.msoFalse, MsoTriState.msoTrue, x1, y1, x2, y2);
        }
        /// <summary>
        /// 插入图片操作二。
        /// </summary>
        /// <param name="Filename">文件名</param>
        /// <param name="ws">工作表</param>
        /// <param name="Height">高</param>
        /// <param name="Width">宽</param>
        /// <param name="x1">x1</param>
        /// <param name="y1">y1</param>
        /// <param name="x2">x2</param>
        /// <param name="y2">y2</param>
        public void InsertPictures(string Filename, string ws, int Height, int Width, int x1, int y1, int x2, int y2)
        {
            GetSheet(ws).Shapes.AddPicture(Filename, MsoTriState.msoFalse, MsoTriState.msoTrue, x1, y1, x2, y2);
            GetSheet(ws).Shapes.get_Range(Type.Missing).Height = Height;
            GetSheet(ws).Shapes.get_Range(Type.Missing).Width = Width;
        }
        /// <summary>
        /// 插入图片操作三。
        /// </summary>
        /// <param name="Filename">文件名</param>
        /// <param name="ws">工作表</param>
        /// <param name="left">左</param>
        /// <param name="top">上</param>
        /// <param name="Height">高</param>
        /// <param name="Width">宽</param>
        /// <param name="x1">x1</param>
        /// <param name="y1">y1</param>
        /// <param name="x2">x2</param>
        /// <param name="y2">y2</param>
        public void InsertPictures(string Filename, string ws, int left, int top, int Height, int Width, int x1, int y1, int x2, int y2)
        {
            GetSheet(ws).Shapes.AddPicture(Filename, MsoTriState.msoFalse, MsoTriState.msoTrue, x1, y1, x2, y2);
            GetSheet(ws).Shapes.get_Range(Type.Missing).IncrementLeft(left);
            GetSheet(ws).Shapes.get_Range(Type.Missing).IncrementTop(top);
            GetSheet(ws).Shapes.get_Range(Type.Missing).Height = Height;
            GetSheet(ws).Shapes.get_Range(Type.Missing).Width = Width;
        }
        */
    }
}  


