using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;

namespace Utility
{
    public class WordEdit
    {
        /// <summary>
        /// 应用程序对象。
        /// </summary>
        Word._Application app;
        /// <summary>
        /// 文档对象。
        /// </summary>
        Word._Document doc;
        /// <summary>
        /// 缺失对象。
        /// </summary>
        object missing = System.Reflection.Missing.Value;
        /// <summary>
        /// 构造方法。
        /// </summary>
        public WordEdit() {
            app = new Word.Application();
            app.Visible = true;
        }
        /// <summary>
        /// 创建新的实例。
        /// </summary>
        public void Create()
        {
            doc = app.Documents.Add(ref missing,ref missing,ref missing,ref missing);
        }
        /// <summary>
        /// 打开已存在的文件。
        /// </summary>
        /// <param name="path"></param>
        public void Open(string path)
        {
            object filename = path;
            doc = app.Documents.Open(ref filename, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
        }
        /// <summary>
        /// 导入模板。
        /// </summary>
        /// <param name="path">模板路径</param>
        public void Import(string path)
        {
            object filename = path;
            doc = app.Documents.Add(ref filename, ref missing, ref missing, ref missing);
        }
        /// <summary>
        /// 保存文件。
        /// </summary>
        /// <param name="path"></param>
        public void Save(string path)
        {
            object strFileName = path;
            doc.SaveAs(ref   strFileName, ref   missing, ref   missing, ref   missing, ref   missing, ref   missing, ref   missing, ref   missing, ref   missing, ref   missing, ref   missing, ref   missing, ref   missing, ref   missing, ref   missing, ref   missing);
            //关闭WordDoc文档对象   
            doc.Close(ref   missing, ref   missing, ref   missing);
            //关闭WordApp组件对象   
            app.Quit(ref   missing, ref   missing, ref   missing);   

        }
        /// <summary>
        /// 添加表格。
        /// </summary>
        public void AddTable()
        {
            object start = 0;
            object end = 0;
            Word.Range tableLocation = doc.Range(ref start, ref end);
            doc.Tables.Add(tableLocation, 3, 4, ref missing, ref missing);
        }
        /// <summary>
        /// 添加行。
        /// </summary>
        public void AddTableRow()
        {
            Word.Table newTable = doc.Tables[1];
            object beforeRow = newTable.Rows[1];
            newTable.Rows.Add(ref beforeRow);
        }
        /// <summary>
        /// 合并单元格。
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        public void EmergeCell(Word.Cell c1,Word.Cell c2)
        {
            //Word.Cell cell = newTable.Cell(1, 1);
            //cell.Merge(newTable.Cell(1, 2));
            c1.Merge(c2);
        }
        /// <summary>
        /// 拆分单元格。
        /// </summary>
        /// <param name="cell"></param>
        public void SplitCell(Word.Cell cell)
        {
            object Rownum = 2;
            object Columnnum = 2;
            cell.Split(ref Rownum, ref  Columnnum);
        }
        /// <summary>
        /// 插入段落。
        /// </summary>
        public void InsertParaGraph()
        {
            Word.Paragraph para;
            para = doc.Content.Paragraphs.Add(ref missing);
            para.Range.Text = "Heading 1";
            para.Range.Font.Bold = 1;
            para.Format.SpaceAfter = 24;    //24 pt spacing after paragraph.
            para.Range.InsertParagraphAfter();
            
        }
        public void AddShape()
        {
           Word.InlineShape shape = doc.InlineShapes[0];
           Word.OLEFormat olef = shape.OLEFormat;
            
            
        }
            //wordDoc.Tables.Item(k).Cell(1, 2).Range.Text = 
        //wordDoc.Tables.Item(1).Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightAtLeast;                        wordDoc.Tables.Item(1).Rows.Height = wordApp.CentimetersToPoints(float.Parse("0.8"));                        wordDoc.Tables.Item(1).Range.Font.Size = 10;                        wordDoc.Tables.Item(1).Range.Font.Name = "宋体";                        wordDoc.Tables.Item(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;                        wordDoc.Tables.Item(1).Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;                        //设置表格样式                        wordDoc.Tables.Item(1).Borders.Item(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle; 
    }
}

