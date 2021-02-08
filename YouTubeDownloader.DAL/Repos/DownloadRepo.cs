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

        public async void DownloadVideoAsync(string url = "", string audioOnly = "off")
        {
            var video = youtubeDl.Videos.GetAsync(url);
            var title = video.Result.Title;
            var id = video.Result.Id;

            var streamManifest = await youtubeDl.Videos.Streams.GetManifestAsync(id);
            string filename;
            IStreamInfo streamInfo;

            if (audioOnly == "on")
            {
                streamInfo = streamManifest.GetAudioOnly().WithHighestBitrate();
                filename = $"{title}.mp3";
            }
            else
            {
                streamInfo = streamManifest.GetMuxed().WithHighestVideoQuality();
                filename = $"{title}.mp3";
            }

            filePath = @$".\videoDownloads\{filename}";

            if (streamInfo != null)
            {
                // Download the stream to file
                await youtubeDl.Videos.Streams.DownloadAsync(streamInfo, filePath);
            }
        }

        public async void DownloadPlaylistAsync(string url = "", string audioOnly = "")
        {
            string playlistId = url.Remove(0, 49);

            if (playlistId.Contains("&index="))
            {
                playlistId = playlistId.Remove(34, playlistId.Length - 34);
            }

            // Get playlist metadata
            var playlist = await youtubeDl.Playlists.GetAsync(playlistId);

            // Get all playlist videos
            var playlistVideos = await youtubeDl.Playlists.GetVideosAsync(playlist.Id);

            // Creates directory where videos will be saved
            var path = @$".\videoDownloads\{playlist.Title}";
            System.IO.Directory.CreateDirectory(path);

            foreach (var video in playlistVideos)
            {
                var streamManifest = await youtubeDl.Videos.Streams.GetManifestAsync(video.Id);
                string filename;
                IStreamInfo streamInfo;

                if (audioOnly == "on")
                {
                    // Download Audio
                    streamInfo = streamManifest.GetAudioOnly().WithHighestBitrate();
                    filename = $"{video.Title}.mp3";
                }
                else
                {
                    // Download Video
                    streamInfo = streamManifest.GetMuxed().WithHighestVideoQuality();
                    filename = $"{video.Title}.mp4";
                }

                filePath = @$"{path}\{filename}";

                if (streamInfo != null)
                {
                    // Download the stream to file
                    await youtubeDl.Videos.Streams.DownloadAsync(streamInfo, filePath);
                }
            }
        }
    }
}
