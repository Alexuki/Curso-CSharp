using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

public class UserViewModel
{
    [Required, StringLength(50)]
    public string Name { get; set; }

    [Required, StringLength(20, MinimumLength=3)]
    [Remote("NickExists", "User", ErrorMessage="Nick already exists")]
    public string Nickname { get; set; }

    [Display(Name = "User type")]
    [Range(1, 3, ErrorMessage = "Unknown type")]
    public int Type { get; set; }

    [StringLength(100)]
    [EmailAddress(ErrorMessage = "Invalid email")]
    public string Email { get; set; }

    [Required, StringLength(20, MinimumLength=5)]
    public string Password { get; set; }

    [Required, StringLength(20, MinimumLength = 5), Compare("Password")]
    [Display(Name = "Retype password")]
    public string RePassword { get; set; }

    [Display(Name = "Date of birth")]
    public DateTime? Birthdate { get; set; }

    public bool Enabled { get; set; }

    public IEnumerable<UserType> UserTypes { get; set; }

}