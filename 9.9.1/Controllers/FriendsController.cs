using Microsoft.AspNetCore.Mvc;

[ApiController] //(6)
public class FriendsController : ControllerBase
{
    //[HttpPost]
    [HttpPost("/friends/create")] //(6)
    //public ActionResult<Friend> Create([FromBody]Friend f)
    public ActionResult<Friend> Create(Friend f) //(10)
    {
        //(9) Eliminar la validación del objeto
        /* if(!ModelState.IsValid)
        {
            return BadRequest();
        } */

        if (f.Name == "john")
            return Conflict(); // Error HTTP 409
        return f;
    }
}