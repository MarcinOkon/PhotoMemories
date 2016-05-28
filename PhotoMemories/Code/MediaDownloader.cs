using ImageResizer.Configuration;
using PhotoMemories.Code.ImageHoster.Picasa.DataModel;
using PhotoMemories.Code.ImageProcessing;
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
            var imageProcessorConfig = new ImageProcessorConfig();
            var index = 0;
            foreach (var media in mediaList)
            {
                media.SetFilePath(index);
                if (!CacheManager.CopiedFileFromCache(media))
                {
                    DownloadFile(media, imageProcessorConfig);
                }
                index++;
            }
        }

        private static void DownloadFile(PicasaMedia media, ImageProcessorConfig imageProcessorConfig)
        {
            if (media.ImageType == "image/jpeg")
            {
                using (var client = new WebClient())
                {
                    StartDownload(media, client, imageProcessorConfig);
                }
            }
        }

        private static void StartDownload(PicasaMedia media, WebClient client, ImageProcessorConfig imageProcessorConfig)
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


                    ImageProcessor.ProcessImage(media, cachedMediaPath, imageProcessorConfig);


                    var file = new FileInfo(cachedMediaPath);
                    new FileInfo(media.FilePath).Directory.Create();
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
