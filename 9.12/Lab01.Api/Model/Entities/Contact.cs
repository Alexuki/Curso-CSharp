using System.ComponentModel.DataAnnotations;

namespace Lab01.Api.Model.Entities;

public class Contact
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Phone { get; set; }
}