using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Nolita.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Nolita.Controllers
{
    public class LinksController : Controller
    {
        private readonly AppSettings _appSettings;

        public LinksController(IOptions<AppSettings> AppSettings)
        {
            _appSettings = AppSettings.Value;
        }

        [Route("~/")]
        public IActionResult Index()
        {
            return View(LinkLoader.GroupedLinks);
        }

        [Route("~/{slug}")]
        public IActionResult RedirectSlug(string slug)
        {
            slug = slug.Trim().ToLower();

            if (LinkLoader.Links.TryGetValue(slug, out string redirectURL) == false)
            {
                return Redirect("~/");
            }
            else
            {
                return Redirect(redirectURL);
            }
        }

        [Route("~/reload")]
        public async Task<IActionResult> ReloadLinks(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return Unauthorized(new
                {
                    Error = "No Key"
                });
            }

            if (key != _appSettings.SecretKey)
            {
                return Unauthorized(new
                {
                    Error = "Invalid Key"
                });
            }

            await LinkLoader.ReloadLinksFromGist();

            return Redirect("/");
        }

        [Route("~/edit")]
        public IActionResult EditLinks()
        {
            return Redirect(Constants.EditGistURL);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
