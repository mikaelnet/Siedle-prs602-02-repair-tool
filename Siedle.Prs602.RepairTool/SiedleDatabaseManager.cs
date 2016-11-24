using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Siedle.Prs602.RepairTool
{
    public class SiedleDatabaseManager
    {
        private readonly TextWriter _logger;
        private XDocument[] _xDocuments;
        private string[] _fileNames;
        private List<EcCard> _allCards;


        public SiedleDatabaseManager(TextWriter logger)
        {
            _logger = logger;
        }

        public void LoadDocuments(FileInfo projectFile1, FileInfo projectFile2, FileInfo projectFile3)
        {
            // Load all documents
            _fileNames = new[] { projectFile1.FullName, projectFile2.FullName, projectFile3.FullName };
            _xDocuments = new XDocument[3];
            _allCards = new List<EcCard>();
            var projects = new HashSet<int>();
            for (int i=0 ; i < _xDocuments.Length; i ++)
            {
                _xDocuments[i] = XDocument.Load(_fileNames[i]);
                int projectId = EcCard.GetProjectNumber(_xDocuments[i]);
                if (!EcCard.ProjectIds.Contains(projectId))
                    throw new Exception($"Project {i+1} has an invalid Project id of {projectId}");
                if (projects.Contains(projectId))
                    throw new Exception($"Project {projectId} already loaded");
                projects.Add(projectId);
                _allCards.AddRange(EcCard.LoadAll(_xDocuments[i]));
            }
        }

        public void CommitChanges()
        {
            _logger.WriteLine("Testing consistency...");
            if (TestConsistency())
            {
                _logger.WriteLine("Can't save broken database");
                return;
            }

            _logger.WriteLine("Saving...");

            for (int i = 0; i < _xDocuments.Length; i++)
            {
                int projectId = EcCard.GetProjectNumber(_xDocuments[i]);
                EcCard.UpdateAll(_xDocuments[i], _allCards.Where(c => c.ProjectNumber == projectId).OrderBy(c => c.ID));
            }

            // Make backups
            foreach (var fileName in _fileNames)
            {
                File.Delete($"{fileName}.5");
            }
            for (int i = 5; i > 0; i --)
            {
                foreach (var fileName in _fileNames)
                {
                    var dest = $"{fileName}.{i}";
                    var src = fileName;
                    if (i > 1)
                        src += "." + (i - 1);

                    if (File.Exists(src))
                        File.Move(src, dest);
                }
            }

            for (int i = 0; i < _xDocuments.Length; i++)
                _xDocuments[i].Save(_fileNames[i]);
        }

        private bool TestConsistency()
        {
            bool isCorrupt = false;
            // Ensure all indexes, numbers and identities are unique
            foreach (int projectId in EcCard.ProjectIds)
            {
                var indexHash = new HashSet<int>();
                var cardHash = new HashSet<int>();
                var identityHash = new HashSet<string>();

                foreach (var card in _allCards.Where(c => c.ProjectNumber == projectId))
                {
                    if (indexHash.Add(card.ID) == false)
                    {
                        _logger.WriteLine($"ID {card.ID} already exist in project {projectId}");
                        isCorrupt = true;
                    }
                    if (cardHash.Add(card.CardNumber) == false)
                    {
                        _logger.WriteLine($"CardNumber {card.CardNumber} already exist in project {projectId}");
                        isCorrupt = true;
                    }
                    if (identityHash.Add(card.CardIdentity) == false)
                    {
                        _logger.WriteLine($"Card {card.CardIdentity} already exist in project {projectId}");
                        isCorrupt = true;
                    }
                }
            }
            return isCorrupt;
        }

        public bool SanitizeInput()
        {
            _logger.WriteLine("Sanitizing input...");

            foreach (var card in _allCards.Where(c => c.Text != null && c.Text != c.Text.Trim()))
            {
                _logger.WriteLine("{0}", card);
                card.Text = card.Text.Trim();
            }

            var isCorrupt = TestConsistency();
            if (isCorrupt)
                _logger.WriteLine("Database is corrupt. Can't continue. This must be fixed manually.");

            _logger.WriteLine("Renumbering...");
            foreach (int projectId in EcCard.ProjectIds)
            {
                foreach (var card in _allCards.Where(c => c.ProjectNumber == projectId))
                {
                    card.ID = card.CardNumber;
                }
            }

            return isCorrupt;
        }

        public void TestFlagsValidity()
        {
            _logger.WriteLine("Testing card validity...");
            foreach (var card in _allCards.Where(c => !c.IsFlagsValid))
            {
                _logger.WriteLine("{0}", card);
                _logger.Write("     flags:");
                foreach (var flag in card.Flags)
                {
                    _logger.Write(" {0}", flag ? "1" : "0");
                }
                _logger.Write("  expected:");
                foreach (var flag in EcCard.ExpectedFlags(card.ProjectNumber))
                {
                    _logger.Write(" {0}", flag ? "1" : "0");
                }
                card.ClearSurplusFlags();

                _logger.WriteLine();
            }
        }

        public void CreateMissingCards()
        {
            _logger.WriteLine("Creating missing cards...");

            var cardIds = new HashSet<string>();
            foreach (var c in _allCards)
                cardIds.Add(c.CardIdentity);

            foreach (var id in cardIds)
            {
                bool unique = true;
                var cards = _allCards.Where(c => string.Equals(c.CardIdentity, id)).ToList();
                if (cards.Count != 3)
                {
                    _logger.WriteLine("{0} have {1} entries", id, cards.Count);
                }

                if (cards.Any(c => c.CardNumber != cards[0].CardNumber))
                {
                    unique = false;
                    _logger.WriteLine("{0} have different card numbers: {1}", id, string.Join(", ", cards.Select(c => c.CardNumber)));
                }

                if (unique && cards.Count < 3)
                {
                    foreach (var projectId in EcCard.ProjectIds)
                    {
                        if (cards.Any(c => c.ProjectNumber == projectId))
                            continue;

                        int newId;
                        if (!_allCards.Any(c => c.ProjectNumber == projectId && c.ID == cards[0].CardNumber))
                            newId = cards[0].CardNumber;
                        else
                            newId = _allCards.Where(c => c.ProjectNumber == projectId).Max(c => c.ID) + 1;
                        var card = EcCard.Create(newId, projectId, 
                            cards[0].CardNumber, cards[0].CardIdentity);
                        card.Text = cards[0].Text;
                        card.SetFlags(cards[0].IsActive);
                        _allCards.Add(card);
                        _logger.WriteLine("Creating new card {0}", card);
                    }
                }
            }
        }

        public void FixDescriptionTexts()
        {
            _logger.WriteLine("Fixing card text comments...");
            var masterCards = _allCards.Where(c => c.ProjectNumber == EcCard.ProjectIds[0]).ToList();
            var slaveCards = _allCards.Where(c => c.ProjectNumber != EcCard.ProjectIds[0]).ToList();

            _logger.WriteLine("Master unique cards:");
            foreach (var card in masterCards.Where(m => !slaveCards.Any(s => string.Equals(s.CardIdentity, m.CardIdentity))))
            {
                _logger.WriteLine(card);
            }
            _logger.WriteLine();

            _logger.WriteLine("Slave unique cards:");
            foreach (var card in slaveCards.Where(s => !masterCards.Any(m => string.Equals(s.CardIdentity, m.CardIdentity))))
            {
                _logger.WriteLine(card);
            }
            _logger.WriteLine();

            foreach (var masterCard in masterCards.Where(m => !string.IsNullOrWhiteSpace(m.Text)))
            {
                var identity = masterCard.CardIdentity;
                var slaves = slaveCards.Where(s => string.Equals(s.CardIdentity, identity)).ToList();
                if (slaves.Any(s => !string.Equals(s.Text, masterCard.Text) && !string.IsNullOrWhiteSpace(s.Text)))
                {
                    _logger.WriteLine("different texts:");
                    _logger.WriteLine("\t" + masterCard);
                    foreach (var slave in slaves)
                    {
                        _logger.WriteLine("\t" + slave);
                    }
                    continue;
                }

                foreach (var slave in slaves.Where(s => string.IsNullOrWhiteSpace(s.Text)))
                {
                    _logger.WriteLine("Copying '{0}' to card '{1}' in project {2}", masterCard.Text, slave.CardIdentity, slave.ProjectNumber);
                    slave.Text = masterCard.Text;
                }
            }
        }

        public void FindNumberingHoles()
        {
            _logger.WriteLine("Finding card numbering holes...");
            int cardIndex = 0;
            foreach (var card in _allCards.OrderBy(c => c.CardNumber))
            {
                if (card.CardNumber > cardIndex + 1)
                {
                    _logger.WriteLine("No card(s) at position {0}-{1}", cardIndex + 1, card.CardNumber);
                }
                cardIndex = card.CardNumber;
            }
        }
    }
}
