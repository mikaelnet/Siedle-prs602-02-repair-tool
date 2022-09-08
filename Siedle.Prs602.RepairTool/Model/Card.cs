using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml;

namespace Siedle.Prs602.RepairTool.Model
{
    public class Card
    {
        private static readonly Regex PrintedNumberRegex = new Regex(@"^([0-9]{4}):(.+)$");
        private readonly XmlElement _cardElement;
        public Project Owner { get; }

        public int Index
        {
            get => int.Parse(_cardElement.GetAttribute("index"));
            private set => _cardElement.SetAttribute("index", value.ToString());
        }

        public int EntryNumber
        {
            get => int.Parse(_cardElement.GetAttribute("eintragsnummer"));
            private set => _cardElement.SetAttribute("eintragsnummer", value.ToString());
        }

        public string CardNumber
        {
            get => _cardElement.GetAttribute("kartennummer");
            set => _cardElement.SetAttribute("kartennummer", value);
        }

        private bool _descriptionParsed = false;
        private string _printedNumber = null;
        private string _belongsTo = null;

        public string Description
        {
            get => _cardElement.SelectSingleNode("Beschreibung")?.InnerText;
            set { 
                _cardElement.SetInnerTextElement("Beschreibung", value);
                _descriptionParsed = false;
            }
        }

        public string PrintedNumber
        {
            get
            {
                ParseDescription();
                return _printedNumber;
            }
        }

        public string BelongsTo
        {
            get
            {
                ParseDescription();
                return _belongsTo;
            }
        }

        private void ParseDescription()
        {
            if (_descriptionParsed)
                return;

            var match = PrintedNumberRegex.Match(Description);
            if (match.Success)
            {
                _printedNumber = match.Groups[1].Value;
                _belongsTo = match.Groups[2].Value;
            }
            else
            {
                _printedNumber = null;
                _belongsTo = null;
            }
            _descriptionParsed = true;
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

        public Card(XmlElement cardElement, Project owner)
        {
            _cardElement = cardElement;
            Owner = owner;
        }

        public void ReIndex(int newIndex)
        {
            Index = newIndex;
            EntryNumber = newIndex;
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
}