using PhotoMemories.Code.ImageHoster.Picasa.DataModel;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;

namespace PhotoMemories.Code
{
    class MediaDownloader
    {
        const string cachePath = @"D:\PhotoMemories\Cache";

        public static void GetFiles(IEnumerable<PicasaMedia> mediaList)
        {
            var index = 0;
            foreach (var media in mediaList)
            {
                media.SetFilePath(index);
                if (!CacheManager.CopiedFileFromCache(media))
                {
                    DownloadFile(media);
                }
                index++;
            }
        }

        private static void DownloadFile(PicasaMedia media)
        {
            if (media.ImageType == "image/jpeg")
            {
                using (var client = new WebClient())
                {
                    StartDownload(media, client);
                }
            }
        }

        private static void StartDownload(PicasaMedia media, WebClient client)
        {
            for (int retry = 0; retry < 3; retry++)
            {
                try
                {
                    if (retry > 0)
                    {
                        Thread.Sleep(1000);
                    }
                    Directory.CreateDirectory(cachePath);
                    var cachedMediaPath = Path.Combine(cachePath, media.Id);
                    client.DownloadFile(media.Url, cachedMediaPath);
                    var file = new FileInfo(cachedMediaPath);
                    file.CopyTo(media.FilePath);
                    return;
                }
                catch (WebException exception)
                {
                    if (exception.Status != WebExceptionStatus.ReceiveFailure)
                    {
                        throw exception;
                    }
                }
            }
        }
    }
}
