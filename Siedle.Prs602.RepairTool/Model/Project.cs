using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using JetBrains.Annotations;

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

    public class Door
    {
        private readonly XmlElement _doorElement;

        public int Id => int.Parse(_doorElement.GetAttribute("id"));

        public string Name => _doorElement.SelectSingleNode("Name")?.InnerText;

        public bool Connected {
            get
            {
                var emElement = _doorElement.SelectSingleNode("EM") as XmlElement;
                return bool.TryParse(emElement?.GetAttribute("connected"), out var connected) && connected;
            }
        }

        public Door(XmlElement doorElement)
        {
            if (doorElement == null || doorElement.Name != "Tuer")
                throw new ArgumentNullException(nameof(doorElement), "element is null or is not named Tuer");
            _doorElement = doorElement;
        }
    }

    public class Sluice
    {
        private readonly XmlElement _sluiceElement;

        public string Id => _sluiceElement.GetAttribute("id");

        public string Name => _sluiceElement.SelectSingleNode("Name")?.InnerText;

        public bool Active => bool.TryParse(_sluiceElement.GetAttribute("aktiviert"), out var active) && active;

        public Sluice(XmlElement sluiceElement)
        {
            _sluiceElement = sluiceElement;
        }
    }

    public class Card
    {
        private readonly XmlElement _cardElement;

        public int Index => int.Parse(_cardElement.GetAttribute("index"));
        public int EntryNumber => int.Parse(_cardElement.GetAttribute("eintragsnummer"));
        public string CardNumber
        {
            get => _cardElement.GetAttribute("kartennummer");
            set => _cardElement.SetAttribute("kartennummer", value);
        }

        public string Description
        {
            get => _cardElement.SelectSingleNode("Beschreibung")?.InnerText;
            set => _cardElement.SetInnerTextElement("Beschreibung", value);
        }

        public bool Door1
        {
            get => GetDoorEnabled(1);
            set => SetDoorEnabled(1, value);
        }
        public bool Door2
        {
            get => GetDoorEnabled(2);
            set => SetDoorEnabled(2, value);
        }
        public bool Door3
        {
            get => GetDoorEnabled(3);
            set => SetDoorEnabled(3, value);
        }
        public bool Door4
        {
            get => GetDoorEnabled(4);
            set => SetDoorEnabled(4, value);
        }
        public bool Door5
        {
            get => GetDoorEnabled(5);
            set => SetDoorEnabled(5, value);
        }
        public bool Door6
        {
            get => GetDoorEnabled(6);
            set => SetDoorEnabled(6, value);
        }
        public bool Door7
        {
            get => GetDoorEnabled(7);
            set => SetDoorEnabled(7, value);
        }
        public bool Door8
        {
            get => GetDoorEnabled(8);
            set => SetDoorEnabled(8, value);
        }

        public bool Sluice1
        {
            get => GetSluiceEnabled(1);
            set => SetSluiceEnabled(1, value);
        }
        public bool Sluice2
        {
            get => GetSluiceEnabled(2);
            set => SetSluiceEnabled(2, value);
        }
        public bool Sluice3
        {
            get => GetSluiceEnabled(3);
            set => SetSluiceEnabled(3, value);
        }
        public bool Sluice4
        {
            get => GetSluiceEnabled(4);
            set => SetSluiceEnabled(4, value);
        }

        public Card(XmlElement cardElement)
        {
            _cardElement = cardElement;
        }

        private bool GetDoorEnabled(int doorNumber)
        {
            if (doorNumber < 1 || doorNumber > 8)
                throw new ArgumentOutOfRangeException(nameof(doorNumber));

            return bool.TryParse(_cardElement.GetAttribute($"sk{doorNumber}"), out var enabled) && enabled;
        }

        private void SetDoorEnabled(int doorNumber, bool enabled)
        {
            if (doorNumber < 1 || doorNumber > 8)
                throw new ArgumentOutOfRangeException(nameof(doorNumber));

            _cardElement.SetAttribute($"sk{doorNumber}", enabled.ToString(CultureInfo.InvariantCulture));
        }

        private bool GetSluiceEnabled(int sluiceNumber)
        {
            if (sluiceNumber < 1 || sluiceNumber > 4)
                throw new ArgumentOutOfRangeException(nameof(sluiceNumber));

            return bool.TryParse(_cardElement.GetAttribute($"schleu{sluiceNumber}"), out var enabled) && enabled;
        }

        private void SetSluiceEnabled(int sluiceNumber, bool enabled)
        {
            if (sluiceNumber < 1 || sluiceNumber > 4)
                throw new ArgumentOutOfRangeException(nameof(sluiceNumber));

            _cardElement.SetAttribute($"schleu{sluiceNumber}", enabled.ToString(CultureInfo.InvariantCulture));
        }
    }

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
                _doors.Add(new Door(doorElement));
            }

            _sluices.Clear();
            // ReSharper disable once PossibleNullReferenceException
            foreach (XmlElement sluiceElement in _xml.SelectNodes("/DB/Schleusen/Schleuse"))
            {
                _sluices.Add(new Sluice(sluiceElement));
            }

            _cards.Clear();
            // ReSharper disable once PossibleNullReferenceException
            foreach (XmlElement cardElement in _xml.SelectNodes("/DB/Geraeteeintraege/Geraete/ECELMKartenEintraege/ECELMKartenEintrag"))
            {
                _cards.Add(new Card(cardElement));
            }
        }

        public void Save()
        {
            _xml.Save($"{ProjectFile.FullName}.2");
        }
    }

    public class CardWrapper
    {
        private readonly Card _card1, _card2, _card3;

        public int Index => _card1.Index;
        public int EntryNumber => _card1.EntryNumber;

        public string CardNumber
        {
            get => _card1.CardNumber;
            set
            {
                _card1.CardNumber = value; 
                _card2.CardNumber = value;
                _card3.CardNumber = value;
            }
        }

        public string Description
        {
            get => _card1.Description;
            set
            {
                _card1.Description = value;
                _card2.Description = value;
                _card3.Description = value;
            }
        }

        [UsedImplicitly]
        public bool Door1
        {
            get => _card1.Door1;
            set => _card1.Door1 = value;
        }
        [UsedImplicitly]
        public bool Door2
        {
            get => _card1.Door2;
            set => _card1.Door2 = value;
        }
        [UsedImplicitly]
        public bool Door3
        {
            get => _card1.Door3;
            set => _card1.Door3 = value;
        }
        [UsedImplicitly]
        public bool Door4
        {
            get => _card1.Door4;
            set => _card1.Door4 = value;
        }
        [UsedImplicitly]
        public bool Door5
        {
            get => _card1.Door5;
            set => _card1.Door5 = value;
        }
        [UsedImplicitly]
        public bool Door6
        {
            get => _card1.Door6;
            set => _card1.Door6 = value;
        }
        [UsedImplicitly]
        public bool Door7
        {
            get => _card1.Door7;
            set => _card1.Door7 = value;
        }
        [UsedImplicitly]
        public bool Door8
        {
            get => _card1.Door8;
            set => _card1.Door8 = value;
        }

        [UsedImplicitly]
        public bool Door9
        {
            get => _card2.Door1;
            set => _card2.Door1 = value;
        }
        [UsedImplicitly]
        public bool Door10
        {
            get => _card2.Door2;
            set => _card2.Door2 = value;
        }
        [UsedImplicitly]
        public bool Door11
        {
            get => _card2.Door3;
            set => _card2.Door3 = value;
        }
        [UsedImplicitly]
        public bool Door12
        {
            get => _card2.Door4;
            set => _card2.Door4 = value;
        }
        [UsedImplicitly]
        public bool Door13
        {
            get => _card2.Door5;
            set => _card2.Door5 = value;
        }
        [UsedImplicitly]
        public bool Door14
        {
            get => _card2.Door6;
            set => _card2.Door6 = value;
        }
        [UsedImplicitly]
        public bool Door15
        {
            get => _card2.Door7;
            set => _card2.Door7 = value;
        }
        [UsedImplicitly]
        public bool Door16
        {
            get => _card2.Door8;
            set => _card2.Door8 = value;
        }

        [UsedImplicitly]
        public bool Door17
        {
            get => _card3.Door1;
            set => _card3.Door1 = value;
        }
        [UsedImplicitly]
        public bool Door18
        {
            get => _card3.Door2;
            set => _card3.Door2 = value;
        }
        [UsedImplicitly]
        public bool Door19
        {
            get => _card3.Door3;
            set => _card3.Door3 = value;
        }
        [UsedImplicitly]
        public bool Door20
        {
            get => _card3.Door4;
            set => _card3.Door4 = value;
        }
        [UsedImplicitly]
        public bool Door21
        {
            get => _card3.Door5;
            set => _card3.Door5 = value;
        }
        [UsedImplicitly]
        public bool Door22
        {
            get => _card3.Door6;
            set => _card3.Door6 = value;
        }
        [UsedImplicitly]
        public bool Door23
        {
            get => _card3.Door7;
            set => _card3.Door7 = value;
        }
        [UsedImplicitly]
        public bool Door24
        {
            get => _card3.Door8;
            set => _card3.Door8 = value;
        }

        [UsedImplicitly]
        public bool Sluice1
        {
            get => _card1.Sluice1;
            set => _card1.Sluice1 = value;
        }
        [UsedImplicitly]
        public bool Sluice2
        {
            get => _card1.Sluice2;
            set => _card1.Sluice2 = value;
        }
        [UsedImplicitly]
        public bool Sluice3
        {
            get => _card1.Sluice3;
            set => _card1.Sluice3 = value;
        }
        [UsedImplicitly]
        public bool Sluice4
        {
            get => _card1.Sluice4;
            set => _card1.Sluice4 = value;
        }
        [UsedImplicitly]
        public bool Sluice5
        {
            get => _card2.Sluice1;
            set => _card2.Sluice1 = value;
        }
        [UsedImplicitly]
        public bool Sluice6
        {
            get => _card2.Sluice2;
            set => _card2.Sluice2 = value;
        }
        [UsedImplicitly]
        public bool Sluice7
        {
            get => _card2.Sluice3;
            set => _card2.Sluice3 = value;
        }
        [UsedImplicitly]
        public bool Sluice8
        {
            get => _card2.Sluice4;
            set => _card2.Sluice4 = value;
        }
        [UsedImplicitly]
        public bool Sluice9
        {
            get => _card3.Sluice1;
            set => _card3.Sluice1 = value;
        }
        [UsedImplicitly]
        public bool Sluice10
        {
            get => _card3.Sluice2;
            set => _card3.Sluice2 = value;
        }
        [UsedImplicitly]
        public bool Sluice11
        {
            get => _card3.Sluice3;
            set => _card3.Sluice3 = value;
        }
        [UsedImplicitly]
        public bool Sluice12
        {
            get => _card3.Sluice4;
            set => _card3.Sluice4 = value;
        }

        public CardWrapper(Card card1, Card card2, Card card3)
        {
            _card1 = card1;
            _card2 = card2;
            _card3 = card3;
        }

        public static string GetDoorPropertyName(int doorIndex)
        {
            if (doorIndex < 0 || doorIndex >= 8*3)
                throw new ArgumentOutOfRangeException(nameof(doorIndex));

            return $"Door{doorIndex + 1}";
        }

        public static string GetSluicePropertyName(int sluiceIndex)
        {
            if (sluiceIndex < 0 || sluiceIndex >= 4*3)
                throw new ArgumentOutOfRangeException(nameof(sluiceIndex));

            return $"Sluice{sluiceIndex + 1}";
        }
    }

    public class ProjectWrapper
    {
        private readonly List<Project> _projects = new List<Project>();
        private readonly List<FileInfo> _files = new List<FileInfo>();
        private readonly List<CardWrapper> _allCards = new List<CardWrapper>();
        public IReadOnlyList<FileInfo> Files => _files;

        public List<CardWrapper> AllCards => _allCards;

        public IReadOnlyList<Project> Projects => _projects;

        public ProjectWrapper(IEnumerable<FileInfo> projectFiles)
        {
            _files.AddRange(projectFiles);
        }

        public void LoadAll()
        {
            foreach (var file in _files)
            {
                var project = new Project(file);
                project.Load();
                _projects.Add(project);
            }


            foreach (var card1 in _projects.First().Cards)
            {
                var card2 = _projects[1].Cards.Single(c => c.CardNumber == card1.CardNumber);
                var card3 = _projects[2].Cards.Single(c => c.CardNumber == card1.CardNumber);
                var cardWrapper = new CardWrapper(card1, card2, card3);
                _allCards.Add(cardWrapper);
            }

        }

        

        public string GetDoorName(int doorIndex)
        {
            return GetDoor(doorIndex).Name;
        }

        public bool IsDoorEnabled(int doorIndex)
        {
            return GetDoor(doorIndex).Connected;
        }

        private Door GetDoor(int doorIndex)
        {
            if (doorIndex < 0 || doorIndex >= 8*3)
                throw new ArgumentOutOfRangeException(nameof(doorIndex));

            var project = _projects[doorIndex / 8];
            return project.Doors[doorIndex % 8];
        }

        public string GetSluiceName(int sluiceIndex)
        {
            return GetSluice(sluiceIndex).Name;
        }

        public bool IsSluiceEnabled(int sluiceIndex)
        {
            return GetSluice(sluiceIndex).Active;
        }

        private Sluice GetSluice(int sluiceIndex)
        {
            if (sluiceIndex < 0 || sluiceIndex >= 4 * 3)
                throw new ArgumentOutOfRangeException(nameof(sluiceIndex));

            var project = _projects[sluiceIndex / 4];
            return project.Sluices[sluiceIndex % 4];
        }

        public void SortDescription()
        {
            _allCards.Sort((a, b) => string.CompareOrdinal(a.Description, b.Description));
        }
    }
}
