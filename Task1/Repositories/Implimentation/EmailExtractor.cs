using System.Globalization;
using System.Text.RegularExpressions;

namespace Task1.Repositories.Implimentation
{
    public class EmailExtractor
    {
        public List<Dictionary<string, string>> SeparateString(string inputString)
        {
            List<Dictionary<string, string>> separatedLists = new List<Dictionary<string, string>>();
            string[] entries = inputString.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string entry in entries)
            {
                string[] parts = entry.Split();
                if (parts.Length < 2)
                {
                    Console.WriteLine($"Error: Invalid entry '{entry}'. It should contain at least a first name and an email.");
                    continue;
                }
                string firstName = parts[0];
                string lastName = string.Join(" ", parts, 1, parts.Length - 2);
                string email = parts[parts.Length - 1];

                if (IsValidEmail(email))
                {
                    Dictionary<string, string> entryDict = new Dictionary<string, string>();
                    entryDict.Add("firstName", firstName);
                    entryDict.Add("lastName", lastName);
                    entryDict.Add("email", email);
                    separatedLists.Add(entryDict);
                }
                else
                {
                    Dictionary<string, string> entryDict = new Dictionary<string, string>();
                    entryDict.Add("incorrectEmail", email);
                    /*           entryDict.Add("incorrectFname", firstName);
                               entryDict.Add("incorrectLname", lastName);*/
                    // separatedLists.Add(entryDict);
                    //Console.WriteLine($"Error: Invalid email '{email}' for entry      '{entry}'.");
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