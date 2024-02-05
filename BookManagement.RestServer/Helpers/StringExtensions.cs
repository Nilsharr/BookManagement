namespace BookManagement.RestServer.Helpers;

public static class StringExtensions
{
    public static string FirstCharToLower(this string str)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return str;
        }

        return char.ToLower(str[0]) + str[1..];
    }
}