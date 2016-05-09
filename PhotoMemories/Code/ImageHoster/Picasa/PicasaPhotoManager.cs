using GooglePhotosUploader.Code.DataModel;
using GooglePhotosUploader.Code.ImageHoster.Picasa.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace GooglePhotosUploader.Code
{
    class PicasaPhotoManager
    {
        public static void DownloadMedia()
        {
            var picasaProject = LoadPicasaProject(new List<string> { "Marcin", "Daniela" });

            List<PicasaMedia> mediaCollection = GetMediaCollection(picasaProject);

            if (mediaCollection.Any())
            {
                var matchingMedia = GetMatchingMedia(mediaCollection);

                var filteredMedia = GetFilteredMedia(matchingMedia);

                var sortedMedia = GetSortedMedia(filteredMedia);

                SetFilePaths(sortedMedia);
                MediaDownloader.DownloadFiles(sortedMedia);
                ImageProcessor.ResizeImages(sortedMedia);
            }

        }

        private static List<PicasaMedia> GetMediaCollection(PicasaProject picasaProject)
        {
            return picasaProject.UserCollection
                .SelectMany(user => user.AlbumCollection
                .SelectMany(album => album.MediaCollection))
                .Where(media => media.Date > DateTime.MinValue).ToList();
        }

        private static void SetFilePaths(IOrderedEnumerable<PicasaMedia> sortedMedia)
        {
            var index = 0;
            foreach (var media in sortedMedia)
            {
                media.SetFilePath(index);
                index++;
            }
        }

        private static IOrderedEnumerable<PicasaMedia> GetSortedMedia(IEnumerable<PicasaMedia> filteredMedia)
        {
            return filteredMedia.OrderBy(media => media.Date);
        }

        private static IEnumerable<PicasaMedia> GetFilteredMedia(List<PicasaMedia> matchingMedia)
        {
            return matchingMedia.GroupBy(media => new { media.FileName, media.Date }).Select(group => group.FirstOrDefault());
        }

        private static PicasaProject LoadPicasaProject(List<string> usernames)
        {
            var filePath = "D:\\photos.xml";

            var serializer = new XmlSerializer(typeof(PicasaProject));
            using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                if (fileStream.Length > 1)
                {
                    var existingPicasaProject = (PicasaProject) serializer.Deserialize(fileStream);
                    existingPicasaProject.Update();
                    return existingPicasaProject;
                }
                var picasaProject = new PicasaProject(usernames);
                serializer.Serialize(fileStream, picasaProject);
                return picasaProject;
            }
        }
        
        private static List<PicasaMedia> GetMatchingMedia(List<PicasaMedia> mediaCollection)
        {
            var days = 7;
            var minDate = mediaCollection.Min(media => media.Date);
            var matchingMedia = new List<PicasaMedia>();

            var checkDate = DateTime.Now;

            do
            {
                matchingMedia.AddRange(mediaCollection.Where(media => media.Date > checkDate.AddDays(-1 * days) && media.Date < checkDate.AddDays(days)));
                checkDate = checkDate.AddYears(-1);
            } while (checkDate > minDate);

            return matchingMedia;
        }
    }
}
