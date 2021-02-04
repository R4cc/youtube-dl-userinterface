using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouTubeDownloader.DAL.Repos;

namespace YouTubeDownloader.UI.Controllers
{
    public class PlaylistController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DownloadPlaylist(string url)
        {
            var ytdl = new DownloadRepo();
            return RedirectToAction("Index");
        }
    }
}
