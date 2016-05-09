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
        public PicasaAlbum()
        {

        }

        public PicasaAlbum(XElement element, string username)
        {
            Title = element.GetValue(XmlNamespaces.Atom, "title");
            Id = element.GetValue(XmlNamespaces.GPhoto, "id");
            MediaCollection = GetMedia(username);
        }

        public string Title { get; set; }

        public string Id { get; set; }

        public List<PicasaMedia> MediaCollection { get; set; }


        public DateTime From
        {
            get
            {
                var validMedia = GetValidMedia(MediaCollection);
                if (validMedia.Any())
                {
                    return validMedia.Min(media => media.Date);
                }
                return DateTime.MinValue;
            }
        }

        public DateTime To
        {
            get
            {
                var validMedia = GetValidMedia(MediaCollection);
                if (validMedia.Any())
                {
                    return validMedia.Max(media => media.Date);
                }
                return DateTime.MinValue;
            }
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
