namespace Lab01.Models.Entities;

public class Post
{
    public Post()
    {
        Comments = new List<Comment>();
    }
    public int Id { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public string Text { get; set; }
    public DateTime Date { get; set; }
    public string Author { get; set; }
    public List<Comment> Comments { get; set; }
}