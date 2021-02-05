using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace YouTubeDownloader.DAL.Repos
{
    public class DownloadRepo
    {
        public string filePath { get; set; }
        private YoutubeClient youtubeDl;

        public DownloadRepo()
        {
            youtubeDl = new YoutubeClient();
        }

        public async void DownloadVideoAsync(string url = "https://www.youtube.com/watch?v=WyPm6p0GnwY", string audioOnly = "off")
        {
            var video = youtubeDl.Videos.GetAsync(url);
            var title = video.Result.Title;
            var id = video.Result.Id;

            var streamManifest = await youtubeDl.Videos.Streams.GetManifestAsync(id);
            IStreamInfo streamInfo;

            if (audioOnly == "on")
            {
                streamInfo = streamManifest.GetAudioOnly().WithHighestBitrate();
            }
            else
            {
                streamInfo = streamManifest.GetMuxed().WithHighestVideoQuality();
            }

            var filename = $"{title}.mp4";
            filePath = @$".\videoDownloads\{filename}";

            if (streamInfo != null)
            {
                // Get the actual stream
                var stream = await youtubeDl.Videos.Streams.GetAsync(streamInfo);

                // Download the stream to file
                await youtubeDl.Videos.Streams.DownloadAsync(streamInfo, filePath);
            }


        }

        public async void DownloadPlaylistAsync(string url)
        {
            var playlist = await youtubeDl.Playlists.GetAsync("PLa1F2ddGya_-UvuAqHAksYnB0qL9yWDO6");
        }
    }
}
