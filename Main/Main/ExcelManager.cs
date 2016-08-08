using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Main
{
    public static class ExcelManager
    {
        public const string EXCEL2003 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=NO;IMEX=1'";

        public const string EXCEL2007 = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;HDR=NO;IMEX=1'";
        public static DataTable getDataFormExcel(string filepath)
        {
            DataTable dt = new DataTable();
            string constr = string.Format(EXCEL2007, filepath);
            using (var conn = new System.Data.OleDb.OleDbConnection(constr))
            {
                conn.Open();
                var myCommand = new System.Data.OleDb.OleDbDataAdapter("select * from [general_report$]", conn);
                myCommand.Fill(dt);
                conn.Close();
            }
            return dt;
        }
        public static DataTable ResetDataTableByRow(DataTable srcTable, int rowIndex, Dictionary<string, Type> nametype)
        {
            DataTable dt = new DataTable();
            if (rowIndex > 0)
            {
                for (int i = 0; i < srcTable.Columns.Count; i++)
                {
                    if (nametype.Keys.Contains(srcTable.Columns[i].ColumnName))
                    {

                        dt.Columns.Add(srcTable.Rows[rowIndex - 1][i].ToString(), nametype[srcTable.Columns[i].ColumnName]);
                    }
                    else {
                        dt.Columns.Add(srcTable.Rows[rowIndex - 1][i].ToString());
                    }
                }
                for (int i = 0; i < srcTable.Rows.Count; i++)
                {
                    if (i < rowIndex)
                    {
                        continue;
                    }
                    // dt.ImportRow(srcTable.Rows[i]);
                    var newrow = dt.NewRow();
                    for (int j = 0; j < srcTable.Columns.Count; j++)
                    {
                        newrow[j] = srcTable.Rows[i][j];

                    }
                    dt.Rows.Add(newrow);
                }
            }
            return dt;
        }

        public static DataTable GetAddData(DataTable tbold, DataTable tbnew, string keyname)
        {
            DataTable dttemp = new DataTable();
            dttemp.Merge(tbold);
            dttemp.Merge(tbnew);
            List<DataRow> list = new List<DataRow>();
            foreach (DataRow temp in dttemp.Rows)
            {
                foreach (DataRow old in tbold.Rows)
                {
                    if (temp[keyname].ToString() == old[keyname].ToString())
                    {
                        list.Add(temp);

                        break;
                    }
                }

            }
            list.ForEach(x => dttemp.Rows.Remove(x));

            return dttemp;

        }
        public static DataTable GetRemoveData(DataTable tbold, DataTable tbnew, string keyname)
        {

            DataTable dttemp = new DataTable();
            dttemp.Merge(tbold);
            dttemp.Merge(tbnew);
            List<DataRow> list = new List<DataRow>();
            foreach (DataRow temp in dttemp.Rows)
            {
                foreach (DataRow rownew in tbnew.Rows)
                {
                    if (temp[keyname].ToString() == rownew[keyname].ToString())
                    {
                        list.Add(temp);
                        break;
                    }
                }

            }
            list.ForEach(x => dttemp.Rows.Remove(x));
            return dttemp;
        }

        public static void NpoiExcel(DataTable dt, string filepath)
        {

            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet = book.CreateSheet("Sheet1");

            NPOI.SS.UserModel.IRow headerrow = sheet.CreateRow(0);
            ICellStyle style = book.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Center;
            style.VerticalAlignment = VerticalAlignment.Center;

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = headerrow.CreateCell(i);
                cell.CellStyle = style;
                cell.SetCellValue(dt.Columns[i].ColumnName);

            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                NPOI.SS.UserModel.IRow row = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = row.CreateCell(j);
                    cell.CellStyle = style;
                    if (dt.Columns[j].DataType == typeof(DateTime) && !string.IsNullOrEmpty(dt.Rows[i][j].ToString()))
                    {
                        cell.SetCellValue(DateTime.Parse(dt.Rows[i][j].ToString()).ToString("yyyy/MM/dd"));
                    }
                    else {
                        cell.SetCellValue(dt.Rows[i][j].ToString());
                    }
                }

            }
            using (var file = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                book.Write(file);
                book = null;
            }

        }
    }
}
