using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;


namespace Siedle.Prs602.RepairTool
{
    public class EcCard
    {
        public int ID { get; set; }
        public int ProjectNumber { get; set; }
        public int CardNumber { get; set; }
        public string CardIdentity { get; set; }
        public string Text { get; set; }

        private readonly bool[] _flags;

        public static readonly string[] FlagNames = {
            "sk1", "sk2", "sk3", "sk4", "sk5", "sk6", "sk7", "sk8",
            "schleu1", "schleu2", "schleu3", "schleu4", "sperre"
        };

        public static readonly int[] ProjectIds = {1, 2, 3};

        private static readonly bool[,] ProjectFlags;

        static EcCard()
        {
            ProjectFlags = new bool[ProjectIds.Length,FlagNames.Length];

            // Project 1: sk8, schleu3, schleu4, sperre false
            // Project 2: sk6, sk7, sk8, schleu3, schleu4, sperre false
            // Project 3: sk8, sperre false
            // All other should be true when active
            for (int i = 0; i < ProjectIds.Length; i++)
            {
                for (int j = 0; j < FlagNames.Length; j++)
                {
                    var name = FlagNames[j];
                    var proj = ProjectIds[i];
                    if ((name == "sk6" || name == "sk7") && (proj == 2))
                    {
                        ProjectFlags[i, j] = false;
                    }
                    else if (name == "sk8" || name == "sperre")
                    {
                        ProjectFlags[i, j] = false;
                    }
                    else if ((name == "schleu3" || name == "schleu4") && (proj == 1 || proj == 2))
                    {
                        ProjectFlags[i, j] = false;
                    }
                    else
                    {
                        ProjectFlags[i, j] = true;
                    }
                }
            }

            for (int i = 0; i < ProjectIds.Length; i++)
            {
                ProjectFlags[i, 7] = false;
                ProjectFlags[i, 12] = false;
            }
            for (int i = 0; i < 2; i++)
            {
                ProjectFlags[i, 7] = false;
                ProjectFlags[i, 12] = false;
            }
        }

        private EcCard()
        {
            _flags = new bool[FlagNames.Length];
        }

        public static EcCard Create(int id, int projectNumber, int cardNumber, string cardIdentity)
        {
            var card = new EcCard();
            card.ID = id;
            card.ProjectNumber = projectNumber;
            card.CardNumber = cardNumber;
            card.CardIdentity = cardIdentity;
            return card;
        }

        private static int GetProjectIndex(int projectNumber)
        {
            var projectIndex = -1;
            for (int i = 0; i < ProjectIds.Length; i++)
            {
                if (ProjectIds[i] == projectNumber)
                {
                    projectIndex = i;
                    break;
                }
            }
            if (projectIndex < 0 || projectIndex > ProjectIds.Length)
                throw new ArgumentException("Invalid project number " + projectNumber);

            return projectIndex;
        }

        public bool IsFlagsValid
        {
            get
            {
                if (_flags.All(f => f == false))
                    return true;
                var projectIndex = GetProjectIndex (ProjectNumber);

                return !_flags.Where((t, i) => t != ProjectFlags[projectIndex, i]).Any();
            }
        }

        public bool IsActive
        {
            get
            {
                return _flags.Any(f => f);
            }
        }

        public IEnumerable<bool> Flags => _flags;

        public static IEnumerable<bool> ExpectedFlags(int projectNumber)
        {
            int projectIndex = GetProjectIndex(projectNumber);
            for (int i = 0; i < FlagNames.Length; i ++)
            {
                yield return ProjectFlags[projectIndex, i];
            }
        }

        public void ClearSurplusFlags()
        {
            var expectedFlags = ExpectedFlags(ProjectNumber).ToList();
            for (int i = 0; i < _flags.Length; i ++)
            {
                if (expectedFlags[i] == false)
                    _flags[i] = false;
            }
        }

        public void SetFlags(bool state)
        {
            if (state)
            {
                var projectIndex = ProjectNumber - 2;
                if (projectIndex < 0 || projectIndex > 2)
                    throw new ArgumentException("Invalid project number " + ProjectNumber);

                for (int i = 0; i < _flags.Length; i++)
                    _flags[i] = ProjectFlags[projectIndex, i];
            }
            else
            {
                for (int i = 0; i < _flags.Length; i++)
                    _flags[i] = false;
            }
        }

        private static XElement GetCardRoot(XDocument xDocument)
        {
            return xDocument?
                .Element("DB")?
                .Element("Geraeteeintraege")?
                .Element("Geraete")?
                .Element("ECELMKartenEintraege");
        }

        private static IEnumerable<XElement> GetCardList(XElement cardRoot)
        {
            return cardRoot?.Elements("ECELMKartenEintrag") ?? Enumerable.Empty<XElement>();
        }

        private static IEnumerable<XElement> GetCardList(XDocument xDocument)
        {
            var root = GetCardRoot(xDocument);
            return GetCardList(root);
        }

        public static int GetProjectNumber(XDocument xDocument)
        {
            var idElement = xDocument?
                .Element("DB")?
                .Element("ProjektKopfdaten")?
                .Element("Projekt")?
                .Attribute("id");
            return idElement != null ? (int) idElement : 0;
        }

        private static EcCard LoadFromElement(XElement xCardElement)
        {
            if (xCardElement == null)
                return null;

            var card = new EcCard();
            card.ID = (int)xCardElement.Attribute("index");
            card.ProjectNumber = GetProjectNumber(xCardElement.Document);
            card.CardNumber = (int) xCardElement.Attribute("eintragsnummer");
            card.CardIdentity = (string) xCardElement.Attribute("kartennummer");
            card.Text = xCardElement.Element("Beschreibung")?.Value;
            for (int i = 0; i < FlagNames.Length; i++)
            {
                card._flags[i] = (bool) xCardElement.Attribute(FlagNames[i]);
            }
            return card;
        }

        public static IEnumerable<EcCard> LoadAll(XDocument xDocument)
        {
            var xCardList = GetCardList(xDocument);
            if (xCardList == null)
                yield break;

            foreach (XElement xCardElement in xCardList)
            {
                var card = LoadFromElement(xCardElement);
                if (card != null)
                    yield return card;
            }
        }

        private XElement CreateXElement()
        {
            /* <ECELMKartenEintrag index="2" eintragsnummer="4" kartennummer="08000400051508020609" sk1="True" sk2="True" sk3="True" sk4="True" sk5="True" sk6="True" sk7="True" sk8="False" schleu1="True" schleu2="True" schleu3="False" schleu4="False" sperre="False"> 
             <Beschreibung>1650:fs-nyckel</Beschreibung>
             </ECELMKartenEintrag>
             */
            var xe = new XElement("ECELMKartenEintrag", 
                new XAttribute("index", ID),
                new XAttribute("eintragsnummer", CardNumber),
                new XAttribute("kartennummer", CardIdentity),
                new XElement("Beschreibung", Text));
            for (var i=0 ; i < FlagNames.Length ; i ++)
            {
                xe.SetAttributeValue(FlagNames[i], _flags[i].ToString());
            }
            
            return xe;
        }

        public static void UpdateAll(XDocument xDocument, IEnumerable<EcCard> cards)
        {
            var xCardRoot = GetCardRoot(xDocument);
            var newNodes = cards.Select(c => c.CreateXElement());
            xCardRoot.ReplaceAll(newNodes);
        }

        /*public bool Update(XDocument xDocument)
        {
            var xCardList = GetCardList(xDocument);
            if (xCardList == null)
                return false;

            if (ID > 0)
            {
                var xCardElement = xCardList;

            }

            var command = new OleDbCommand(ID > 0 ? UpdateCommand : InsertCommand, connection);
            command.Parameters.Add(new OleDbParameter("@CustomerNumber", CustomerNumber));
            command.Parameters.Add(new OleDbParameter("@ProjectNumber", ProjectNumber));
            command.Parameters.Add(new OleDbParameter("@CardNumber", CardNumber));
            command.Parameters.Add(new OleDbParameter("@CardIdentity", CardIdentity));
            command.Parameters.Add(new OleDbParameter("@Text", Text));
            for (int i = 0 ; i < FlagNames.Length ; i ++) 
            {
                command.Parameters.Add(new OleDbParameter($"@{FlagNames[i]}", _flags[i]));
            }

            if (ID > 0)
            {
                command.Parameters.Add(new OleDbParameter("@ID", ID));
            }

            command.ExecuteNonQuery();
        }*/

        public void Delete(XDocument xDocument)
        {
            if (ID > 0)
            {
                var xCardRoot = GetCardRoot(xDocument);
                var xCard = (from card in xCardRoot.Elements("ECELMKartenEintrag")
                    where card.Attribute("index")?.Value == ID.ToString(CultureInfo.InvariantCulture) 
                    select card).SingleOrDefault();
                xCard?.Remove();
            }
        }

        public override string ToString()
        {
            return string.Join("\t", ProjectNumber, CardNumber, $"({ID})", CardIdentity, Text);
        }
    }
}