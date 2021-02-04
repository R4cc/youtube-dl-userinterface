using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeDownloader.UI.Controllers
{
    public class DownloadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Download(string url)
        {
            var ytdl = new YouTubeDownloader.DAL.YouTubeDlClient.YouTubeDlClient();

            ytdl.DownloadVideo(url);
            return RedirectToAction("Index");
        }
    }
}
