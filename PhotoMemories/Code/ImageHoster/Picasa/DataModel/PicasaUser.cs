using PicasaAPI;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PhotoMemories.Code.ImageHoster.Picasa.DataModel
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
            var entries = RequestManager.GetAlbums(UserName);
            var albumCollection = entries.Select(entry => new PicasaAlbum(entry, UserName));

            var filteredAlbums = albumCollection.Where(album => album.Title != "Automatische Sicherung" && !album.Title.StartsWith("Hangout:") && album.Title != "Auto Backup");
            var regex = new Regex("\\d{4}-\\d{2}-\\d{2}");
            return filteredAlbums.Where(album => !regex.IsMatch(album.Title)).ToList();
        }
    }
}
