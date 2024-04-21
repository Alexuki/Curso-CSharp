using System.ComponentModel.DataAnnotations;

public class Friend
{
    [Required]
    public string Name { get; set; }
    [Range(1, 120)]
    public int Age { get; set; }
}