using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC_2015
{
    public class Day_06 : BaseDay
    {
        private readonly ICollection<Instruction> _input;

        public Day_06()
        {
            _input = ParseInput().ToList();
        }

        public override string Solve_1()
        {
            var lights = new Dictionary<Point, bool>(1_000_000);

            foreach (var x in Enumerable.Range(0, 1000))
            {
                foreach (var y in Enumerable.Range(0, 1000))
                {
                    lights[new Point(x, y)] = false;
                }
            }

            foreach (var instruction in _input)
            {
                foreach (var point in ExtractRectangle(instruction.AffectedPointsRange.A, instruction.AffectedPointsRange.B))
                {
                    lights[point] = instruction.Type switch
                    {
                        InstructionType.TurnOn => true,
                        InstructionType.TurnOff => false,
                        InstructionType.Toggle => !lights[point],
                        _ => throw new SolvingException()
                    };
                }
            }

            return lights.Count(l => l.Value).ToString();
        }

        public override string Solve_2()
        {
            var lights = new Dictionary<Point, int>(1_000_000);

            var range = Enumerable.Range(0, 1000);
            foreach (var x in range)
            {
                foreach (var y in range)
                {
                    lights[new Point(x, y)] = 0;
                }
            }

            foreach (var instruction in _input)
            {
                foreach (var point in ExtractRectangle(instruction.AffectedPointsRange.A, instruction.AffectedPointsRange.B))
                {
                    lights[point] = instruction.Type switch
                    {
                        InstructionType.TurnOn => lights[point] + 1,
                        InstructionType.TurnOff => lights[point] > 0 ? lights[point] - 1 : 0,
                        InstructionType.Toggle => lights[point] + 2,
                        _ => throw new SolvingException()
                    };
                }
            }

            return lights.Sum(l => l.Value).ToString();
        }

        /// <summary>
        /// Overall it's way cheaper instantiate new Points than looking into
        /// a 1_000_000 instances dictionary for points to retrieve an existing instance
        /// and avoid allocating extra memory.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static IEnumerable<Point> ExtractRectangle(Point a, Point b)
        {
            var xRange = Enumerable.Range(
                Convert.ToInt32(new[] { a, b }.Min(p => p.X)),
                Convert.ToInt32(Math.Abs(a.X - b.X) + 1));

            var yRange = Enumerable.Range(
                Convert.ToInt32(new[] { a, b }.Min(p => p.Y)),
                Convert.ToInt32(Math.Abs(a.Y - b.Y) + 1));

            foreach (var x in xRange)
            {
                foreach (var y in yRange)
                {
                    yield return new Point(x, y);
                }
            }
        }

        private IEnumerable<Instruction> ParseInput()
        {
            foreach (var line in File.ReadAllLines(InputFilePath))
            {
                (string order, int aStart) = line.StartsWith("turn", StringComparison.OrdinalIgnoreCase)
                    ? (line[0..8], 8)
                    : ("toggle", 7);

                var index = line.IndexOf("through");
                string pointA = line[aStart..index];
                string pointB = line[(index + "through".Length)..];

                var a = pointA.Split(',');
                var b = pointB.Split(',');

                yield return new Instruction
                {
                    Type = (InstructionType)Enum.Parse(typeof(InstructionType), order.Replace(" ", ""), ignoreCase: true),
                    AffectedPointsRange = (
                        new Point(int.Parse(a[0].Trim()), int.Parse(a[1].Trim())),
                        new Point(int.Parse(b[0].Trim()), int.Parse(b[1].Trim())))
                };
            }
        }

        private enum InstructionType
        {
            TurnOn = 0,
            TurnOff = 1,
            Toggle = 2
        }

        private class Instruction
        {
            public InstructionType Type { get; set; }

            public (Point A, Point B) AffectedPointsRange { get; set; }
        }

        private record Point(int X, int Y);
    }
}
