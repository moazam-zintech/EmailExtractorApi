

using System.Text.RegularExpressions;

namespace Task1.Model.Domain
{
    public class UserInfoExtractor
    {
        public List<Dictionary<string, string>> SeparateString(string inputString)
        {
            List<Dictionary<string, string>> separatedLists = new List<Dictionary<string, string>>();
            string[] entries = inputString.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string entry in entries)
            {
                string[] parts = entry.Split();
                string name = string.Join(" ", parts, 0, parts.Length - 1);
                string email = parts[parts.Length - 1];

                if (IsValidEmail(email))
                {
                    Dictionary<string, string> entryDict = new Dictionary<string, string>();
                    entryDict.Add("name", name);
                    entryDict.Add("email", email);
                    separatedLists.Add(entryDict);
                }
            }
            return separatedLists;
        }
        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailPattern);
        }
    }
}