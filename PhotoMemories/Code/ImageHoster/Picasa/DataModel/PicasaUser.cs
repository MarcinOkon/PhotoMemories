using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GooglePhotosUploader.Code.ImageHoster.Picasa.DataModel
{
    public class PicasaUser
    {
        public PicasaUser()
        {

        }

        public PicasaUser(string username)
        {
            UserName = username;
            AlbumCollection = GetAlbumCollection();
        }

        public List<PicasaAlbum> AlbumCollection { get; set; }

        public string UserName { get; set; }

        private List<PicasaAlbum> GetAlbumCollection()
        {
            var entries = GetAlbumEntries();
            var albumCollection = entries.Select(entry => new PicasaAlbum(entry, UserName));
            return GetFilteredAlbumCollection(albumCollection);
        }

        private static List<PicasaAlbumBase> GetFilteredAlbumCollection(IEnumerable<PicasaAlbumBase> albumCollection)
        {
            var filteredAlbums = albumCollection.Where(album => album.Title != "Automatische Sicherung" && !album.Title.StartsWith("Hangout:"));
            var regex = new Regex("\\d{4}-\\d{2}-\\d{2}");
            return filteredAlbums.Where(album => !regex.IsMatch(album.Title)).ToList();
        }
        private static List<PicasaAlbum> GetFilteredAlbumCollection(IEnumerable<PicasaAlbum> albumCollection)
        {
            var filteredAlbums = albumCollection.Where(album => album.Title != "Automatische Sicherung" && !album.Title.StartsWith("Hangout:"));
            var regex = new Regex("\\d{4}-\\d{2}-\\d{2}");
            return filteredAlbums.Where(album => !regex.IsMatch(album.Title)).ToList();
        }

        private IEnumerable<System.Xml.Linq.XElement> GetAlbumEntries()
        {
            var userUrl = "https://picasaweb.google.com/data/feed/api/user/default";
            return PicasaXmlProvider.GetEntries(userUrl, UserName);
        }

        public void Update()
        {
            var entries = GetAlbumEntries();
            var albumCollection = entries.Select(entry => new PicasaAlbumBase(entry));
            albumCollection = GetFilteredAlbumCollection(albumCollection);
            var diff = albumCollection.Except(AlbumCollection, new PicasaAlbumBaseEqualityComparer());

            foreach (var albumBase in diff)
            {
                var existingAlbum = AlbumCollection.FirstOrDefault(album => album.Id == albumBase.Id);
                if (existingAlbum != null)
                {
                    AlbumCollection.Remove(existingAlbum);
                }
                AlbumCollection.Add(new PicasaAlbum(albumBase, UserName));
            }
        }
    }
}
