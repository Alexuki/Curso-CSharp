using Microsoft.AspNetCore.Mvc;

public class FriendsController : Controller
{
    // GET: /Friends/
    public IActionResult Index() => Content("FriendsController.Index()");

    // GET: /Friends/View/John
    public new IActionResult View(string name) => Content($"FriendsController.View('{name}')");

    // GET: /Friends/Edit/23
    public IActionResult Edit(int id) => Content($"FriendsController.Edit({id})");

    // GET: /delete/friends/18
    public IActionResult Delete(int id) => Content($"FriendsController.Delete({id})");

}