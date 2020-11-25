using AoCHelper;
using System.IO;
using System.Linq;

namespace AoC_2015
{
    public class Day_01 : BaseDay
    {
        public override string Solve_1()
        {
            var input = ParseInput();

            var solution = input.Aggregate(
                0,
                (int total, char item) => total + ChangeFloorLevel(item));

            return solution.ToString();
        }

        public override string Solve_2()
        {
            var input = ParseInput();

            var floor = 0;
            for (int i = 0; i < input.Length; ++i)
            {
                floor += ChangeFloorLevel(input[i]);

                if (floor < 0)
                {
                    return (i + 1).ToString();
                }
            }

            throw new SolvingException();
        }

        private static int ChangeFloorLevel(char item) => item == '(' ? 1 : -1;

        private string ParseInput()
        {
            return File.ReadAllText(InputFilePath);
        }
    }
}
