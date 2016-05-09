using System.Xml.Linq;

namespace GooglePhotosUploader.Code.ImageHoster
{
    class XmlNamespaces
    {
        public static XNamespace Atom { get { return "http://www.w3.org/2005/Atom"; } }
        public static XNamespace GPhoto { get { return "http://schemas.google.com/photos/2007"; } }
        public static XNamespace Exif { get { return "http://schemas.google.com/photos/exif/2007"; } }
        public static XNamespace Media { get { return "http://search.yahoo.com/mrss/"; } }
    }
}
