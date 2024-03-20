namespace Lab01.Models.Entities;

public class Comment
{
    public int Id { get; set; }
    public string Text { get; set; }
    public string Author { get; set; }
    public DateTime Date { get; set; }
}