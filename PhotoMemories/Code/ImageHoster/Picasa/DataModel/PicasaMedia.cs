using GooglePhotosUploader.Code.ImageHoster;
using System;
using System.Xml.Linq;

namespace GooglePhotosUploader.Code.DataModel
{
    public class PicasaMedia
    {
        public PicasaMedia()
        {

        }

        public PicasaMedia(XElement element, string title)
        {
            AlbumTitle = title;
            ImageType = element.GetAttribute(XmlNamespaces.Atom, "content", "type");
            Date = GetDate(element.GetValue(XmlNamespaces.GPhoto, "timestamp"));
            Url = element.GetElement(XmlNamespaces.Media, "group").GetAttribute(XmlNamespaces.Media, "content", "url");
            FileName = element.GetValue(XmlNamespaces.Atom, "title");
        }

        private DateTime GetDate(string timestamp)
        {
            if (timestamp != null)
            {
                var unixTimeStamp = Convert.ToInt64(timestamp);
                DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                return dtDateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
            }
            return DateTime.MinValue;
        }

        public void SetFilePath(long index)
        {
            var fileName = string.Format("{0:D5}", index);
            FilePath = string.Format(@"D:\ImageDestination\{0}.jpg", fileName);
        }

        public string ImageType { get; set; }

        public string FilePath { get; set; }

        public DateTime Date { get; set; }

        public string Url { get; set; }

        public string AlbumTitle { get; set; }

        public string FileName { get; set; }
    }
}
