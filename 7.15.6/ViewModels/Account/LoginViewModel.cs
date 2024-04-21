using System.ComponentModel.DataAnnotations;

//(2) Clase utilizada para recibir los datos
public class LoginViewModel
{
    [Required]
    public string Username { get; set; }
    public string Password { get; set; }
}
