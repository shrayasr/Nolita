using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Threading.Tasks;
using Tomlyn;

namespace Nolita
{
    public class LinkLoader
    {
        public static readonly ConcurrentDictionary<string, string> Links = new ConcurrentDictionary<string, string>();

        public static readonly ConcurrentDictionary<string, ConcurrentDictionary<string, string>> GroupedLinks
            = new ConcurrentDictionary<string, ConcurrentDictionary<string, string>>();

        public static async Task ReloadLinksFromGist()
        {
            Links.Clear();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("User-Agent", Constants.USER_AGENT);

                var response = await httpClient.GetAsync(Constants.API_GIST_URL);
                var content = await response.Content.ReadAsStringAsync();

                var definedLinks = JsonConvert
                                    .DeserializeObject<GistResponse>(content)
                                    ?.Files
                                    ?.LinksToml
                                    ?.Content;

                if (definedLinks == null)
                {
                    throw new Exception("No content at that URL");
                }

                var doc = Toml.Parse(definedLinks);

                var tables = doc.Tables;
                foreach (var table in tables)
                {
                    var groupName = table.Name.ToString().Trim();
                    var groupContents = new ConcurrentDictionary<string, string>();

                    var items = table.Items;
                    foreach (var item in items)
                    {
                        var slug = item.Key.ToString().Trim().ToLower();
                        var link = item.Value.ToString().Trim().Replace("'", "");

                        Links[slug] = link;
                        groupContents[slug] = link;
                    }

                    GroupedLinks[groupName] = groupContents;
                }
            }
        }
    }

    public class GistResponse
    {
        [JsonProperty("files")]
        public Files Files { get; set; }
    }

    public partial class Files
    {
        [JsonProperty("links.toml")]
        public LinksToml LinksToml { get; set; }
    }

    public partial class LinksToml
    {
        [JsonProperty("content")]
        public string Content { get; set; }
    }
}
