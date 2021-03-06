﻿using Aliyun.OSS;
using OfficeOpenXml;
using System;
using System.Data;
using System.IO;

namespace ACBC.Common
{
    public class FileManager
    {
        private string path = Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), "upload");
        private string filePath = Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), "file");
        /// <summary>
        /// 将Base64位码保存成图片
        /// </summary>
        /// <param name="base64Img">Base64位码</param>
        /// <param name="fileName">图片名</param>
        /// <returns></returns>
        public bool saveFileByBase64String(string base64, string fileName)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                base64 = base64.Split("base64,")[1];
                byte[] bt = Convert.FromBase64String(base64);//获取图片base64
                string ImageFilePath = Path.Combine(path, fileName);
                File.WriteAllBytes(ImageFilePath, bt); //保存图片到服务器，然后获取路径 
                return true;
            }
            catch (Exception ex)
            {
                string s = ex.ToString();
                return false;
            }
        }
        public bool saveImgByByte(byte[] bt, string fileName)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string ImageFilePath = Path.Combine(path, fileName);
                File.WriteAllBytes(ImageFilePath, bt); //保存图片到服务器，然后获取路径 
                return true;
            }
            catch (Exception ex)
            {
                string s = ex.ToString();
                return false;
            }
        }
        public bool fileCopy(string oldFile, string newFile)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                File.Move(Path.Combine(path, oldFile), Path.Combine(path, newFile));
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public DataTable readExcelFileToDataTable(string fileName)
        {
            FileInfo file = new FileInfo(Path.Combine(path, fileName));
            try
            {
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    DataTable dt = new DataTable();
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                    int rowCount = worksheet.Dimension.Rows;
                    int ColCount = worksheet.Dimension.Columns;
                    for (int row = 1; row <= rowCount; row++)
                    {
                        if (row == 1)
                        {
                            for (int col = 1; col <= ColCount; col++)
                            {
                                if (worksheet.Cells[row, col].Value == null || worksheet.Cells[row, col].Value.ToString() == "")
                                {
                                    ColCount = col - 1;
                                    break;
                                }
                                else
                                {
                                    dt.Columns.Add(worksheet.Cells[row, col].Value.ToString());
                                }

                            }
                        }
                        else
                        {
                            if (worksheet.Cells[row, 1].Value == null || worksheet.Cells[row, 1].Value.ToString() == "")
                            {
                                continue;
                            }
                            DataRow dr = dt.NewRow();
                            for (int col = 1; col <= ColCount; col++)
                            {
                                if (worksheet.Cells[row, col].Value == null)
                                {
                                    dr[col - 1] = "";
                                }
                                else
                                {
                                    dr[col - 1] = worksheet.Cells[row, col].Value.ToString();
                                }
                            }
                            dt.Rows.Add(dr);
                        }
                    }
                    return dt;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public DataSet readExcelToDataSet(string fileName, out string msg)
        {
            FileInfo file = new FileInfo(Path.Combine(path, fileName));
            try
            {
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    DataSet ds = new DataSet();
                    int count = package.Workbook.Worksheets.Count;
                    for (int j = 1; j <= count; j++)
                    {
                        DataTable dt = new DataTable();
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[j];
                        dt.TableName = worksheet.Name;
                        int rowCount = worksheet.Dimension.Rows;
                        int ColCount = worksheet.Dimension.Columns;
                        for (int row = 1; row <= rowCount; row++)
                        {
                            if (row == 1)
                            {
                                for (int col = 1; col <= ColCount; col++)
                                {
                                    dt.Columns.Add(worksheet.Cells[row, col].Value.ToString());
                                }
                            }
                            else
                            {
                                if (worksheet.Cells[row, 1].Value == null || worksheet.Cells[row, 1].Value.ToString() == "")
                                {
                                    continue;
                                }
                                DataRow dr = dt.NewRow();
                                for (int col = 1; col <= ColCount; col++)
                                {
                                    if (worksheet.Cells[row, col].Value != null)
                                    {
                                        dr[col - 1] = worksheet.Cells[row, col].Value.ToString();
                                    }
                                }
                                dt.Rows.Add(dr);
                            }
                        }
                        ds.Tables.Add(dt);
                    }
                    msg = "";
                    return ds;
                }
            }
            catch (Exception ex)
            {
                msg = ex.ToString();
                return null;
            }
        }
        public DataSet readGoodsTempletToDataSet()
        {
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();
            dt.Columns.Add("商品条码");
            dt.Columns.Add("品牌名称(中文)");
            dt.Columns.Add("品牌名称(外文)");
            dt.Columns.Add("商品名称(中文)");
            dt.Columns.Add("商品名称(外文)");
            dt.Columns.Add("一级分类");
            dt.Columns.Add("二级分类");
            dt.Columns.Add("三级分类");
            dt.Columns.Add("原产国/地");
            dt.Columns.Add("货源国/地");
            dt.Columns.Add("型号");
            dt.Columns.Add("颜色");
            dt.Columns.Add("口味");
            dt.Columns.Add("毛重（kg)");
            dt.Columns.Add("净重(kg)");
            dt.Columns.Add("计量单位");
            dt.Columns.Add("商品规格CM:长*宽*高");
            dt.Columns.Add("包装规格CM:长*宽*高");
            dt.Columns.Add("适用人群");
            dt.Columns.Add("使（食）用方法");
            dt.Columns.Add("用途/功效");
            dt.Columns.Add("卖点");
            dt.Columns.Add("配料成分含量");
            dt.Columns.Add("保质期（天）");
            dt.Columns.Add("贮存方式");
            dt.Columns.Add("注意事项");
            dt.Columns.Add("指导零售价(RMB)");

            ds.Tables.Add(dt);

            return ds;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool writeDataSetToExcel(DataSet ds, string fileName)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            FileInfo file = new FileInfo(Path.Combine(path, fileName));
            try
            {
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    for (int i = 0; i < ds.Tables.Count; i++)
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(ds.Tables[i].TableName);
                        for (int j = 0; j <= ds.Tables[i].Rows.Count; j++)
                        {
                            for (int k = 0; k < ds.Tables[i].Columns.Count; k++)
                            {
                                if (j == 0)
                                {
                                    worksheet.Cells[j + 1, k + 1].Value = ds.Tables[i].Columns[k].ColumnName;
                                }
                                else
                                {
                                    worksheet.Cells[j + 1, k + 1].Value = ds.Tables[i].Rows[j - 1][k].ToString();
                                    //worksheet.Cells[FromRow, FromColumn, ToRow, ToColumn].Merge = true; //合并单元格
                                }
                            }
                        }
                    }
                    package.Save();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool writeDataTableToExcel(DataTable dt, string fileName)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            FileInfo file = new FileInfo(Path.Combine(path, fileName));
            try
            {
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("sheet1");
                    for (int j = 0; j <= dt.Rows.Count; j++)
                    {
                        for (int k = 0; k < dt.Columns.Count; k++)
                        {
                            if (j == 0)
                            {
                                worksheet.Cells[j + 1, k + 1].Value = dt.Columns[k].ColumnName;
                            }
                            else
                            {
                                worksheet.Cells[j + 1, k + 1].Value = dt.Rows[j - 1][k].ToString();
                            }
                        }
                    }
                    package.Save();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string writeDataTableToExcel1(DataTable dt, string fileName)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            FileInfo file = new FileInfo(Path.Combine(path, fileName));
            try
            {
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("sheet1");
                    for (int j = 0; j <= dt.Rows.Count; j++)
                    {
                        for (int k = 0; k < dt.Columns.Count; k++)
                        {
                            if (j == 0)
                            {
                                worksheet.Cells[j + 1, k + 1].Value = dt.Columns[k].ColumnName;
                            }
                            else
                            {
                                worksheet.Cells[j + 1, k + 1].Value = dt.Rows[j - 1][k].ToString();
                            }
                        }
                    }
                    package.Save();
                    return "true";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        public bool writeSelectOrderToExcel(DataTable dt, string fileName)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            FileInfo file = new FileInfo(Path.Combine(path, fileName));
            try
            {
                using (ExcelPackage package = new ExcelPackage(file))
                {

                    string[] sts = { "", "" };
                    int num = 1;
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(dt.TableName);
                    for (int j = 0; j <= dt.Rows.Count; j++)
                    {
                        for (int k = 0; k < dt.Columns.Count; k++)
                        {
                            if (j == 0)
                            {
                                worksheet.Cells[j + 1, k + 1].Value = dt.Columns[k].ColumnName;
                            }
                            else
                            {
                                worksheet.Cells[j + 1, k + 1].Value = dt.Rows[j - 1][k].ToString();
                                if (k == dt.Columns.Count - 1)
                                {
                                    if (sts[0] == "")
                                    {
                                        sts[0] = dt.Rows[j - 1]["子订单号"].ToString();
                                        sts[1] = (j + 1).ToString();
                                        worksheet.Cells[2, 1].Value = num.ToString();
                                    }
                                    else
                                    {
                                        if (dt.Rows[j - 1]["子订单号"].ToString() == sts[0])
                                        {
                                            for (int x = 1; x <= dt.Rows.Count; x++)
                                            {
                                                if (x <= 5)
                                                {
                                                    worksheet.Cells[Convert.ToInt16(sts[1]), x, j + 1, x].Merge = true; //合并单元格
                                                }
                                                else if (x > 15)
                                                {
                                                    worksheet.Cells[Convert.ToInt16(sts[1]), x, j + 1, x].Merge = true; //合并单元格
                                                }
                                            }
                                        }
                                        else
                                        {
                                            sts[0] = dt.Rows[j - 1]["子订单号"].ToString();
                                            sts[1] = (j + 1).ToString();
                                            num++;
                                            worksheet.Cells[j + 1, 1].Value = num.ToString();
                                        }
                                    }

                                }
                            }
                        }
                    }
                    package.Save();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


       
    }
}
