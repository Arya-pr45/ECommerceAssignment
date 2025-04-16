using System.Text.RegularExpressions;

namespace ECommerce.Utilities
{
    public static class EmailExtensions
    {
        public static bool IsValidEmail(this string email)
        {
            return !string.IsNullOrEmpty(email) &&
                   Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
    }
}