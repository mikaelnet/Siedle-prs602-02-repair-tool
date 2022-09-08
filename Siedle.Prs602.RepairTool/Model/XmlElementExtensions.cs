using System.Xml;

namespace Siedle.Prs602.RepairTool.Model
{
    public static class XmlElementExtensions
    {
        public static void SetInnerTextElement(this XmlElement element, string innerElementName, string value)
        {
            if (element?.OwnerDocument == null)
                return;

            var innerElement = element.SelectSingleNode(innerElementName) as XmlElement;
            if (innerElement == null)
            {
                innerElement = element.OwnerDocument.CreateElement(innerElementName);
                element.AppendChild(innerElement);
            }
            innerElement.InnerText = value;
        }
    }
}