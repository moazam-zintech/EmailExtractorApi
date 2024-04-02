using static Azure.Core.HttpHeader;

namespace Task1.Model.Domain
{
    public class UserInfoExtractor
    {
        public (string Name, string Email) ExtractUserData(string userInput)
        {


            var names = new List<string>();
            var emails = new List<string>();

            return (string.Join(", ", names), string.Join(", ", emails));
        }
    }
}
