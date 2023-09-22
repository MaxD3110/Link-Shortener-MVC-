using LinkShortener.Service;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortener.Controllers
{
    public class ShortController : Controller
    {
        private ILinkService _linkService;

        public ShortController(ILinkService linkService)
        {
            _linkService = linkService;
        }

        //Immediately redirect to stored original URL
        public async Task<IActionResult> Index(string code)
        {
            var result = await _linkService.GetByTokenAsync(code);

            if (result == null)
                return View("NotFound");

            await _linkService.IncrementViewed(result);

            return Redirect(result.RawUrl);
        }
    }
}
