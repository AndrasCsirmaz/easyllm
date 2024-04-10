using Microsoft.Extensions.Primitives;

namespace www1.Components;

public class UserHandler
{
    private static List<string> UUU = new List<string>
    {
        "\\",
        "guest",
        "gast",
        "admin"
    };

    private static Dictionary<string, string> users = new Dictionary<string, string>
    {
        {"gast","go"}
    };
    public static int GetUserId(string loginname, StringValues password)
    {
        if (loginname.Contains("\\") || !users.ContainsKey(loginname)) return 0;
        try
        {
            if(password==users[loginname]) return UUU.IndexOf(loginname);
        }
        catch (Exception e)
        {
            return 0;
        }

        return 0;
    }
}