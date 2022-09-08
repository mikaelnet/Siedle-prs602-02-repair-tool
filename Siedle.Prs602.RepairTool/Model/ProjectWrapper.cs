using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Siedle.Prs602.RepairTool.Model
{
    public enum CardValidationSeverity { Information, Warning, Error }

    public class CardValidationMessage
    {
        public CardValidationSeverity Severity { get; }
        public string Message { get; set; }

        public CardValidationMessage(CardValidationSeverity severity, string message)
        {
            Severity = severity;
            Message = message;
        }

        public static CardValidationMessage Error(string message)
        {
            return new CardValidationMessage(CardValidationSeverity.Error, message);
        }

        public static CardValidationMessage Warning(string message)
        {
            return new CardValidationMessage(CardValidationSeverity.Warning, message);
        }

        public static CardValidationMessage Info(string message)
        {
            return new CardValidationMessage(CardValidationSeverity.Information, message);
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

        public IEnumerable<CardValidationMessage> LoadAll()
        {
            foreach (var file in _files)
            {
                var project = new Project(file);
                project.Load();
                _projects.Add(project);

                if (project.Cards.Count == 0)
                {
                    yield return CardValidationMessage.Error($"No tags found in {file}");
                }
                if (project.Doors.Count == 0)
                {
                    yield return CardValidationMessage.Error($"No tags found in {file}");
                }
            }

            // Check that all cards are unique in each file
            bool duplicates = false;
            foreach (var project in _projects)
            {
                var duplicateCards = project.Cards
                    .GroupBy(c => c.CardNumber)
                    .Where(g => g.Skip(1).Any())
                    .SelectMany(c => c);
                foreach (var duplicateCard in duplicateCards)
                {
                    yield return CardValidationMessage.Error($"Duplicates of tag {duplicateCard.CardNumber} found in {project.ProjectFile}");
                    duplicates = true;
                }

                var duplicateIndexes = project.Cards
                    .GroupBy(c => c.Index)
                    .Where(g => g.Skip(1).Any())
                    .SelectMany(c => c);
                foreach (var duplicateCard in duplicateIndexes)
                {
                    yield return CardValidationMessage.Error($"Duplicate tag indexes found {duplicateCard.Index} in {project.ProjectFile}");
                    duplicates = true;
                }

                var duplicateEntryNumber = project.Cards
                    .GroupBy(c => c.EntryNumber)
                    .Where(g => g.Skip(1).Any())
                    .SelectMany(c => c);
                foreach (var duplicateCard in duplicateEntryNumber)
                {
                    yield return CardValidationMessage.Error($"Duplicate tag entry numbers found {duplicateCard.EntryNumber} in {project.ProjectFile}");
                    duplicates = true;
                }
            }

            if (duplicates)
            {
                yield break;
            }

            // Test that all tags are in all projects. We may want to auto-create tags in project 2/3.
            var project1Cards = new HashSet<string>(_projects.First().Cards.Select(c => c.CardNumber));
            foreach (var project in _projects.Skip(1))
            {
                var extraCards = project.Cards.Where(c => !project1Cards.Contains(c.CardNumber));
                foreach (var extraCard in extraCards)
                {
                    yield return CardValidationMessage.Warning($"Extra card {extraCard.CardNumber} in project {project.ProjectFile}");
                }

                foreach (var card1 in _projects.First().Cards)
                {
                    if (project.Cards.All(c => c.CardNumber != card1.CardNumber))
                    {
                        project.AddCard(card1.Index, card1.EntryNumber, card1.CardNumber, card1.Description);
                        yield return CardValidationMessage.Warning($"Card {card1.CardNumber} missing in project {project.ProjectFile}. Adding it");
                    }
                }
            }

            int cardIndex = 1;
            foreach (var card1 in _projects.First().Cards)
            {
                var card2 = _projects[1].Cards.Single(c => c.CardNumber == card1.CardNumber);
                var card3 = _projects[2].Cards.Single(c => c.CardNumber == card1.CardNumber);
                var cardWrapper = new CardWrapper(card1, card2, card3);

                if (card1.Index != card2.Index || card1.Index != card3.Index)
                    yield return CardValidationMessage.Error($"Tag {card1.CardNumber} does not have the same index in all project files: {card1.Index}, {card2.Index}, {card3.Index}");
                if (card1.EntryNumber != card2.EntryNumber || card1.EntryNumber != card3.EntryNumber)
                    yield return CardValidationMessage.Error($"Tag {card1.CardNumber} does not have the same entry number in all project files: {card1.EntryNumber}, {card2.EntryNumber}, {card3.EntryNumber}");

                if (string.IsNullOrWhiteSpace(card2.Description) || string.IsNullOrWhiteSpace(card3.Description))
                {
                    card3.Description = card2.Description = card1.Description;
                    yield return CardValidationMessage.Info($"Fixing missing description of {card1.CardNumber}");
                }
                else if (!string.Equals(card1.Description, card2.Description, StringComparison.OrdinalIgnoreCase) ||
                         !string.Equals(card1.Description, card3.Description, StringComparison.OrdinalIgnoreCase))
                {
                    card3.Description = card2.Description = card1.Description;
                    yield return CardValidationMessage.Warning($"Repairing description of {card1.CardNumber}");
                }

                if (card1.Index != cardIndex)
                {
                    card1.ReIndex(cardIndex);
                    card2.ReIndex(cardIndex);
                    card3.ReIndex(cardIndex);
                }

                cardIndex++;
                _allCards.Add(cardWrapper);
            }

        }

        public void ReOrderDoorTags()
        {

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