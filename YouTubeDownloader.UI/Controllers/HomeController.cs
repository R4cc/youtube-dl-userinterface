using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using YouTubeDownloader.DAL.Repos;
using YouTubeDownloader.UI.Models;

namespace YouTubeDownloader.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Download(string url, string audioOnly)
        {
            var ytdl = new DownloadRepo();

            ytdl.DownloadVideoAsync(url, audioOnly);

            return RedirectToAction("Index");
        }
    }
}