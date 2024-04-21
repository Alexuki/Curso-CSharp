using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;

public class ExcelResult : IActionResult
{
    private readonly IEnumerable<object> _data;
    private readonly string _name;

    public ExcelResult(string name, IEnumerable<object> data)
    {
        _name = name;
        _data = data;
    }

    public async Task ExecuteResultAsync(ActionContext context)
    {
        if (_data == null || !_data.Any())
        {
            return;
        }

        var response = context.HttpContext.Response;
        response.ContentType =
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        response.Headers.Add("content-disposition",
            $"attachment;filename={_name}.xlsx");

        await using var memoryStream = CreateExcelFile(_name, _data);
        memoryStream.Seek(0, SeekOrigin.Begin);
        await  memoryStream.CopyToAsync(response.Body);
    }

    private MemoryStream CreateExcelFile(string name, IEnumerable<object> data)
    {
        var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add(name);
        var row = 1;
        var col = 1;

        var type = data.First().GetType();
        var props = type.GetProperties();

        foreach (var prop in props)
        {
            var cell = worksheet.Cell(row, col++);
            cell.Value = prop.Name;
            cell.Style.Font.Bold = true;
            cell.Style.Font.FontColor = XLColor.Blue;
        }

        foreach (var elem in data)
        {
            row++;
            col = 1;
            foreach (var prop in props)
            {
                var cell = worksheet.Cell(row, col++);
                cell.Value = XLCellValue.FromObject(prop.GetValue(elem, null));
            }
        }
        worksheet.Columns().AdjustToContents();

        MemoryStream memoryStream = new MemoryStream();
        workbook.SaveAs(memoryStream);
        return memoryStream;
    }
}
