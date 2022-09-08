using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Siedle.Prs602.RepairTool.Model
{
    public class Project
    {
        public FileInfo ProjectFile { get; }

        private XmlDocument _xml;

        private readonly List<Door> _doors = new List<Door>();

        private readonly List<Sluice> _sluices= new List<Sluice>();

        private readonly List<Card> _cards = new List<Card>();

        public IReadOnlyList<Card> Cards => _cards;
        public IReadOnlyList<Sluice> Sluices => _sluices;
        public IReadOnlyList<Door> Doors => _doors;


        public Project(FileInfo file)
        {
            ProjectFile = file;
        }

        public void Load()
        {
            _xml = new XmlDocument();
            _xml.PreserveWhitespace = true;
            _xml.Load(ProjectFile.FullName);

            _doors.Clear();
            // ReSharper disable once PossibleNullReferenceException
            foreach (XmlElement doorElement in _xml.SelectNodes("/DB/GeraeteListe/Tuerliste/Tuer"))
            {
                _doors.Add(new Door(doorElement, this));
            }

            _sluices.Clear();
            // ReSharper disable once PossibleNullReferenceException
            foreach (XmlElement sluiceElement in _xml.SelectNodes("/DB/Schleusen/Schleuse"))
            {
                _sluices.Add(new Sluice(sluiceElement, this));
            }

            _cards.Clear();
            // ReSharper disable once PossibleNullReferenceException
            foreach (XmlElement cardElement in _xml.SelectNodes("/DB/Geraeteeintraege/Geraete/ECELMKartenEintraege/ECELMKartenEintrag"))
            {
                _cards.Add(new Card(cardElement, this));
            }
        }

        public void Save()
        {
            _xml.Save($"{ProjectFile.FullName}.2");
        }

        public void AddCard(int index, int entryNumber, string cardNumber, string description)
        {
            var cardElement = _xml.CreateElement("ECELMKartenEintrag");
            cardElement.SetAttribute("index", index.ToString());
            cardElement.SetAttribute("eintragsnummer", entryNumber.ToString());
            cardElement.SetAttribute("kartennummer", cardNumber);
            for (int i = 1; i <= 8; i++)
                cardElement.SetAttribute($"sk{i}", "False");
            for (int i = 1; i <= 4; i++)
                cardElement.SetAttribute($"schleu{i}", "False");
            cardElement.SetAttribute("sperre", "False");

            var descElement = _xml.CreateElement("Beschreibung");
            descElement.InnerText = description;
            cardElement.AppendChild(descElement);

            var cardElements = _xml.SelectSingleNode("/DB/Geraeteeintraege/Geraete/ECELMKartenEintraege") as XmlElement;
            cardElements?.AppendChild(cardElement);
        }


    }
}
