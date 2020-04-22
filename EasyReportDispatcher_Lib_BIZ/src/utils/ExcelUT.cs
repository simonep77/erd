using ClosedXML.Excel;
using ClosedXML.Report;
using EasyReportDispatcher_Lib_BIZ.src.report;
using EasyReportDispatcher_Lib_DAL.src.report;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyReportDispatcher_Lib_BIZ.src.utils
{
    public class ExcelUT
    {

        public class ExcelRender
        {
            public string NomeFile;
            public byte[] DatiMemory;
            public string MimeType = @"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        }

        /// <summary>
        /// Scrive un datatable su Excel
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="nomeStat"></param>
        /// <param name="titolo"></param>
        /// <param name="sheetName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static ExcelRender EseguiRenderDataTableExcel(DataTable dt, string nomeStat, string titolo, string sheetName, Dictionary<string, string> args)
        {
            var oEsitoRender = new ExcelRender();

            oEsitoRender.NomeFile = String.Format("Report_{0}_{1:yyyyMMdd_HHmmss}.xlsx", nomeStat, DateTime.Now);

            var workbook = new ClosedXML.Excel.XLWorkbook();

            //Se fornito imposta un nome sheet
            if (string.IsNullOrEmpty(sheetName))
                sheetName = nomeStat;

            //Crea sheet dati
            var worksheet = workbook.Worksheets.Add(dt, sheetName);



            worksheet.Rows().AdjustToContents();
            worksheet.Columns().AdjustToContents();

            for (int i = 0; i < dt.Columns.Count - 1; i++)
            {
                var colIdx = i + 1;

                if (dt.Columns[i].DataType == typeof(decimal))
                {
                    worksheet.Column(colIdx).Cells().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                    worksheet.Column(colIdx).CellsUsed().Style.NumberFormat.Format = "0.00 €";
                }
                else
                {
                    worksheet.Column(colIdx).Cells().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                }

            }

            //Titolo
            if (!String.IsNullOrEmpty(titolo))
            {
                //Add titolo
                worksheet.FirstRowUsed().InsertRowsAbove(1);

                //Ultima colonna
                var cellLast = worksheet.Cell(1, dt.Columns.Count);

                //Crea range e merge
                var oRange = worksheet.Range("A1:" + cellLast.Address.ColumnLetter + "1").Merge();
                oRange.Value = titolo;
                oRange.Style.Font.Bold = true;
                oRange.Style.Font.FontColor = ClosedXML.Excel.XLColor.CornflowerBlue;
                oRange.Style.Font.FontSize = 16d;
                oRange.Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.Yellow;
                oRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            }

            //Parametri
            if (args != null && args.Count > 0)
            {
                //Crea sheet parametri
                var worksheetp = workbook.Worksheets.Add("Parametri");

                worksheetp.Column(1).Cell(1).Value = "Nome";
                worksheetp.Column(2).Cell(2).Value = "Valore";

                var iRow = 1;
                foreach (var oParam in args)
                {
                    worksheetp.Column(1).Cell(iRow).Value = oParam.Key;
                    worksheetp.Column(2).Cell(iRow).Value = oParam.Value;
                    iRow += 1;
                }
                worksheetp.Rows().AdjustToContents();
                worksheetp.Columns().AdjustToContents();
                worksheetp.Rows(1, 1).Style.Font.Bold = true;
            }

            //Scrive
            var memoryStream = new MemoryStream();
            workbook.SaveAs(memoryStream);
            oEsitoRender.DatiMemory = memoryStream.ToArray();

            return oEsitoRender;
        }




        public static ExcelRender EseguiRenderDataTableExcelTemplate(DataTable dt, string nomeFile, byte[] template, Dictionary<string, string> args)
        {
            var oEsitoRender = new ExcelRender();

            oEsitoRender.NomeFile = nomeFile;

            var ms = new MemoryStream(template);

            var tpl = new ClosedXML.Report.XLTemplate(ms);

            ms.Dispose();

            tpl.AddVariable("Dati", dt);
            tpl.AddVariable("Parametri", args);

            var ret = tpl.Generate();
            if (ret.HasErrors)
                throw new ApplicationException(string.Join(" - ", ret.ParsingErrors.Select(s => s.Message)));
            
            //Scrive
            var memoryStream = new MemoryStream();
            tpl.SaveAs(memoryStream);

            //Imposta blob output
            oEsitoRender.DatiMemory = memoryStream.ToArray();

            return oEsitoRender;
        }


        /// <summary>
        /// Esegue accorpamento di excel multipli in uno (da byte array)
        /// </summary>
        /// <param name="wbks"></param>
        /// <returns></returns>
        public static byte[] EseguiAccorpamento(IList<byte[]> wbks)
        {
            using (var ms = new MemoryStream())
            {
                var workbook = new XLWorkbook(ms);

                foreach (var item in wbks)
                {
                    using (var mst = new MemoryStream(item))
                    {
                        var wb = new XLWorkbook(mst);

                        foreach (var ws in wb.Worksheets)
                        {
                            ws.CopyTo(workbook, ws.Name);
                        }
                    }
                }

                using (var mso = new MemoryStream())
                {
                    workbook.SaveAs(mso);
                    //Reimposta il blob dell'output
                    return mso.ToArray();
                }
            }

        }

    }
}
