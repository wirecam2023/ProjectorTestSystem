﻿using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ProjectorInformation_Common
{
    public static class ExcelHelperForCs
    {
        #region 私有方法
        /// <summary>
        /// 获取要保存的文件名称（含完整路径）
        /// </summary>
        /// <returns></returns>
        private static string GetSaveFilePath()
        {
            SaveFileDialog savaFileDig = new SaveFileDialog();
            savaFileDig.Filter = "Excel Office97-2003(*.xls)|*.xls|Excel Office2007及以上(*.xlsx)|*.xlsx";
            savaFileDig.FilterIndex = 0;
            savaFileDig.Title = "导出到";
            savaFileDig.OverwritePrompt = true;
            savaFileDig.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string filePath = null;
            if (savaFileDig.ShowDialog() == DialogResult.OK)
            {
                filePath = savaFileDig.FileName;
            }
            return filePath;
        }
        /// <summary>
        /// 获取要打开要导入的文件名称（含完整路径）
        /// </summary>
        /// <returns></returns>
        private static string GetOpenFilePath()
        {
            OpenFileDialog openFileDig = new OpenFileDialog();
            openFileDig.Filter = "Excel Office97-2003(*.xls)|*.xls|Excel Office2007及以上(*.xlsx)|*.xlsx";
            openFileDig.FilterIndex = 0;
            openFileDig.Title = "打开";
            openFileDig.CheckFileExists = true;
            openFileDig.CheckPathExists = true;
            openFileDig.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string filePath = null;
            if (openFileDig.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDig.FileName;
            }

            return filePath;
        }
        /// <summary>
        /// 判断是否为兼容模式
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private static bool GetIsCompatible(string filepath)
        {
            return filepath.EndsWith(".xsl",StringComparison.OrdinalIgnoreCase);
        }
        /// <summary>
        /// 创建工作薄
        /// </summary>
        /// <param name="isCompatible"></param>
        /// <returns></returns>
        private static IWorkbook CreateWorkbook(bool isCompatible)
        {
            if (isCompatible)
            {
                return new HSSFWorkbook();
            }
            else
            {
                return new XSSFWorkbook();
            }
        }

        /// <summary>
        /// 创建表格头单元格
        /// </summary>
        /// <param name="sheet"></param>
        /// <returns></returns>
        private static ICellStyle GetCellStyle(IWorkbook workbook)
        {
            ICellStyle style = workbook.CreateCellStyle();
            style.FillPattern = FillPattern.SolidForeground;
            style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
 
            return style;
        }

        /// <summary>
        /// 创建工作薄(依据文件流)
        /// </summary>
        /// <param name="isCompatible"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        private static IWorkbook CreateWorkbook(bool isCompatible, dynamic stream)
        {
            if (isCompatible)
            {
                return new HSSFWorkbook(stream);
            }
            else
            {
                return new XSSFWorkbook(stream);
            }
        }
        
        /// <summary>
        /// 从工作表中生成DataTable
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="headerRowIndex"></param>
        /// <returns></returns>
        private static DataTable GetDataTableFromSheet(ISheet sheet, int headerRowIndex)
        {
            DataTable table = new DataTable();
 
            IRow headerRow = sheet.GetRow(headerRowIndex);
            int cellCount = headerRow.LastCellNum;
 
            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                try
                {
                    if (headerRow.GetCell(i) == null || headerRow.GetCell(i).StringCellValue.Trim() == "")
                    {
                        // 如果遇到第一个空列，则不再继续向后读取
                        cellCount = i;
                        break;
                    }
                    if (!table.Columns.Contains(headerRow.GetCell(i).StringCellValue))
                    {
                        DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                        table.Columns.Add(column);
                    }
                }
                catch
                {
                    try
                    {
                        if (headerRow.GetCell(i).DateCellValue.Year < 2000)
                        {
                            if (headerRow.GetCell(i) == null || headerRow.GetCell(i).NumericCellValue.ToString().Trim() == "")
                            {
                                // 如果遇到第一个空列，则不再继续向后读取
                                cellCount = i;
                                break;
                            }
                            if (!table.Columns.Contains(headerRow.GetCell(i).NumericCellValue.ToString().Trim()))
                            {
                                DataColumn column = new DataColumn(headerRow.GetCell(i).NumericCellValue.ToString().Trim());
                                table.Columns.Add(column);
                            }

                        }
                        else
                        {
                            if (headerRow.GetCell(i) == null || headerRow.GetCell(i).DateCellValue.ToString().Trim() == "")
                            {
                                // 如果遇到第一个空列，则不再继续向后读取
                                cellCount = i;
                                break;
                            }
                            if (!table.Columns.Contains(headerRow.GetCell(i).DateCellValue.ToString().Trim()))
                            {
                                DataColumn column = new DataColumn(headerRow.GetCell(i).DateCellValue.ToString().Trim());
                                table.Columns.Add(column);
                            }

                        }
                    }
                    catch
                    {
                        if (headerRow.GetCell(i) == null || headerRow.GetCell(i).NumericCellValue.ToString().Trim() == "")
                        {
                            // 如果遇到第一个空列，则不再继续向后读取
                            cellCount = i;
                            break;
                        }
                        if (!table.Columns.Contains(headerRow.GetCell(i).NumericCellValue.ToString().Trim()))
                        {
                            DataColumn column = new DataColumn(headerRow.GetCell(i).NumericCellValue.ToString().Trim());
                            table.Columns.Add(column);
                        }

                    }
                    
                    continue;
                }
            }
            //headerRowIndex+1去表头
            for (int i = (headerRowIndex); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                //如果遇到某行的第一个单元格的值为空，则不再继续向下读取
                if (row != null && !string.IsNullOrEmpty(row.GetCell(0).ToString()))
                {
                    DataRow dataRow = table.NewRow();
 
                    for (int j = row.FirstCellNum; j < table.Columns.Count; j++)
                    {
                        dataRow[j] = row.GetCell(j).ToString();
                    }
 
                    table.Rows.Add(dataRow);
                }
            }
 
            return table;
        }
 
        #endregion
 
        #region 公共导出方法
 
        /// <summary>
        /// 由DataSet导出Excel
        /// </summary>
        /// <param name="sourceTable">要导出数据的DataTable</param>
        /// <returns>Excel工作表</returns>
        public static string ExportToExcel(DataSet sourceDs, string filePath = null)
        {
 
            if (string.IsNullOrEmpty(filePath))
            {
                filePath = GetSaveFilePath();
            }
 
            if (string.IsNullOrEmpty(filePath)) return null;
 
            bool isCompatible = GetIsCompatible(filePath);
 
            IWorkbook workbook = CreateWorkbook(isCompatible);
            ICellStyle cellStyle = GetCellStyle(workbook);
 
            for (int i = 0; i < sourceDs.Tables.Count; i++)
            {
                DataTable table = sourceDs.Tables[i];
                string sheetName = "result" + i.ToString();
                ISheet sheet = workbook.CreateSheet(sheetName);
                IRow headerRow = sheet.CreateRow(0);
                // handling header.
                foreach (DataColumn column in table.Columns)
                {
                    ICell cell = headerRow.CreateCell(column.Ordinal);
                    cell.SetCellValue(column.ColumnName);
                    cell.CellStyle = cellStyle;
                }
 
                // handling value.
                int rowIndex = 1;
 
                foreach (DataRow row in table.Rows)
                {
                    IRow dataRow = sheet.CreateRow(rowIndex);
 
                    foreach (DataColumn column in table.Columns)
                    {
                        dataRow.CreateCell(column.Ordinal).SetCellValue((row[column] ?? "").ToString());
                    }
 
                    rowIndex++;
                }
            }
 
            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            workbook.Write(fs);
            fs.Dispose();
            workbook = null;
 
            return filePath;
 
        }
 
 
        /// <summary>
        /// 由DataTable导出Excel
        /// </summary>
        /// <param name="sourceTable">要导出数据的DataTable</param>
        /// <returns>Excel工作表</returns>
        public static string ExportToExcel(DataTable sourceTable, string sheetName = "result", string filePath = null)
        {
            if (sourceTable.Rows.Count <= 0) return null;
 
            if (string.IsNullOrEmpty(filePath))
            {
                filePath = GetSaveFilePath();
            }
 
            if (string.IsNullOrEmpty(filePath)) return null;
 
            bool isCompatible = GetIsCompatible(filePath);
 
            IWorkbook workbook = CreateWorkbook(isCompatible);
            ICellStyle cellStyle = GetCellStyle(workbook);
 
            ISheet sheet = workbook.CreateSheet(sheetName);
            IRow headerRow = sheet.CreateRow(0);
            // handling header.
            foreach (DataColumn column in sourceTable.Columns)
            {
                ICell headerCell = headerRow.CreateCell(column.Ordinal);
                headerCell.SetCellValue(column.ColumnName);
                headerCell.CellStyle = cellStyle;
            }
 
            // handling value.
            int rowIndex = 1;
 
            foreach (DataRow row in sourceTable.Rows)
            {
                IRow dataRow = sheet.CreateRow(rowIndex);
 
                foreach (DataColumn column in sourceTable.Columns)
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue((row[column]??"").ToString());
                }
 
                rowIndex++;
            }
            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            workbook.Write(fs);
            fs.Dispose();
 
            sheet = null;
            headerRow = null;
            workbook = null;
 
            return filePath;
        }
 
        /// <summary>
        /// 由List导出Excel
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="data">在导出的List</param>
        /// <param name="sheetName">sheet名称</param>
        /// <returns></returns>
        public static string ExportToExcel<T>(List<T> data, IList<KeyValuePair<string, string>> headerNameList, string sheetName = "result", string filePath = null) where T : class
        {
            if (data.Count <= 0) return null;
 
            if (string.IsNullOrEmpty(filePath))
            {
                filePath = GetSaveFilePath();
            }
 
            if (string.IsNullOrEmpty(filePath)) return null;
 
            bool isCompatible = GetIsCompatible(filePath);
 
            IWorkbook workbook = CreateWorkbook(isCompatible);
            ICellStyle cellStyle = GetCellStyle(workbook);
            ISheet sheet = workbook.CreateSheet(sheetName);
            IRow headerRow = sheet.CreateRow(0);
 
            for (int i = 0; i < headerNameList.Count; i++)
            {
                ICell cell = headerRow.CreateCell(i);
                cell.SetCellValue(headerNameList[i].Value);
                cell.CellStyle = cellStyle;
            }
 
            Type t = typeof(T);
            int rowIndex = 1;
            foreach (T item in data)
            {
                IRow dataRow = sheet.CreateRow(rowIndex);
                for (int n = 0; n < headerNameList.Count; n++)
                {
                    object pValue = t.GetProperty(headerNameList[n].Key).GetValue(item, null);
                    dataRow.CreateCell(n).SetCellValue((pValue ?? "").ToString());
                }
                rowIndex++;
            }
            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            workbook.Write(fs);
            fs.Dispose();
 
            sheet = null;
            headerRow = null;
            workbook = null;
 
            return filePath;
        }
 
        /// <summary>
        /// 由DataGridView导出
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="sheetName"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ExportToExcel(DataGridView grid, string sheetName = "result", string filePath = null)
        {
            if (grid.Rows.Count <= 0) return null;
 
            if (string.IsNullOrEmpty(filePath))
            {
                filePath = GetSaveFilePath();
            }
 
            if (string.IsNullOrEmpty(filePath)) return null;
 
            bool isCompatible = GetIsCompatible(filePath);
 
            IWorkbook workbook = CreateWorkbook(isCompatible);
            ICellStyle cellStyle = GetCellStyle(workbook);
            ISheet sheet = workbook.CreateSheet(sheetName);
 
            IRow headerRow = sheet.CreateRow(0);
 
            for (int i = 0; i < grid.Columns.Count; i++)
            {
                if (grid.Columns[i].Visible)
                {
                    ICell cell = headerRow.CreateCell(i);
                    cell.SetCellValue(grid.Columns[i].HeaderText);
                    cell.CellStyle = cellStyle;
                }
            }
 
            int rowIndex = 1;
            foreach (DataGridViewRow row in grid.Rows)
            {
                IRow dataRow = sheet.CreateRow(rowIndex);
                for (int n = 0; n < grid.Columns.Count; n++)
                {
                    if (grid.Columns[n].Visible)
                    {
                        dataRow.CreateCell(n).SetCellValue((row.Cells[n].Value ?? "").ToString());
                    }
                }
                rowIndex++;
            }
 
            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            workbook.Write(fs);
            fs.Dispose();
 
            sheet = null;
            headerRow = null;
            workbook = null;
 
            return filePath;
        }

        /// <summary>
        /// 由DataGridView导出
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="sheetName"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ExportToExcelproductionEfficiency(DataGridView grid, string OnWorkClass, int Number_Min, bool BlankLine, string sheetName = "result", string filePath = null)
        {
            if (grid.Rows.Count <= 0) return null;

            if (string.IsNullOrEmpty(filePath))
            {
                filePath = GetSaveFilePath();
            }

            if (string.IsNullOrEmpty(filePath)) return null;

            bool isCompatible = GetIsCompatible(filePath);

            IWorkbook workbook = CreateWorkbook(isCompatible);
            ICellStyle cellStyle = GetCellStyle(workbook);
            ISheet sheet = workbook.CreateSheet(sheetName);

            IRow headerRow = sheet.CreateRow(0);

            for (int i = 0; i < grid.Columns.Count; i++)
            {
                if (grid.Columns[i].Visible)
                {
                    ICell cell = headerRow.CreateCell(i);
                    cell.SetCellValue(grid.Columns[i].HeaderText);
                    cell.CellStyle = cellStyle;
                }
            }

            int rowIndex = 1;
            foreach (DataGridViewRow row in grid.Rows)//循环建立行
            {
                if (OnWorkClass == "所有工位")
                {
                    if (BlankLine)
                    {
                        if ( row.Cells["ProductionClass"].Value.ToString() != "" && Convert.ToInt32(row.Cells["sum"].Value) >= Number_Min)
                        {
                            IRow dataRow = sheet.CreateRow(rowIndex);
                            for (int n = 0; n < grid.Columns.Count; n++)
                            {
                                if (grid.Columns[n].Visible)
                                {
                                    dataRow.CreateCell(n).SetCellValue((row.Cells[n].Value ?? "").ToString());
                                }
                            }
                            rowIndex++;
                        }
                        else
                        {
                            IRow dataRow = sheet.CreateRow(rowIndex);
                            for (int n = 0; n < grid.Columns.Count; n++)
                            {
                                if (grid.Columns[n].Visible)
                                {
                                    dataRow.CreateCell(n).SetCellValue((row.Cells[n].Value ?? "").ToString());
                                }
                            }
                            rowIndex++;
                        }
                    }
                    else
                    {
                        if (row.Cells["ProductionClass"].Value.ToString() != "" && Convert.ToInt32(row.Cells["sum"].Value) >= Number_Min)
                        {
                            IRow dataRow = sheet.CreateRow(rowIndex);
                            for (int n = 0; n < grid.Columns.Count; n++)
                            {
                                if (grid.Columns[n].Visible)
                                {
                                    dataRow.CreateCell(n).SetCellValue((row.Cells[n].Value ?? "").ToString());
                                }
                            }
                            rowIndex++;
                        }
                    }
                }
                else
                {
                    if (row.Cells["ProductionClass"].Value.ToString() == OnWorkClass || row.Cells["ProductionClass"].Value.ToString() == "")
                    {
                        if (BlankLine)
                        {
                            if (row.Cells["ProductionClass"].Value.ToString() != "" && Convert.ToInt32(row.Cells["sum"].Value) >= Number_Min)
                            {
                                IRow dataRow = sheet.CreateRow(rowIndex);
                                for (int n = 0; n < grid.Columns.Count; n++)
                                {
                                    if (grid.Columns[n].Visible)
                                    {
                                        dataRow.CreateCell(n).SetCellValue((row.Cells[n].Value ?? "").ToString());
                                    }
                                }
                                rowIndex++;
                            }
                            else
                            {
                                IRow dataRow = sheet.CreateRow(rowIndex);
                                for (int n = 0; n < grid.Columns.Count; n++)
                                {
                                    if (grid.Columns[n].Visible)
                                    {
                                        dataRow.CreateCell(n).SetCellValue((row.Cells[n].Value ?? "").ToString());
                                    }
                                }
                                rowIndex++;
                            }
                        }
                        else
                        {
                            if (row.Cells["ProductionClass"].Value.ToString() != "" && Convert.ToInt32(row.Cells["sum"].Value) >= Number_Min)
                            {
                                IRow dataRow = sheet.CreateRow(rowIndex);
                                for (int n = 0; n < grid.Columns.Count; n++)
                                {
                                    if (grid.Columns[n].Visible)
                                    {
                                        dataRow.CreateCell(n).SetCellValue((row.Cells[n].Value ?? "").ToString());
                                    }
                                }
                                rowIndex++;
                            }
                        }
                    }

                }
            }

            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            workbook.Write(fs);
            fs.Dispose();

            sheet = null;
            headerRow = null;
            workbook = null;

            return filePath;
        }
        #endregion
 
        #region 公共导入方法
 
        /// <summary>
        /// 由Excel导入DataTable
        /// </summary>
        /// <param name="excelFileStream">Excel文件流</param>
        /// <param name="sheetName">Excel工作表名称</param>
        /// <param name="headerRowIndex">Excel表头行索引</param>
        /// <param name="isCompatible">是否为兼容模式</param>
        /// <returns>DataTable</returns>
        public static DataTable ImportFromExcel(Stream excelFileStream, string sheetName, int headerRowIndex, bool isCompatible)
        {
            IWorkbook workbook = CreateWorkbook(isCompatible, excelFileStream);
            ISheet sheet = null;
            int sheetIndex = -1;
            if (int.TryParse(sheetName, out sheetIndex))
            {
                sheet = workbook.GetSheetAt(sheetIndex);
            }
            else
            {
                sheet = workbook.GetSheet(sheetName);
            }
 
            DataTable table = GetDataTableFromSheet(sheet, headerRowIndex);
 
            excelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }
 
        /// <summary>
        /// 由Excel导入DataTable
        /// </summary>
        /// <param name="excelFilePath">Excel文件路径，为物理路径。</param>
        /// <param name="sheetName">Excel工作表名称</param>
        /// <param name="headerRowIndex">Excel表头行索引</param>
        /// <returns>DataTable</returns>
        public static DataTable ImportFromExcel(string excelFilePath, string sheetName, int headerRowIndex)
        {
            if (string.IsNullOrEmpty(excelFilePath))
            {
                excelFilePath = GetOpenFilePath();
            }
 
            if (string.IsNullOrEmpty(excelFilePath))
            {
                return null;
            }
 
            using (FileStream stream = System.IO.File.OpenRead(excelFilePath))
            {
                bool isCompatible = GetIsCompatible(excelFilePath);
                return ImportFromExcel(stream, sheetName, headerRowIndex, isCompatible);
            }
        }
 
        /// <summary>
        /// 由Excel导入DataSet，如果有多个工作表，则导入多个DataTable
        /// </summary>
        /// <param name="excelFileStream">Excel文件流</param>
        /// <param name="headerRowIndex">Excel表头行索引</param>
        /// <param name="isCompatible">是否为兼容模式</param>
        /// <returns>DataSet</returns>
        public static DataSet ImportFromExcel(Stream excelFileStream, int headerRowIndex, bool isCompatible)
        {
            DataSet ds = new DataSet();
            IWorkbook workbook = CreateWorkbook(isCompatible, excelFileStream);
            for (int i = 0; i < workbook.NumberOfSheets; i++)
            {
                ISheet sheet = workbook.GetSheetAt(i);
                DataTable table = GetDataTableFromSheet(sheet, headerRowIndex);
                ds.Tables.Add(table);
            }
 
            excelFileStream.Close();
            workbook = null;
 
            return ds;
        }
 
        /// <summary>
        /// 由Excel导入DataSet，如果有多个工作表，则导入多个DataTable
        /// </summary>
        /// <param name="excelFilePath">Excel文件路径，为物理路径。</param>
        /// <param name="headerRowIndex">Excel表头行索引</param>
        /// <returns>DataSet</returns>
        public static DataSet ImportFromExcel(string excelFilePath, int headerRowIndex)
        {
            if (string.IsNullOrEmpty(excelFilePath))
            {
                excelFilePath = GetOpenFilePath();
            }
 
            if (string.IsNullOrEmpty(excelFilePath))
            {
                return null;
            }
 
            using (FileStream stream = System.IO.File.OpenRead(excelFilePath))
            {
                bool isCompatible = GetIsCompatible(excelFilePath);
                return ImportFromExcel(stream, headerRowIndex, isCompatible);
            }
        }
 
        #endregion
 
        #region 公共转换方法
 
        /// <summary>
        /// 将Excel的列索引转换为列名，列索引从0开始，列名从A开始。如第0列为A，第1列为B...
        /// </summary>
        /// <param name="index">列索引</param>
        /// <returns>列名，如第0列为A，第1列为B...</returns>
        public static string ConvertColumnIndexToColumnName(int index)
        {
            index = index + 1;
            int system = 26;
            char[] digArray = new char[100];
            int i = 0;
            while (index > 0)
            {
                int mod = index % system;
                if (mod == 0) mod = system;
                digArray[i++] = (char)(mod - 1 + 'A');
                index = (index - 1) / 26;
            }
            StringBuilder sb = new StringBuilder(i);
            for (int j = i - 1; j >= 0; j--)
            {
                sb.Append(digArray[j]);
            }
            return sb.ToString();
        }
 
 
        /// <summary>
        /// 转化日期
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static DateTime ConvertToDate(object date)
        {
            string dtStr = (date ?? "").ToString();
 
            DateTime dt = new DateTime();
 
            if (DateTime.TryParse(dtStr, out dt))
            {
                return dt;
            }
 
            try
            {
                string spStr = "";
                if (dtStr.Contains("-"))
                {
                    spStr = "-";
                }
                else if (dtStr.Contains("/"))
                {
                    spStr = "/";
                }
                string[] time = dtStr.Split(spStr.ToCharArray());
                int year = Convert.ToInt32(time[2]);
                int month = Convert.ToInt32(time[0]);
                int day = Convert.ToInt32(time[1]);
                string years = Convert.ToString(year);
                string months = Convert.ToString(month);
                string days = Convert.ToString(day);
                if (months.Length == 4)
                {
                    dt = Convert.ToDateTime(date);
                }
                else
                {
                    string rq = "";
                    if (years.Length == 1)
                    {
                        years = "0" + years;
                    }
                    if (months.Length == 1)
                    {
                        months = "0" + months;
                    }
                    if (days.Length == 1)
                    {
                        days = "0" + days;
                    }
                    rq = "20" + years + "-" + months + "-" + days;
                    dt = Convert.ToDateTime(rq);
                }
            }
            catch
            {
                throw new Exception("日期格式不正确，转换日期类型失败！");
            }
            return dt;
        }
 
        /// <summary>
        /// 转化数字
        /// </summary>
        /// <param name="d">数字字符串</param>
        /// <returns></returns>
        public static decimal ConvertToDecimal(object d)
        {
            string dStr = (d ?? "").ToString();
            decimal result = 0;
            if (decimal.TryParse(dStr, out result))
            {
                return result;
            }
            else
            {
                throw new Exception("数字格式不正确，转换数字类型失败！");
            }
 
        }
 
 
        /// <summary>
        /// 转化布尔
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool ConvertToBoolen(object b)
        {
            string bStr = (b ?? "").ToString().Trim();
            bool result = false;
            if (bool.TryParse(bStr, out result))
            {
                return result;
            }
            else if (bStr=="0" || bStr=="1")
            {
                return (bStr == "0");
            }
            else
            {
                throw new Exception("布尔格式不正确，转换布尔类型失败！");
            }
        }
 
        #endregion
    }

}
