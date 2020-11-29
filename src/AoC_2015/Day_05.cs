using AoCHelper;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC_2015
{
    public class Day_05 : BaseDay
    {
        private readonly List<string> _input;
        private readonly ICollection<char> _vowels = new[] { 'a', 'e', 'i', 'o', 'u' };
        private readonly ICollection<string> _forbiddenStrings = new[] { "ab", "cd", "pq", "xy" };

        public Day_05()
        {
            _input = File.ReadAllLines(InputFilePath).Where(l => !string.IsNullOrWhiteSpace(l)).Select(l => l.ToLowerInvariant()).ToList();
        }

        public override string Solve_1()
        {
            return _input.Count(line =>
                !_forbiddenStrings.Any(forbidden => line.Contains(forbidden))
                && line.Count(ch => _vowels.Contains(ch)) >= 3
                && HasDoubleLetters(line))
            .ToString();
        }

        /// <summary>
        /// Wrong: 19, 20, 33, 35, 48, 41, 50, 429
        /// </summary>
        /// <returns></returns>
        public override string Solve_2()
        {
            return _input.Count(line => IsNicePart2(line))
                .ToString();
        }

        private static bool HasDoubleLetters(string line)
        {
            var previousLetter = line[0];
            for (int i = 1; i < line.Length; ++i)
            {
                var currentLetter = line[i];
                if (previousLetter == currentLetter)
                {
                    return true;
                }

                previousLetter = currentLetter;
            }

            return false;

        }

        private static bool IsNicePart2(string line)
        {
            bool hasRepeatedPairs = false;
            bool hasRepeatedLetterWithOneInTheMiddle = false;

            var pairs = new HashSet<(char letter1, char letter2)>();

            var previousLetter = line[0];
            var previousPreviousLetter = '\t';
            var previousPreviousPreviousLetter = '\n';
            for (int i = 1; i < line.Length; ++i)
            {
                var currentLetter = line[i];

                if (!hasRepeatedPairs && !pairs.Add((previousLetter, currentLetter)) //&& overLap.Contains(currentLetter))
                    && (previousLetter != currentLetter
                        || (previousPreviousLetter != currentLetter && (i == line.Length - 1 || currentLetter != line[i + 1]))          // Prevents AAA
                        || (previousPreviousLetter == previousLetter && previousPreviousPreviousLetter == previousPreviousLetter)))     // Allows   AAAA
                {
                    hasRepeatedPairs = true;
                }

                if (!hasRepeatedLetterWithOneInTheMiddle && currentLetter == previousPreviousLetter)
                {
                    hasRepeatedLetterWithOneInTheMiddle = true;
                }

                previousPreviousPreviousLetter = previousPreviousLetter;
                previousPreviousLetter = previousLetter;
                previousLetter = currentLetter;
            }

            return hasRepeatedPairs && hasRepeatedLetterWithOneInTheMiddle;
        }
    }
}
