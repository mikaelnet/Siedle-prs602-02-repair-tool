using System;
using JetBrains.Annotations;

namespace Siedle.Prs602.RepairTool.Model
{
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
        public string PrintedNumber => _card1.PrintedNumber;
        
        [UsedImplicitly]
        public string BelongsTo => _card1.BelongsTo;

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
}