using Microsoft.AspNetCore.Mvc;

public class CustomersController : Controller
{
    public IActionResult Create()
    {
        return View(new CreateCustomerViewModel()); //Devuelve un formulario
    }

    [HttpPost]
    public IActionResult Create(CreateCustomerViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }
        return Content("product created");
    }
}