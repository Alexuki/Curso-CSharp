using System.ComponentModel.DataAnnotations;

public class CreateCustomerViewModel
{
    [Required]
    public string Name { get; set;}

    [Required, EmailAddress]
    public string Email { get; set;}

    public CustomerType Type { get; set;}
}

public enum CustomerType
{
    Vip,
    Prime,
    Standard
}