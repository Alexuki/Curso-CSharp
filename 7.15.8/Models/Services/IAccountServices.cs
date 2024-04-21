public interface IAccountServices
{
    bool CheckCredentials(string username, string password);
    IEnumerable<string> GetRolesForUser(string userName);
}