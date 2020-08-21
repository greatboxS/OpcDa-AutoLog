using OPCDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using Microsoft.Office.Interop.Excel;

namespace DataLogger
{
    public class ExcelHandle
    {
        // return false if success ortherwise fail
        public bool SaveAs(IList<LoggingGroup> LoggingGroup, string filePath)
        {
            try
            {
                return CreateConfigFile(LoggingGroup, filePath);
            }
            catch (Exception ex)
            {
                DebugLog.WriteExceptionLogFile(ex.ToString());
                return true;
            }
        }

        // return false if success ortherwise fail
        public bool Save(IList<LoggingGroup> LoggingGroup, string filePath)
        {
            try
            {
                UpdateConfigFile(LoggingGroup, filePath);
            }
            catch (Exception ex)
            {
                DebugLog.WriteExceptionLogFile(ex.ToString());
                return true;
            }
            return false;
        }

        // return false if success ortherwise fail
        public bool UpdateConfigFile(IList<LoggingGroup> LoggingGroup, string filePath)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(filePath);

                using (var excel = new ExcelPackage(fileInfo))
                {
                    var workBook = excel.Workbook;

                    foreach (var item in LoggingGroup)
                    {
                        string sheetName = $"Group_{workBook.Worksheets.Count + 1}";
                        var workSheet = workBook.Worksheets.Add(sheetName);
                        workSheet.Name = sheetName;
                        WriteConfigGroup(workSheet, item);
                    }
                    excel.Save();
                }
            }
            catch (Exception ex)
            {
                DebugLog.WriteExceptionLogFile(ex.ToString());
                return true;
            }

            return false;
        }

        // return false if success ortherwise fail
        public bool CreateConfigFile(IList<LoggingGroup> LoggingGroup, string filePath)
        {
            string FilePath = filePath;
            try
            {
                FileInfo fileInfo = new FileInfo(filePath);

                using (var Excel = new ExcelPackage(fileInfo))
                {
                    var workBook = Excel.Workbook;


                    foreach (var item in LoggingGroup)
                    {
                        var sheet = Excel.Workbook.Worksheets.Add(item.GroupName);
                        if (WriteConfigGroup(sheet, item)) return true;
                    }

                    Excel.SaveAs(fileInfo);
                }
            }
            catch (Exception ex)
            {
                DebugLog.WriteExceptionLogFile(ex.ToString());
                return true;
            }
            return false;
        }

        public IList<LoggingGroup> ReadConfigFile(string filePath)
        {
            IList<LoggingGroup> groups = new List<LoggingGroup>();

            string exp = string.Empty;
            try
            {
                FileInfo fileInfo = new FileInfo(filePath);

                using (var Excel = new ExcelPackage(fileInfo))
                {
                    var workBook = Excel.Workbook;



                    for (int i = 0; i < workBook.Worksheets.Count; i++)
                    {
                        var sheet = workBook.Worksheets[i];
                        groups.Add(ReadConfigGroup(sheet));
                    }
                }
            }
            catch (Exception ex)
            {
                DebugLog.WriteExceptionLogFile(ex.ToString());
            }
            return groups;
        }

        public LoggingGroup ReadConfigGroup(ExcelWorksheet sheet)
        {
            LoggingGroup group = new LoggingGroup();
            group.GroupName = sheet.Name;
            group.SqlSetting.ServerName = sheet.Cells["B2"].Text;
            group.SqlSetting.DataBase = sheet.Cells["B3"].Text;
            group.SqlSetting.Table = sheet.Cells["B4"].Text;
            group.OPCServerName = sheet.Cells["B5"].Text;
            int time = 100;
            int.TryParse(sheet.Cells["B6"].Text, out time);
            group.IntervalUpdateTime = time;
            int total = 0;
            int.TryParse(sheet.Cells["E2"].Text, out total);
            group.TotalTag = total;
            bool login = (sheet.Cells["B7"].Text.ToLower() == "false") ? false : true;
            group.SqlSetting.UseLogin = login;
            group.SqlSetting.UserName = sheet.Cells["B8"].Text;
            group.SqlSetting.Password = sheet.Cells["B9"].Text;

            string status = sheet.Cells["B10"].Text;

            if (status == "SETTING") group.State = OPCDataAccess.AppDefinition.GroupState.SETTING;
            else if (status == "SUCCESS") group.State = OPCDataAccess.AppDefinition.GroupState.SUCCESS;
            else group.State = OPCDataAccess.AppDefinition.GroupState.ERROR;

            for (int i = 0; i < group.TotalTag; i++)
            {
                TagProperty tag = new TagProperty();

                tag.Name = sheet.Cells[$"D{i + 5}"].Text;
                tag.TypeName = sheet.Cells[$"E{i + 5}"].Text;
                group.GroupTags.Add(tag);
            }

            return group;
        }

        // return false if success ortherwise fail
        private bool WriteConfigGroup(ExcelWorksheet sheet, LoggingGroup group)
        {
            try
            {
                sheet.Column(1).Width = 20;
                sheet.Column(2).Width = 40;
                sheet.Column(3).Width = 10;
                sheet.Column(4).Width = 45;
                sheet.Column(5).Width = 15;

                sheet.Column(2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                sheet.Column(3).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                sheet.Column(4).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                sheet.Column(5).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                sheet.Cells["A2:B10"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dashed);
                var range = sheet.Cells["D4:E4"];

                range.Merge = true;
                range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.SeaGreen);

                sheet.Name = group.GroupName;
                sheet.Cells["A2"].Value = "Server Name";
                sheet.Cells["B2"].Value = group.SqlSetting.ServerName;

                sheet.Cells["A3"].Value = "Database";
                sheet.Cells["B3"].Value = group.SqlSetting.DataBase;

                sheet.Cells["A4"].Value = "Table";
                sheet.Cells["B4"].Value = group.SqlSetting.Table;

                sheet.Cells["A5"].Value = "OpcDa Server";
                sheet.Cells["B5"].Value = group.OPCServerName;

                sheet.Cells["A6"].Value = "Update Interval";
                sheet.Cells["B6"].Value = group.IntervalUpdateTime;

                sheet.Cells["A7"].Value = "Use login";
                sheet.Cells["B7"].Value = group.SqlSetting.UseLogin;

                sheet.Cells["A8"].Value = "User name";
                sheet.Cells["B8"].Value = group.SqlSetting.UserName;

                sheet.Cells["A9"].Value = "Password";
                sheet.Cells["B9"].Value = group.SqlSetting.Password;

                sheet.Cells["D2"].Value = "Total tag";
                sheet.Cells["E2"].Value = group.GroupTags.Count.ToString();

                sheet.Cells["D3"].Value = "Tag name";
                sheet.Cells["E3"].Value = "Data Type";

                sheet.Cells["A10"].Value = "Status";
                sheet.Cells["B10"].Value = group.State.ToString();
                int i = 4;

                foreach (var item in group.GroupTags)
                {
                    i++;
                    sheet.Cells[$"D{i}"].Value = item.Name;
                    sheet.Cells[$"E{i}"].Value = item.TypeName;
                }

                sheet.Cells[$"D2:E{i}"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dashed);
            }
            catch (Exception ex)
            {
                DebugLog.WriteExceptionLogFile(ex.ToString());
                return true;
            }
            return false;
        }
    }
}
