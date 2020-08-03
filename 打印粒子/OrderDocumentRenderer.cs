using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace 打印粒子
{
    class OrderDocumentRenderer :IDocumentRenderer
    {
        public void Render(FlowDocument doc, object data)
        {   //TableRowGroup 表示用于分组 Table 中的 TableRow 元素的流内容元素
            TableRowGroup group = doc.FindName("rowsDetails") as TableRowGroup;
            //设置流元素属性
            Style styleCell = doc.Resources["BorderedCell"] as Style;
            foreach (OrderDetail item in ((OrderData)data).OrderDetails)
            {
                //TableRow 定义 Table 中行的流内容元素。
                TableRow row = new TableRow();
                //为表头的列赋值
                TableCell cell = new TableCell(new Paragraph(new Run(item.Sku)));
                cell.Style = styleCell;
                row.Cells.Add(cell);
                /*
                 * Paragraph 用于将内容分组到一个段落中的块级别流内容元素。
                 * Run 应包含一连串格式化或未格式化文本的内联级别的流内容元素。
                 * TableCell 定义 Table 中内容单元格的流内容元素。
                 * 
                 * 将指定的文本以流的形式存放到指定的表格单元格中
                 * 
                 */
                cell = new TableCell(new Paragraph(new Run(item.Spec)));
                //为 单元格设置风格
                cell.Style = styleCell;
                //将单元格流内容添加到让及流内容行元素中
                row.Cells.Add(cell);

                cell = new TableCell(new Paragraph(new Run(item.Number.ToString(CultureInfo.InvariantCulture))));
                cell.Style = styleCell;
                row.Cells.Add(cell);

                cell = new TableCell(new Paragraph(new Run(item.Unit)));
                cell.Style = styleCell;
                row.Cells.Add(cell);

                cell = new TableCell(new Paragraph(new Run(item.UnitPrice.ToString(CultureInfo.InvariantCulture))));
                cell.Style = styleCell;
                row.Cells.Add(cell);

                cell = new TableCell(new Paragraph(new Run((item.Number * item.UnitPrice).ToString(CultureInfo.InvariantCulture))));
                cell.Style = styleCell;
                row.Cells.Add(cell);

                cell = new TableCell(new Paragraph(new Run((item.Description))));
                cell.Style = styleCell;
                row.Cells.Add(cell);
                //将行元素添加到表格流元素中
                group.Rows.Add(row);
            }
        }
    }
}
