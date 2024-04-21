using System.ComponentModel.DataAnnotations;

public class Post
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Author { get; set; }
    [Required]
    public string Body { get; set; }
}