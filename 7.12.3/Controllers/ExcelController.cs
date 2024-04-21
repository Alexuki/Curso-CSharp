using Microsoft.AspNetCore.Mvc;

public class ExcelController : Controller
{
    public ExcelResult Generate()
    {
        var list = new[] {
            new { Name="john", Age=32, Email="john@server.com",
                LastUpdated = DateTime.Today
            },
            new { Name="peter", Age=22, Email="peter@server.com",
                LastUpdated = DateTime.Today.AddMonths(-3)
            },
            new { Name="mark", Age=52, Email="mark@server.com",
                LastUpdated = DateTime.Today.AddYears(-1)
            },
            new { Name="arthur", Age=34, Email="art@server.com",
                LastUpdated = DateTime.Today.AddDays(-3)
            }
        };
        return new ExcelResult("test", list);
    }
}
