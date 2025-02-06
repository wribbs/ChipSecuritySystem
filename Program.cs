using System;
using System.Collections.Generic;

namespace ChipSecuritySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            int chipCount = new Random().Next(2, 21);
            var chips = GenerateRandomChips(chipCount);

            Console.WriteLine($"{chipCount} chips generated:");

            foreach (var chip in chips)
            {
                Console.WriteLine(chip);
            }
            Console.WriteLine();

            var maxSequence = FindChipSequence(chips);

            if (maxSequence != null)
            {
                Console.WriteLine($"{maxSequence.Length} chip sequence solution found:");
                foreach (var chip in maxSequence)
                {
                    Console.WriteLine(chip);
                }
            }
            else
            {
                Console.WriteLine(Constants.ErrorMessage);
            }
        }

        public static ColorChip[] GenerateRandomChips(int chipCount)
        {
            var colors = Enum.GetValues(typeof(Color));
            var random = new Random();
            var chips = new ColorChip[chipCount];

            for (int i = 0; i < chipCount; i++)
            {
                Color startColor;
                Color endColor;
                do
                {
                    startColor = (Color)colors.GetValue(random.Next(colors.Length));
                    endColor = (Color)colors.GetValue(random.Next(colors.Length));
                } while (startColor == endColor);

                chips[i] = new ColorChip(startColor, endColor);
            }

            return chips;
        }

        public static ColorChip[] FindChipSequence(ColorChip[] chips)
        {
            var bestSequence = new List<ColorChip>();
            var currentSequence = new List<ColorChip>();

            for (int i = 0; i < chips.Length; i++)
            {
                if (chips[i].StartColor == Color.Blue)
                {
                    FindSequence(chips, chips[i], currentSequence, bestSequence);
                }
            }

            return bestSequence.Count > 0 ? bestSequence.ToArray() : null;
        }

        private static void FindSequence(ColorChip[] chips, ColorChip currentChip, List<ColorChip> currentSequence, List<ColorChip> bestSequence)
        {
            currentSequence.Add(currentChip);

            if (currentChip.EndColor == Color.Green)
            {
                if (currentSequence.Count > bestSequence.Count)
                {
                    bestSequence.Clear();
                    bestSequence.AddRange(currentSequence);
                }
            }
            else
            {
                for (int i = 0; i < chips.Length; i++)
                {
                    if (!currentSequence.Contains(chips[i]) && chips[i].StartColor == currentChip.EndColor)
                    {
                        FindSequence(chips, chips[i], currentSequence, bestSequence);
                    }
                }
            }

            currentSequence.Remove(currentChip);
        }
    }
}
