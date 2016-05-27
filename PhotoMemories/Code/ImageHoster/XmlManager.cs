using System.Xml.Linq;

namespace GooglePhotosUploader.Code.ImageHoster
{
    public static class XmlExtensions
    {
        public static XElement GetElement(this XElement xml, XNamespace xmlNameSpace, string elementName)
        {
            var element = xml.Element(xmlNameSpace + elementName);
            if (element != null)
            {
                return element;
            }
            return null;
        }

        public static string GetValue(this XElement xml, XNamespace xmlNameSpace, string elementName)
        {
            var element = xml.GetElement(xmlNameSpace, elementName);
            if (element != null)
            {
                return element.Value;
            }
            return null;
        }

        public static string GetAttribute(this XElement xml, XNamespace xmlNameSpace, string elementName, string attributeName)
        {
            var element = xml.GetElement(xmlNameSpace, elementName);
            if (element != null)
            {
                var attribute = element.Attribute(attributeName);
                if (attribute != null)
                {
                    return attribute.Value;
                }
            }
            return null;
        }
    }
}
