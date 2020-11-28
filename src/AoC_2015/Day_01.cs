using AoCHelper;
using System.IO;
using System.Linq;

namespace AoC_2015
{
    public class Day_01 : BaseDay
    {
        private readonly string _input;

        public Day_01()
        {
            _input = ParseInput();
        }
        public override string Solve_1()
        {
            var solution = _input.Aggregate(
                0,
                (int total, char item) => total + ChangeFloorLevel(item));

            return solution.ToString();
        }

        public override string Solve_2()
        {
            var floor = 0;
            for (int i = 0; i < _input.Length; ++i)
            {
                floor += ChangeFloorLevel(_input[i]);

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
