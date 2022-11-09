using System.Text.RegularExpressions;

namespace Domain
{
    public static class Utils
    {
        public static bool ValidateEmail(string email)
        {
            Regex regex = new (@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
           
            Match match = regex.Match(email);

            return match.Success;
        }
    }
}
