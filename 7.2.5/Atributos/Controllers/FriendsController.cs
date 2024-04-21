using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]

public class FriendsController : Controller
{
    // GET: /Friends/
    public IActionResult Index() => Content("FriendsController.Index()");

    // GET: /Friends/View/John
    [Route("view/{name:startsWith(jo)}")]
    public IActionResult View(string name) => Content($"FriendsController.View('{name}')");

    // GET: /Friends/Edit/23
    [Route("edit/{id}")]
    public IActionResult Edit(int id) => Content($"FriendsController.Edit({id})");

    // GET: /delete/friends/18
    [Route("/delete/friends/{id:range(1,9999)}")]
    public IActionResult Delete(int id) => Content($"FriendsController.Delete({id})");

}