using System;
using System.Collections.Generic;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using AutoMapper;
using RepositoryObject.UnitOfWorkModel.Interface;
using BaseObject.CommonObject;
using NPOI.HSSF.UserModel;

namespace RepositoryObject.ApplicationService
{
    public class ReportServiceBase : ApplicationServiceBase
    {
        #region Constant

        protected string HSFFWORKBOOK_EXCEL_FILENAME_EXTENSION = ".xls";
        protected string XSFFWORKBOOK_EXCEL_FILENAME_EXTENSION = ".xlsx";

        #endregion Constant

        #region Variable

        protected ICell m_Cell;
        protected List<ExcelCellStyle> m_ecsList = new List<ExcelCellStyle>();
        protected HSSFWorkbook m_hssfWorkbook;
        protected string m_strExcelFilePath;
        protected XSSFWorkbook m_xssfWorkbook;

        #endregion Variable

        #region Property

        public string ExcelFilePath
        {
            get
            {
                return this.m_strExcelFilePath;
            }
        }

        #endregion Property

        #region Constructor

        public ReportServiceBase(ref IUnitOfWork iuow, Profile pof) : base(ref iuow, pof)
        {
        }

        public ReportServiceBase(string strConnectionString, Profile pof) : base(strConnectionString, pof)
        {
        }

        public ReportServiceBase(Profile pof) : base(pof)
        {
        }

        private ReportServiceBase() : base(new ReportServiceBaseProfile())
        {
        }

        #endregion Constructor

        #region Function

        /// <summary>
        /// 自動調整Excel欄寬
        /// </summary>
        /// <param name="sheet">Excel工作表</param>
        /// <param name="intLastColumnIndex">最後欄位索引</param>
        protected virtual void AutoSizeExcelColumn(ref ISheet sheet, int intLastColumnIndex)
        {
            this.AutoSizeExcelColumn(ref sheet, 0, intLastColumnIndex);
        }

        /// <summary>
        /// 自動調整Excel欄寬
        /// </summary>
        /// <param name="sheet">Excel工作表</param>
        /// <param name="intStartColumnIndex">起始欄位索引</param>
        /// <param name="intEndColumnIndex">結束欄位索引</param>
        protected virtual void AutoSizeExcelColumn(ref ISheet sheet, int intStartColumnIndex, int intEndColumnIndex)
        {
            for (int i = intStartColumnIndex; i <= intEndColumnIndex; i++)
            {
                sheet.AutoSizeColumn(i);
            }
        }

        /// <summary>
        /// 建立Excel報表的表單開頭
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="strReportTitle"></param>
        /// <param name="intLastColumnIndex"></param>
        protected virtual void CreateExcelReportTitle(ref ISheet sheet, ref int intRowNum, string strReportTitle, int intLastColumnIndex)
        {
            sheet.CreateRow(intRowNum);
            this.m_Cell = sheet.GetRow(intRowNum).CreateCell(0);
            this.m_Cell.SetCellValue(strReportTitle);
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, intLastColumnIndex));
            intRowNum++;
        }

        /// <summary>
        /// 將Excel存檔至存檔路徑底下
        /// </summary>
        /// <param name="strOutputFolder">存檔路徑</param>
        /// <param name="strFileName">檔案名稱(不含副檔名)</param>
        /// <returns>儲存成功與否</returns>
        protected virtual string SaveExcel(string strOutputFolder, string strFileName)
        {
            try
            {
                if (Directory.Exists(strOutputFolder) == false)
                {
                    Directory.CreateDirectory(strOutputFolder);
                }

                if (this.m_xssfWorkbook != null)
                {
                    this.m_strExcelFilePath = strOutputFolder + "\\" + strFileName + XSFFWORKBOOK_EXCEL_FILENAME_EXTENSION;

                    var _file = new FileStream(this.m_strExcelFilePath, FileMode.Create);
                    this.m_xssfWorkbook.Write(_file);
                    _file.Close();
                }
                else
                {
                    this.m_strExcelFilePath = strOutputFolder + "\\" + strFileName + HSFFWORKBOOK_EXCEL_FILENAME_EXTENSION;

                    var file = new FileStream(this.m_strExcelFilePath, FileMode.Create);
                    this.m_hssfWorkbook.Write(file);
                    file.Close();
                }

                this.m_ecsList.Clear();

                return this.m_strExcelFilePath;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void SetCellType(ref ICell cell, ExcelCellNumericTypeEnum enumExcelNumericType, ExcelCellAreaTypeEnum enumExcelCellAreaType)
        {
            ExcelCellStyle _ecsTarget = this.m_ecsList.Find(p => p.ExcelCellNumericType == enumExcelNumericType && p.ExcelCellAreaType == enumExcelCellAreaType);

            if (_ecsTarget == null)
            {
                ICellStyle _cellstyle = this.m_xssfWorkbook.CreateCellStyle();

                switch (enumExcelNumericType)
                {
                    case ExcelCellNumericTypeEnum.NonNumeric:
                        break;

                    case ExcelCellNumericTypeEnum.Integer:
                        _cellstyle.DataFormat = this.m_xssfWorkbook.CreateDataFormat().GetFormat("#,##0");
                        break;

                    case ExcelCellNumericTypeEnum.Percentage:
                        _cellstyle.DataFormat = this.m_xssfWorkbook.CreateDataFormat().GetFormat("0%");
                        break;

                    case ExcelCellNumericTypeEnum.PercentageOnSecondDecimalPoint:
                        _cellstyle.DataFormat = this.m_xssfWorkbook.CreateDataFormat().GetFormat("0.00%");
                        break;
                }

                IFont _font = this.m_xssfWorkbook.CreateFont();

                switch (enumExcelCellAreaType)
                {
                    case ExcelCellAreaTypeEnum.Normal:
                        break;

                    case ExcelCellAreaTypeEnum.Title:
                        _cellstyle.Alignment = HorizontalAlignment.Center;
                        _font.Boldweight = (short)FontBoldWeight.Bold;
                        _cellstyle.SetFont(_font);
                        break;

                    case ExcelCellAreaTypeEnum.SubSummary:
                        _font.Boldweight = (short)FontBoldWeight.Bold;
                        _cellstyle.SetFont(_font);
                        _cellstyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Lime.Index;
                        _cellstyle.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground;
                        break;

                    case ExcelCellAreaTypeEnum.Summary:
                        _font.Boldweight = (short)FontBoldWeight.Bold;
                        _cellstyle.SetFont(_font);
                        _cellstyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Yellow.Index;
                        _cellstyle.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground;
                        break;
                }

                _ecsTarget = new ExcelCellStyle(enumExcelNumericType, enumExcelCellAreaType, _cellstyle);
                this.m_ecsList.Add(_ecsTarget);
            }

            cell.CellStyle = _ecsTarget.Style;
        }

        #endregion Function

        #region Class

        protected class ExcelCellStyle
        {
            public ExcelCellStyle(ExcelCellNumericTypeEnum enumExcelCellNumericType, ExcelCellAreaTypeEnum enumExcelCellAreaType, ICellStyle icellStyle)
            {
                this.ExcelCellNumericType = enumExcelCellNumericType;
                this.ExcelCellAreaType = enumExcelCellAreaType;
                this.Style = icellStyle;
            }

            public ExcelCellAreaTypeEnum ExcelCellAreaType { get; set; }
            public ExcelCellNumericTypeEnum ExcelCellNumericType { get; set; }
            public ICellStyle Style { get; set; }
        }

        protected class ReportServiceBaseProfile : Profile
        {
            public ReportServiceBaseProfile()
            {
            }
        }

        #endregion Class
    }
}