using System;
using System.Xml;

namespace Siedle.Prs602.RepairTool.Model
{
    public class Door
    {
        private readonly XmlElement _doorElement;
        public Project Owner { get; }


        public int Id => int.Parse(_doorElement.GetAttribute("id"));

        public string Name => _doorElement.SelectSingleNode("Name")?.InnerText;

        public bool Connected {
            get
            {
                var emElement = _doorElement.SelectSingleNode("EM") as XmlElement;
                return bool.TryParse(emElement?.GetAttribute("connected"), out var connected) && connected;
            }
        }

        public Door(XmlElement doorElement, Project owner)
        {
            if (doorElement == null || doorElement.Name != "Tuer")
                throw new ArgumentNullException(nameof(doorElement), @"Xml Element is null or is not named Tuer");
            _doorElement = doorElement;
            Owner = owner;
        }
    }
}