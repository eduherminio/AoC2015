using AoCHelper;
using SheepTools.Model;
using System.Collections.Generic;
using System.IO;

namespace AoC_2015
{
    public class Day_03 : BaseDay
    {
        private readonly string _input;

        public Day_03()
        {
            _input = File.ReadAllText(InputFilePath);
        }

        public override string Solve_1()
        {
            var visitedPoints = new HashSet<Point>();

            var currentPoint = new Point(0, 0);
            visitedPoints.Add(currentPoint);

            foreach (var dir in _input)
            {
                currentPoint = currentPoint.Move(dir);
                visitedPoints.Add(currentPoint);
            }

            return visitedPoints.Count.ToString();
        }

        public override string Solve_2()
        {
            var visitedPoints = new HashSet<Point>();

            var start = new Point(0, 0);
            visitedPoints.Add(start);

            var currentSantaPoint = start;
            var currentRoboSantaPoint = start;

            for (int i = 0; i < _input.Length; ++i)
            {
                var dir = _input[i];

                if (i % 2 == 0)
                {
                    currentSantaPoint = currentSantaPoint.Move(dir);
                    visitedPoints.Add(currentSantaPoint);
                }
                else
                {
                    currentRoboSantaPoint = currentRoboSantaPoint.Move(dir);
                    visitedPoints.Add(currentRoboSantaPoint);
                }
            }

            return visitedPoints.Count.ToString();
        }
    }
}
