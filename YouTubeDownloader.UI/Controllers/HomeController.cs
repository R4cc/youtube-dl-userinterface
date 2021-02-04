using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        public IActionResult Download(string url, bool audioOnly)
        {
            var ytdl = new DownloadRepo();

            ytdl.DownloadVideoAsync(url, audioOnly);

            //FileInfo file = new FileInfo(filePath);
            //if (file.Exists)
            //{
            //    var wc = new WebClient();
            //    wc.DownloadFileAsync(filePath);
            //}

            return RedirectToAction("Index");
        }
    }
}
