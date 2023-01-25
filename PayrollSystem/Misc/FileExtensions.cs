using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spire.Xls;
using Spire.Pdf;
using Spire.Pdf.Security;

namespace PayrollSystem.Misc
{


    public static class FileExtensions
    {
        public static void GeneratePdf(this string filePath,string pdfPassword)
        {
            var pdfFile = filePath.Replace("xlsx", "pdf");
            Workbook workbook = new Workbook();
            workbook.LoadFromFile(filePath);
            Worksheet sheet = workbook.Worksheets[0];
            System.IO.File.Delete(pdfFile);
            sheet.PageSetup.Orientation = PageOrientationType.Landscape;
            sheet.PageSetup.PaperSize = PaperSizeType.PaperA4;
            sheet.SaveToPdf(pdfFile);

            System.IO.File.Delete(filePath);

            using (PdfDocument pdf = new PdfDocument(pdfFile))
            {

                pdf.Security.Encrypt(pdfPassword, "", PdfPermissionsFlags.Print | PdfPermissionsFlags.CopyContent, PdfEncryptionKeySize.Key128Bit);
                pdf.SaveToFile(pdfFile, Spire.Pdf.FileFormat.PDF);
            }

            System.Diagnostics.Process.Start(pdfFile);
        }

        public static void SecureExcelFile(this string filePath, string pdfPassword)
        {
            
            Workbook workbook = new Workbook();
            workbook.LoadFromFile(filePath);
            Worksheet sheet = workbook.Worksheets[0];
            workbook.Protect(pdfPassword);
            workbook.SaveToFile(filePath, ExcelVersion.Version2013);
            System.Diagnostics.Process.Start(filePath);
           

        }
    }
}
