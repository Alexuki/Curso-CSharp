using System.ComponentModel.DataAnnotations;


public class CreateUserViewModel
{
    [Required, StringLength(20, MinimumLength=3)]
    public string Username { get; set; }

    [Required, StringLength(50)]
    public int FullName { get; set; }

    [EmailAddress, StringLength(100)]
    public string Email { get; set; }

    [Required, StringLength(29, MinimumLength=5)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required, StringLength(29, MinimumLength = 5)]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    [Display(Name = "Retype password")]
    public string RePassword { get; set; }

    public bool Enabled { get; set; }
}