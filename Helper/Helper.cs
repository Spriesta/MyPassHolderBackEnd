using System.Text.RegularExpressions;

namespace MyPassHolder.Common
{
    public static class Helper
    {
        private static readonly Random random = new Random();
        private const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public static bool IsEmailRegex(string text)
        {
            string pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            return Regex.IsMatch(text, pattern);
        }

        public static string generateRandomString(int length)
        {
            char[] randomString = new char[length];

            for (int i = 0; i < length; i++)
            {
                randomString[i] = characters[random.Next(characters.Length)];
            }

            return new string(randomString);
        }
    }
}
