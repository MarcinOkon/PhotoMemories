using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace GooglePhotosUploader.Code.ImageHoster.Picasa.DataModel
{
    public class PicasaAlbum : PicasaAlbumBase
    {
        public List<PicasaMedia> MediaCollection { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public PicasaAlbum()
        {
        }

        public PicasaAlbum(XElement element, string username) : base(element)
        {
            Init(username);
        }

        public PicasaAlbum(PicasaAlbumBase albumBase, string username) : base(albumBase)
        {
            Init(username);
        }

        private void Init(string username)
        {
            MediaCollection = GetMedia(username);
            From = GetMinDate();
            To = GetMaxDate();
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
