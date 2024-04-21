public class AccountServices : IAccountServices
{
    public bool CheckCredentials(string username, string password)
    {
        return username == password;
    }

    public IEnumerable<string> GetRolesForUser(string userName)
    {
        List<string> roles = new List<string>();
        if (userName.Equals("john", StringComparison.OrdinalIgnoreCase))
        {
            roles.Add("admin");
        }
        return roles;
    }


    /* public IEnumerable<string> GetRolesForUser(string userName)
    {
        if (userName.Equals("john", StringComparison.OrdinalIgnoreCase))
        {
            yield return "admin";
        }
    } */


}