using System.Text.RegularExpressions;

namespace MyPassHolder.Common
{
    public static class Helper
    {
        public static bool IsEmailRegex(string text)
        {
            string pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            return Regex.IsMatch(text, pattern);
        }
    }
}
