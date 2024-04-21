namespace Lab04.Models.Services;

public interface IUserServices
{
    bool Exists(string nickName);
}