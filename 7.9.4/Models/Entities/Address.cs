using System.ComponentModel.DataAnnotations;

public class Address
{
    public string Street { get; set; }
    public string City { get; set; }
    [Required, StringLength(5, MinimumLength = 5)]
    public string ZipCode { get; set; }
}