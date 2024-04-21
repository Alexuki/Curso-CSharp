using Microsoft.AspNetCore.Mvc;

[Route("api/blog/[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostsRepository _postsRepository;

    public PostsController(IPostsRepository postsRepository)
    {
        _postsRepository = postsRepository;
    }

    // No se añade nada aparte del verbo porque esta es la ruta por defecto siempre que se acceda por la ruta marcada en el controlador: api/blog/Posts
    [HttpGet]
    public IActionResult All()
    {
        var posts = _postsRepository.All();
        return Ok(posts);
    }

    // En esta ruta se espera un parámetro id: api/blog/Posts/id
    [HttpGet("{id}")]
    public IActionResult GetPost(int id)
    {
        var post = _postsRepository.GetById(id);
        if (post == null)
            return NotFound();
        return Ok(post);
    }

    // Se crea el elemento pasando el Post en el payload de la petición
    // La ruta es la del controlador pero usando el verbo POST
    [HttpPost]
    public IActionResult Create([FromBody] Post post)
    {
        // Comprobación de las validaciones marcadas en el modelo ([Required] en el ejemplo => si alguna viene en blanco, el modelo no es válido)
        if (!ModelState.IsValid)
            return BadRequest("Invalid post");
        
        // Intenta crear el Post
        if (!_postsRepository.Create(post))
            // Si ya existe, devuelve un Conflict
            return Conflict();
        
        // Si se crea, se devuelve un CratedAction que devuelve una URL hacia el post
        return CreatedAtAction("GetPost", new { id = post.Id }, post);
    }
}