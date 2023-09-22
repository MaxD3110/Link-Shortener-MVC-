using LinkShortener.Service;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortener.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILinkService _linkService;

        public HomeController(ILinkService linkService)
        {
            _linkService = linkService;
        }

        public async Task<IActionResult> Index()
        {
            TempData["alertMessage"] = null;
            var data = await _linkService.GetAllAsync();

            foreach (var el in data)
            {
                el.ShortUrl = Request.Scheme + "://" + Request.Host.Value + "/" + el.ShortUrl;
            }

            return View(data);
        }

        public IActionResult Shorten()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Shorten(string link)
        {
            if (!_linkService.ValidateUrl(link))
            {
                TempData["alertMessage"] = "Invalid link!";
                return View();
            }

            var isExist = await _linkService.GetByUrlAsync(link);

            if (isExist != null)
            {
                TempData["alertMessage"] = $"Shortened URL with path {link} already exist!";
                return View();
            }

            var result = await _linkService.CreateAsync(link);

            if (result < 1)
                return View("Error");

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var success =  await _linkService.DeleteAsync(id);

            if (success < 1) return View("Error");

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Regenerate(int id)
        {
            var success = await _linkService.RegenerateAsync(id);

            if (success < 1) return View("Error");

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var data = await _linkService.GetByIdAsync(id);

            if (data == null) return View("NotFound");

            data.ShortUrl = Request.Scheme + "://" + Request.Host.Value + "/" + data.ShortUrl; 

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, string link)
        {
            
            if (!_linkService.ValidateUrl(link))
            {
                TempData["alertMessage"] = "Invalid link!";
                return View();
            } 

            var result = await _linkService.UpdateAsync(link, id);

            if (result < 1)
                return View("Error");

            return RedirectToAction("Index");
        }
    }
}