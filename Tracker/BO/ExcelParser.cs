using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tracker.Model;

namespace Tracker.BO
{
    public class ExcelParser
    {
        public static async Task<IList<Expense>> Parse(String FileName)
        {
            IList<Expense> expenseData = new List<Expense>();
            try
            {
                await Task.Run(() =>
                {
                    Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook workBook = excelApp.Workbooks.Open(FileName);
                    foreach (Microsoft.Office.Interop.Excel.Worksheet sheet in workBook.Worksheets)
                    {
                        foreach (Microsoft.Office.Interop.Excel.Range row in sheet.UsedRange.Rows)
                        {
                            if (!String.IsNullOrEmpty(sheet.Cells[row.Row, 1].Text) && sheet.Cells[row.Row, 1].Text != "Date")
                            {
                                expenseData.Add(new Expense
                                {
                                    Time = Convert.ToDateTime(sheet.Cells[row.Row, 1].Text),
                                    Description = sheet.Cells[row.Row, 2].Value2.ToString(),
                                    Amount = Convert.ToDouble(sheet.Cells[row.Row, 3].Value2.ToString())
                                });
                            }
                        }
                    }
                    excelApp.Quit();
                });

                return expenseData;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
    }
}
