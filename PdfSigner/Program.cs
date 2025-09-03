using System.Drawing;
using Syncfusion.Licensing;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Parsing;
using System;
using System.IO;
using System.Linq;

static class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: PdfSigner <PdfFile> <SignatureFile> <SignatureLeft>,<SignatureTop>,<SignatureWidth,<SignatureHeight>");
            return;
        }
        var pdfFilePath = args[0];
        var signatureFilePath = args[1];
        var signatureRect = new RectangleF();
        try
        {
            var values = args[2].Split(',').Select(value => float.Parse(value)).ToArray();
            signatureRect = new RectangleF(values[0], values[1], values[2], values[3]);
        }
        catch
        {
            Console.WriteLine("Wrong signature rectangle");
            return;
        }

        SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1JEaF5cXmRCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdmWXdecnVXQ2lcVUJwX0JWYEk=");

        using (var doc = new PdfLoadedDocument(File.ReadAllBytes(pdfFilePath)))
        {
            var page = doc.Pages[0];
            using (var signatureFileStream = File.OpenRead(signatureFilePath))
            {
                var signatureImage = PdfImage.FromStream(signatureFileStream);
                page.Graphics.DrawImage(signatureImage, signatureRect);
            }
            using (var reportFileStream = File.Create(pdfFilePath))
            {
                doc.Save(reportFileStream);
            }
        }
        Console.WriteLine($"{signatureFilePath} put on {pdfFilePath}");
    }
}
