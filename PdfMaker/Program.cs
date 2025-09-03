using Microsoft.Office.Interop.Excel;
using System;

static class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: PdfMaker <ExcelFile> <PdfFile>");
            return;
        }
        var excelFilePath = args[0];
        var pdfFilePath = args[1];

        var app = new Application
        {
            Visible = false
        };
        var workbook = app.Workbooks.Open(excelFilePath);
        workbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, pdfFilePath);
        Console.WriteLine($"{excelFilePath} converted to {pdfFilePath}");
    }
}
