using Lab01.Api.Model.Entities;
using Lab01.Api.Model.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Lab01.Api.Controllers;

[EnableCors("AllowAll")]
[Route("api/contacts")]
public class ContactsController : ControllerBase
{
    private readonly IContactRepository _repository;

    public ContactsController(IContactRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        var contacts = _repository.GetAll();
        if (!contacts.Any())
            return NoContent();
        return Ok(_repository.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var contact = _repository.Get(id);
        if (contact == null)
            return NotFound();
        return Ok(contact);
    }

    [HttpPost("")]
    public IActionResult Post([FromBody]Contact contact)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState.Values.First(v=>v.ValidationState == ModelValidationState.Invalid).Errors.First().ErrorMessage);
        _repository.Add(contact);
        return CreatedAtAction("Get", new { contact.Id}, contact);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if(!_repository.Remove(id))
            return NotFound();
        else
            return Ok();
    }
}