using AoCHelper;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC_2015
{
    public class Day_02 : BaseDay
    {
        public override string Solve_1()
        {
            var input = ParseInput();

            var solution = 0;

            foreach (var p in input)
            {
                solution += CalculatePaper(p);
            }

            return solution.ToString();
        }

        public override string Solve_2()
        {
            var input = ParseInput();

            var solution = 0;

            foreach (var p in input)
            {
                solution += CalculateRibbon(p);
            }

            return solution.ToString();
        }

        private static int CalculatePaper((int l, int w, int h) present)
        {
            var a = present.h * present.l;
            var b = present.h * present.w;
            var c = present.l * present.w;
            var min = new[] { a, b, c }.Min();

            return (2 * a) + (2 * b) + (2 * c) + min;
        }

        private static int CalculateRibbon((int l, int w, int h) present)
        {
            var a = present.h + present.l;
            var b = present.h + present.w;
            var c = present.l + present.w;
            var min = new[] { a, b, c }.Min();

            return (2 * min)                                // Min perimeter
                + (present.l * present.w * present.h);      // Volume
        }

        /// <summary>
        /// Optimized way of calculating the aggregation of paper and ribbon,
        /// given that the side with less area is the one with less perimeter
        /// </summary>
        /// <param name="present"></param>
        /// <returns></returns>
        private static int CalculatePaperAndRibbon((int l, int w, int h) present)
        {
            var list = new List<(int a, int b)>
            {
                (a: present.h, b: present.l),
                (a: present.h, b: present.w),
                (a: present.l, b: present.w),
            };

            var min = list.Min(pair => pair.a + pair.b);

            var minPair = list.FindIndex(p => p.a + p.b == min);

            return list.Sum(p => 2 * p.a * p.b)                     // Total area
                + (list[minPair].a * list[minPair].b)               // 2 * min side area
                + (2 * list[minPair].a) + (2 * list[minPair].b)     // Min side perimeter
                + (present.l * present.w * present.h);              // Total volume
        }

        private IEnumerable<(int l, int w, int h)> ParseInput()
        {
            var lines = File.ReadAllLines(InputFilePath);

            return lines.Select(l =>
            {
                var dim = l.Split('x');
                return (l: int.Parse(dim[0]), w: int.Parse(dim[1]), h: int.Parse(dim[2]));
            });
        }
    }
}
