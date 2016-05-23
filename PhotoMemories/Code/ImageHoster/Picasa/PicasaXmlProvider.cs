using GoogleAuthentication;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace GooglePhotosUploader.Code.ImageHoster.Picasa
{
    class PicasaXmlProvider
    {
        public static IEnumerable<XElement> GetEntries(string url, string username)
        {
            var response = PicasaConnection.GetWebResponse(url, username);
            if (response != null)
            {
                var xml = XDocument.Parse(response);
                return xml.Root.Elements(XmlNamespaces.Atom + "entry");
            }
            return Enumerable.Empty<XElement>();
        }
    }
}
