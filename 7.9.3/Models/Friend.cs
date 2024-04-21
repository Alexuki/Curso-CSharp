using System.ComponentModel.DataAnnotations;

public class Friend
{
    [Required][StringLength(50, MinimumLength = 5)]
    public string Name { get; set; }
    [Range(0,120)]
    public int Age { get; set; }
    [Required, EmailAddress]
    public string Email { get; set; }
}