using System.ComponentModel.DataAnnotations;


public class Friend: IValidatableObject
{
    //[Required(ErrorMessage = "El campo de Nombre es obligatorio"), Contains("a", ErrorMessage ="Debe contener una 'a'")]
    [Required(ErrorMessage = "El campo de Nombre es obligatorio")]
    [Contains("a", ErrorMessage="{0} debe contener '{1}'")]
    public string Name { get; set; }
    [Range(18,120)]
    public int Age { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public Address Address { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(Address?.Street))
        {
            yield return new ValidationResult("Debe introducir email o street");
        }
        yield return ValidationResult.Success;
    }
}