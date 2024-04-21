namespace Lab03.Models.Services;

public interface IUserServices
{
    bool Exists(string nickName);
}