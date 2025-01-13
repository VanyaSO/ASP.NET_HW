using System.Text;

namespace lesson_08_01_blog.Helpers;

public static class PasswordHelper
{
    private static string allSymbold = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()-_=+[]{}|;:,.<>?";
    private static Random _random = new Random();

    public static string GeneratePassword(int length = 10)
    {
        var password = new StringBuilder();
        char randomSybmol;
        
        for (int i = 0; i < length; i++)
        {
            randomSybmol = allSymbold[_random.Next(0, allSymbold.Length - 1)];
            password.Append(randomSybmol);
        }

        return password.ToString();
    }
}