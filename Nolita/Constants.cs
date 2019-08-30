namespace Nolita
{
    public class Constants
    {
        public static string GistID
        {
            set
            {
                EditGistURL = $"https://gist.github.com/shrayasr/{value}/edit";
                APIGistURL = $"https://api.github.com/gists/{value}";
            }
        }

        public static string UserAgent = "Notion shortcuts app by github.com/shrayasr";

        public static string EditGistURL { get; private set; }
        public static string APIGistURL { get; private set; }
    }
}
