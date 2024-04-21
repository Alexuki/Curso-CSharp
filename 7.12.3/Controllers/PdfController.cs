using Microsoft.AspNetCore.Mvc;

public class PdfController : Controller
{
    public IActionResult Download()
    {
        var defaultValues = new PdfFile
        {
            FileName = "default.pdf",
            Text = "Hello World!"
        };

        return View(defaultValues);
    }

    [HttpPost]
    public IActionResult Download(PdfFile form)
    {
        if(!ModelState.IsValid)
        {
            return View(form);
        }

        //return Content($"Generated PDF File '{form.FileName}' with text '{form.Text}'");

        // (6) Retornar nuestro propio IActionResult
        return new PdfFileResult(form.FileName, form.Text);
    }
}