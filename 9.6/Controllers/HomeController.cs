using Microsoft.AspNetCore.Mvc;

public class HomeController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILoggerFactory<HomeController> logger)
    {
        _logger = logger;
    }

    [Route("/")]
    public IActionResult Index()
    {
        return Content("API Ready");
    }
}