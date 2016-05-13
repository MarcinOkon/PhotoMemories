using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GooglePhotosUploader.Code.ImageHoster.Picasa.DataModel
{
    public class PicasaAlbumBase
    {
        public string Title { get; set; }
        public string Id { get; set; }
        public DateTime Updated { get; set; }

        public PicasaAlbumBase()
        {

        }

        public PicasaAlbumBase(PicasaAlbumBase albumBase)
        {
            Title = albumBase.Title;
            Id = albumBase.Id;
            Updated = albumBase.Updated;
        }

        public PicasaAlbumBase(XElement element)
        {
            Title = element.GetValue(XmlNamespaces.Atom, "title");
            Id = element.GetValue(XmlNamespaces.GPhoto, "id");
            Updated = DateTime.Parse(element.GetValue(XmlNamespaces.Atom, "updated"));
        }
    }
}
