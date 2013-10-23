using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Core;
using Microsoft.Office.Interop;
using Excel = Microsoft.Office.Interop.Excel;

/* 
 * ���COM����Microsoft Excel 12.0 Object Library
 * ��ֱ������dll��Interop.Microsoft.Office.Core.dll��Microsoft.Office.Interop.Excel.dll��Microsoft.Vbe.Interop.dll��
 */
namespace Utility
{
    /// <summary>
    /// Excel�༭�ࡣ
    /// </summary>
    public class ExcelEdit
    {
        /// <summary>
        /// �ļ�����
        /// </summary>
        public string filename;
        /// <summary>
        /// Ӧ�ó���
        /// </summary>
        public Excel.Application application;
        /// <summary>
        /// ���������ϡ�
        /// </summary>
        public Excel.Workbooks workbooks;
        /// <summary>
        /// ��������
        /// </summary>
        public Excel.Workbook workbook;
        /// <summary>
        /// �����ױ��ϡ�
        /// </summary>
        public Excel.Worksheets worksheets;
        /// <summary>
        /// �����ױ�
        /// </summary>
        public Excel.Worksheet worksheet;
        /// <summary>
        /// ���췽����
        /// </summary>
        public ExcelEdit()
        {
        }
        /// <summary>
        /// ����һ��Excel����
        /// </summary>
        public void Create()
        {
            application = new Excel.Application();
            workbooks = application.Workbooks;
            workbook = workbooks.Add(true);
        }
        /// <summary>
        /// ��һ��Excel�ļ���
        /// </summary>
        /// <param name="FileName">�ļ���</param>
        public void Open(string FileName)
        {
            application = new Excel.Application();
            workbooks = application.Workbooks;
            workbook = workbooks.Add(FileName.Trim());
            filename = FileName;
        }
        /// <summary>
        /// ��ȡһ��������
        /// </summary>
        /// <param name="SheetName">��������</param>
        /// <returns>������</returns>
        public Excel.Worksheet GetSheet(string SheetName)
        {
            Excel.Worksheet s = (Excel.Worksheet)workbook.Worksheets[SheetName];
            return s;
        }
        /// <summary>
        /// ���һ��������
        /// </summary>
        /// <param name="SheetName">��������</param>
        /// <returns>������</returns>
        public Excel.Worksheet AddSheet(string SheetName)
        {
            Excel.Worksheet s = (Excel.Worksheet)workbook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            s.Name = SheetName;
            return s;
        }
        /// <summary>
        /// ɾ��һ��������
        /// </summary>
        /// <param name="SheetName">����</param>
        public void DelSheet(string SheetName)
        {
            ((Excel.Worksheet)workbook.Worksheets[SheetName]).Delete();
        }
        /// <summary>
        /// ������һ��������һ��
        /// </summary>
        /// <param name="OldSheetName">�ɵı���</param>
        /// <param name="NewSheetName">�µı���</param>
        /// <returns></returns>
        public Excel.Worksheet ReNameSheet(string OldSheetName, string NewSheetName)
        {
            Excel.Worksheet s = (Excel.Worksheet)workbook.Worksheets[OldSheetName];
            s.Name = NewSheetName;
            return s;
        }
        /// <summary>
        /// ������һ�����������
        /// </summary>
        /// <param name="Sheet">�ɵı���</param>
        /// <param name="NewSheetName">�µı���</param>
        /// <returns>������</returns>
        public Excel.Worksheet ReNameSheet(Excel.Worksheet Sheet, string NewSheetName)
        {
            Sheet.Name = NewSheetName;
            return Sheet;
        }
        /// <summary>
        ///���õ�Ԫ��
        /// </summary>
        /// <param name="ws">Ҫ��ֵ�Ĺ�����</param>
        /// <param name="x"> X��</param>
        /// <param name="y">Y��</param>
        /// <param name="value">ֵ</param>
        public void SetCellValue(Excel.Worksheet ws, int x, int y, object value)
        {
            ws.Cells[x, y] = value;
        }
        /// <summary>
        /// ���õ�Ԫ��
        /// </summary>
        /// <param name="ws">Ҫ��ֵ�Ĺ����������</param>
        /// <param name="x">X��</param>
        /// <param name="y">Y��</param>
        /// <param name="value">value ֵ</param>
        public void SetCellValue(string ws, int x, int y, object value)
        {
            GetSheet(ws).Cells[x, y] = value;
        }
        /// <summary>
        /// ����һ����Ԫ������ԡ�
        /// </summary>
        /// <param name="ws">������</param>
        /// <param name="Startx">��ʼX</param>
        /// <param name="Starty">��ʼY</param>
        /// <param name="Endx">����Y</param>
        /// <param name="Endy">����X</param>
        /// <param name="size">�����С</param>
        /// <param name="name">��������</param>
        /// <param name="color">������ɫ</param>
        /// <param name="HorizontalAlignment">���뷽ʽ</param>
        public void SetCellProperty(Excel.Worksheet ws, int Startx, int Starty, int Endx, int Endy, int size, string name, Excel.Constants color, Excel.Constants HorizontalAlignment)
        {
            name = "����";
            size = 12;
            color = Excel.Constants.xlAutomatic;
            HorizontalAlignment = Excel.Constants.xlRight;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Name = name;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Size = size;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Color = color;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).HorizontalAlignment = HorizontalAlignment;
        }
        /// <summary>
        /// ���õ�Ԫ�����ԡ�
        /// </summary>
        /// <param name="wsn">����������</param>
        /// <param name="Startx">��ʼx</param>
        /// <param name="Starty">��ʼY</param>
        /// <param name="Endx"></param>
        /// <param name="Endy"></param>
        /// <param name="size">�����С</param>
        /// <param name="name">��������</param>
        /// <param name="color">������ɫ</param>
        /// <param name="HorizontalAlignment"></param>
        public void SetCellProperty(string wsn, int Startx, int Starty, int Endx, int Endy, int size, string name, Excel.Constants color, Excel.Constants HorizontalAlignment)
        {
            name = "����";
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
        /// �ϲ���Ԫ��
        /// </summary>
        /// <param name="ws">������</param>
        /// <param name="x1">x1</param>
        /// <param name="y1">y1</param>
        /// <param name="x2">x2</param>
        /// <param name="y2">y2</param>
        public void UniteCells(Excel.Worksheet ws, int x1, int y1, int x2, int y2)
        {
            ws.get_Range(ws.Cells[x1, y1], ws.Cells[x2, y2]).Merge(Type.Missing);
        }
        /// <summary>
        /// �ϲ���Ԫ��
        /// </summary>
        /// <param name="ws">��������</param>
        /// <param name="x1">x1</param>
        /// <param name="y1">y1</param>
        /// <param name="x2">x2</param>
        /// <param name="y2">y2</param>
        public void UniteCells(string ws, int x1, int y1, int x2, int y2)
        {
            GetSheet(ws).get_Range(GetSheet(ws).Cells[x1, y1], GetSheet(ws).Cells[x2, y2]).Merge(Type.Missing);
        }
        /// <summary>
        /// ���ڴ������ݱ����뵽Excelָ���������ָ��λ�á�
        /// </summary>
        /// <param name="dt">datatable</param>
        /// <param name="ws">������</param>
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
        /// ���ڴ������ݱ����뵽Excelָ���������ָ��λ�ö���
        /// </summary>
        /// <param name="dt">datatable</param>
        /// <param name="ws">������</param>
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
        /// ����ͼ�������
        /// </summary>
        /// <param name="ChartType">ͼ����ʽ</param>
        /// <param name="ws">��������</param>
        /// <param name="DataSourcesX1">x1</param>
        /// <param name="DataSourcesY1">y1</param>
        /// <param name="DataSourcesX2">x2</param>
        /// <param name="DataSourcesY2">y2</param>
        /// <param name="ChartDataType">���л����������ά��</param>
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
        /// �����ĵ���
        /// </summary>
        /// <returns>�Ƿ񱣴�ɹ�</returns>
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
        /// �ĵ����Ϊ��
        /// </summary>
        /// <param name="FileName">�ļ���</param>
        /// <returns>�Ƿ񱣴�ɹ�</returns>
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
        /// �ر�һ��Excel�������ٶ���
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
        /// ����ͼƬ����һ
        /// </summary>
        /// <param name="Filename">�ļ���</param>
        /// <param name="ws">������</param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        public void InsertPictures(string Filename, string ws, int x1, int y1, int x2, int y2)
        {
            GetSheet(ws).Shapes.AddPicture(Filename, MsoTriState.msoFalse, MsoTriState.msoTrue, x1, y1, x2, y2);
        }
        /// <summary>
        /// ����ͼƬ��������
        /// </summary>
        /// <param name="Filename">�ļ���</param>
        /// <param name="ws">������</param>
        /// <param name="Height">��</param>
        /// <param name="Width">��</param>
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
        /// ����ͼƬ��������
        /// </summary>
        /// <param name="Filename">�ļ���</param>
        /// <param name="ws">������</param>
        /// <param name="left">��</param>
        /// <param name="top">��</param>
        /// <param name="Height">��</param>
        /// <param name="Width">��</param>
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


