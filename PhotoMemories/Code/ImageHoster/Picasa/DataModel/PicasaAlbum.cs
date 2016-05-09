using GooglePhotosUploader.Code.ImageHoster;
using GooglePhotosUploader.Code.ImageHoster.Picasa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace GooglePhotosUploader.Code.DataModel
{
    public class PicasaAlbum
    {
        public string Title { get; set; }
        public string Id { get; set; }
        public List<PicasaMedia> MediaCollection { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public DateTime Updated { get; set; }

        public PicasaAlbum()
        {
        }

        public PicasaAlbum(XElement element, string username)
        {
            Title = element.GetValue(XmlNamespaces.Atom, "title");
            Id = element.GetValue(XmlNamespaces.GPhoto, "id");
            MediaCollection = GetMedia(username);
            From = GetMinDate();
            To = GetMaxDate();
            Updated = DateTime.Parse(element.GetValue(XmlNamespaces.Atom, "updated"));
        }

        private DateTime GetMinDate()
        {
            var validMedia = GetValidMedia(MediaCollection);
            if (validMedia.Any())
            {
                return validMedia.Min(media => media.Date);
            }
            return DateTime.MinValue;
        }

        private DateTime GetMaxDate()
        {
            var validMedia = GetValidMedia(MediaCollection);
            if (validMedia.Any())
            {
                return validMedia.Max(media => media.Date);
            }
            return DateTime.MinValue;
        }

        private IEnumerable<PicasaMedia> GetValidMedia(IEnumerable<PicasaMedia> mediaCollection)
        {
            return mediaCollection.Where(media => media.Date > DateTime.MinValue);
        }

        private List<PicasaMedia> GetMedia(string username)
        {
            var albumUrl = string.Format("https://picasaweb.google.com/data/feed/api/user/default/albumid/{0}?imgmax=1024", Id);
            var entries = PicasaXmlProvider.GetEntries(albumUrl, username);
            return entries.Select(entry => new PicasaMedia(entry, Title)).ToList();
        }
    }
}
