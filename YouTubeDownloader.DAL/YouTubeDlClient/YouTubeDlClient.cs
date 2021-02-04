using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using NYoutubeDL;
using System.Web;

namespace YouTubeDownloader.DAL.YouTubeDlClient
{
    public class YouTubeDlClient 
    {
        public string filePath { get; set; }
        public YoutubeDL youtubeDl;

        public YouTubeDlClient()
        {
            youtubeDl = new YoutubeDL();
            youtubeDl.StandardOutputEvent += (sender, output) => Console.WriteLine(output);
            youtubeDl.StandardErrorEvent += (sender, errorOutput) => Console.WriteLine(errorOutput);
            youtubeDl.YoutubeDlPath = @".\youtube-dl.exe";
        }

        public void DownloadVideo(string URL)
        {
            var rnd = new Random();
            int fileName = rnd.Next(0, 99999);
            filePath = @$".\videoDownloads\video{fileName}.mp4";
            youtubeDl.Options.FilesystemOptions.Output = filePath;

            youtubeDl.DownloadAsync(URL);
        }
    }
}
