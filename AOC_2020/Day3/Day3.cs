using System.Collections.Generic;

namespace AOC_2020
{
    public class Day3 : Day
    {
        public Day3() : base(3) { }

        public void solve()
        {
            string[] lines = readAllLinesFromInputFile();

            List<List<char>> map = new List<List<char>>();

            // Initialize the map.
            for (int r = 0; r < lines.Length; r++)
            {

                string line = lines[r];

                List<char> mapRow = new List<char>();

                for (int c = 0; c < line.Length; c++)
                {
                    mapRow.Add(line[c]);
                }

                map.Add(mapRow);
            }

            List<int[]> slopes = new List<int[]>()
            {
                // Right, down respectively.
                new int[] { 1, 1 },
                new int[] { 3, 1 },
                new int[] { 5, 1 },
                new int[] { 7, 1 },
                new int[] { 1, 2 },
            };

            long part2Answer = 1;

            foreach (int[] slope in slopes)
            {
                part2Answer = part2Answer * countTreesOnSlope(slope, map);
            }

            part1_ = countTreesOnSlope(slopes[1], map).ToString();
            part2_ = part2Answer.ToString();
            outputResult();
        }

        private int countTreesOnSlope(int[] slope, List<List<char>> map)
        {
            // Row and Col values of current positions.
            int[] position = new int[] { 0, 0 };
            int rightStep = slope[0];
            int downStep = slope[1];
            int mapRowCount = map.Count;
            int mapColCount = map[0].Count;
            int encounteredTrees = 0;

            while (position[0] < mapRowCount - 1)
            {
                // Walk down by given amount.
                position[0] = position[0] + downStep;

                // Walk right by given amount.
                position[1] = (position[1] + rightStep) % mapColCount;

                if (map[position[0]][position[1]] == '#')
                {
                    encounteredTrees++;
                }
            }

            return encounteredTrees;
        }
    }
}
