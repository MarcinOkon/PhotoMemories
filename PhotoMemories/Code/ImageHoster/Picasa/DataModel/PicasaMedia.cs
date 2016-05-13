using System;
using System.Xml.Linq;

namespace GooglePhotosUploader.Code.ImageHoster.Picasa.DataModel
{
    public class PicasaMedia
    {
        public string ImageType { get; set; }
        public string FilePath { get; set; }
        public DateTime Date { get; set; }
        public string Url { get; set; }
        public string AlbumTitle { get; set; }
        public string FileName { get; set; }

        public PicasaMedia()
        {
        }

        public PicasaMedia(XElement element, string title)
        {
            AlbumTitle = title;
            ImageType = element.GetAttribute(XmlNamespaces.Atom, "content", "type");
            Date = PicasaDateTime.GetDateTime(element.GetValue(XmlNamespaces.GPhoto, "timestamp"));
            Url = element.GetElement(XmlNamespaces.Media, "group").GetAttribute(XmlNamespaces.Media, "content", "url");
            FileName = element.GetValue(XmlNamespaces.Atom, "title");
        }

        public void SetFilePath(long index)
        {
            var fileName = string.Format("{0:D5}", index);
            FilePath = string.Format(@"D:\ImageDestination\{0}.jpg", fileName);
        }

    }
}
