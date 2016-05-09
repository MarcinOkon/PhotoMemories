using GooglePhotosUploader.Code.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
            var regex = new Regex("\\d{4}-\\d{2}-\\d{2}");

            var userUrl = "https://picasaweb.google.com/data/feed/api/user/default";
            var entries = PicasaXmlProvider.GetEntries(userUrl, UserName);
            var albumCollection = entries.Select(entry => new PicasaAlbum(entry, UserName));

            var filteredAlbums = albumCollection.Where(album => album.Title != "Automatische Sicherung" && !album.Title.StartsWith("Hangout:"));
            return filteredAlbums.Where(album => !regex.IsMatch(album.Title)).ToList();
        }
    }
}
