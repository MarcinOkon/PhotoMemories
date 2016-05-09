using GooglePhotosUploader.Code.DataModel;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace GooglePhotosUploader.Code
{
    class MediaDownloader
    {
        public static void DownloadFiles(IEnumerable<PicasaMedia> mediaList)
        {
            foreach (var media in mediaList)
            {
                DownloadFile(media);
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
                    client.DownloadFile(media.Url, media.FilePath);
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
