namespace Lab03.Models.Services;

public class UserServices: IUserServices
{
    public bool Exists(string nickName)
    {
        var invalidNames = new[] { "john", "peter", "mark", "jose" };
        return invalidNames.Any(n => n == nickName);
    }
}