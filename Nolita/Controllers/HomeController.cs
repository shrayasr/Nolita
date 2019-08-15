using Microsoft.AspNetCore.Mvc;
using Nolita.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Nolita.Controllers
{
    public class HomeController : Controller
    {

        [Route("~/")]
        public IActionResult Index()
        {
            return Json(LinkLoader.Links);
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
        public async Task<IActionResult> ReloadLinks()
        {
            await LinkLoader.ReloadLinksFromGist();
            return Json(new { Done = true });
        }

        [Route("~/edit")]
        public IActionResult EditLinks()
        {
            return Redirect(Constants.EDIT_GIST_URL);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
