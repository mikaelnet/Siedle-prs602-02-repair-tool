using System.Linq;
using System.Xml;

namespace Siedle.Prs602.RepairTool.Model
{
    public class Sluice
    {
        private readonly XmlElement _sluiceElement;
        public Project Owner { get; }


        public string Id => _sluiceElement.GetAttribute("id");

        /// <summary>
        /// This name seems to be static in the Siedle software
        /// </summary>
        public string InternalName => _sluiceElement.SelectSingleNode("Name")?.InnerText;

        private string _name = null;
        public string Name
        {
            get
            {
                if (_name != null)
                    return _name;

                if (!int.TryParse(_sluiceElement.SelectSingleNode(@"Schaltausgang1")?.InnerText, out var switch1))
                    return InternalName;
                if (!int.TryParse(_sluiceElement.SelectSingleNode(@"Schaltausgang2")?.InnerText, out var switch2))
                    return InternalName;
                var door1 = Owner.Doors.SingleOrDefault(d => d.Id == switch1);
                var door2 = Owner.Doors.SingleOrDefault(d => d.Id == switch2);
                if (door1 == null || door2 == null)
                    return InternalName;

                _name = $"Sluss {door1.Name} / {door2.Name}";
                return _name;
            }
        }

        public bool Active => bool.TryParse(_sluiceElement.GetAttribute("aktiviert"), out var active) && active;

        public Sluice(XmlElement sluiceElement, Project owner)
        {
            _sluiceElement = sluiceElement;
            Owner = owner;
        }
    }
}