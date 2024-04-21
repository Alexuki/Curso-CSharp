using System.Runtime.CompilerServices;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;

public class PdfFileResult : IActionResult
{
    private readonly string _fileName;
    private readonly string _text;

    public PdfFileResult(string fileName, string text)
    {
        _fileName = fileName;
        _text = text;
    }
    public async Task ExecuteResultAsync(ActionContext context)
    {
        using var stream = new MemoryStream();
        GeneratePdf(stream, _text);
        context.HttpContext.Response.ContentType = "application/pdf";
        context.HttpContext.Response.Headers.Add("content-disposition", $"attachment; filename={_fileName}");
        await context.HttpContext.Response.BodyWriter.WriteAsync(stream.GetBuffer());
    }

    private void GeneratePdf(Stream stream, string text)
    {
        var document = new Document();
        PdfWriter.GetInstance(document, stream);
        document.Open();
        var font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 24, BaseColor.Gray); //Generar fuente que vamos a usar
        document.Add(new Paragraph(text, font)); // Añadir nuevo párrafo al documento, con el texto y nuestra fuente
        document.Add(new Paragraph(DateTime.Now.ToString("M"))); // Nuevo párrafo con la fecha
        document.Close(); //Finalizar edición
    }
}
