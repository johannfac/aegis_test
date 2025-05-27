using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AegisTest.Models;
using ClosedXML.Excel;
using PdfSharpCore.Pdf;
using PdfSharpCore.Drawing;

namespace AegisTest.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        ViewData["data"] = new List<string> { "John", "Doe", "Hill", "Dean", "Tyson" };
        return View();
    }

    public IActionResult Excel()
    {
        var data = new List<string> { "John", "Doe", "Hill", "Dean", "Tyson" };

        string timestamp = DateTime.Now.ToString("yyyyMMDDHHmmss");
        const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        string fileDownloadName = $"workbook_{timestamp}.xlsx";

        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("sheet 1");

            for (int i = 0; i < data.Count; i++)
            {
                worksheet.Cells($"A{i + 1}").Value = data[i];
            }

            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                byte[] bytes = stream.ToArray();

                return new FileContentResult(bytes, contentType)
                {
                    FileDownloadName = fileDownloadName
                };
            }
        }
    }

    public IActionResult Pdf()
    {
        var data = new List<string> { "John", "Doe", "Hill", "Dean", "Tyson" };

        string timestamp = DateTime.Now.ToString("yyyyMMDDHHmmss");
        string fileDownloadName = $"document_{timestamp}.pdf";

        using (var stream = new MemoryStream())
        {
            var document = new PdfDocument();
            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);
            var font = new XFont("Verdana", 20, XFontStyle.Bold);
            
            double topMargin = 40;
            double lineHeight = 24;

            for (int i = 0; i < data.Count; i++)
            {
                double y = topMargin + i * lineHeight;
                gfx.DrawString(
                    data[i],
                    font,
                    XBrushes.Black,
                    new XRect(40, y, page.Width, page.Height),
                    XStringFormats.TopLeft
                );
            }

            document.Save(stream, false);
            byte[] bytes = stream.ToArray();

            return new FileContentResult(bytes, "application/pdf")
            {
                FileDownloadName = fileDownloadName
            };
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
