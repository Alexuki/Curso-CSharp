namespace Lab05.ViewModels.Account;

public class LoginViewModel
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? ErrorMessage { get; set; }
}

/*
(2) Clase utilizada para recibir los datos

public class LoginViewModel
{
    [Required]
    public string Username { get; set; }
    public string Password { get; set; }
}
*/