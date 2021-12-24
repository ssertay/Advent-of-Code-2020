using System.Collections.Generic;

namespace AOC_2020
{
    public class Day11 : Day
    {
        public Day11() : base(11) { }

        public void solve()
        {
            part1();
            part2();
            outputResult();
        }

        private void part2()
        {
            List<List<char>> seatLayout = readSeatLayout();

            int round = 0;
            while (executeSingleRound(seatLayout, 2))
            {
                round++;
            }

            part2_ = countOccupiedSeats(seatLayout).ToString();
        }

        private void part1()
        {
            List<List<char>> seatLayout = readSeatLayout();

            int round = 0;
            while (executeSingleRound(seatLayout, 1))
            {                
                round++;
            }

            part1_ = countOccupiedSeats(seatLayout).ToString();
        }

        private int countOccupiedSeats(List<List<char>> seatLayout)
        {
            int occupied = 0;

            for (int row = 0; row < seatLayout.Count; row++)
            {
                for (int col = 0; col < seatLayout[row].Count; col++)
                {
                    if (seatLayout[row][col] == '#') { occupied++; }
                }
            }

            return occupied;
        }

        private bool executeSingleRound(List<List<char>> seatLayout, int part)
        {
            int[][] adjacentCellOffsets = new int[][] {
                new int[] { -1, -1 }, // top-left
                new int[] { -1, 0 },  // top
                new int[] { -1, 1 },  // top-right
                new int[] { 0, 1 },   // right
                new int[] { 1, 1 },   // bottom-right
                new int[] { 1, 0 },   // bottom
                new int[] { 1, -1 },  // bottom-left
                new int[] { 0, -1 },  // left
            };

            bool seatOutOfBounds(int row, int col)
            {
                int rowSize = seatLayout.Count;
                int colSize = seatLayout[0].Count;

                return row < 0 || col < 0 || row >= rowSize || col >= colSize;
            }

            bool seatIsOccupied(int row, int col)
            {
                if (seatOutOfBounds(row, col)) { return false; }
                return seatLayout[row][col] == '#' || seatLayout[row][col] == '%';
            }

            bool firstSeatInDirectionIsOccupied(int row, int col, int[] direction)
            {
                row += direction[0];
                col += direction[1];

                while (!seatOutOfBounds(row, col))
                {
                    if (seatLayout[row][col] != '.')
                    {
                        return seatIsOccupied(row, col);
                    }

                    row += direction[0];
                    col += direction[1];
                }

                return false;
            }

            bool atLeastOneSeatChanged = false;

            // Replace the changed seats with temp variables.
            for (int row = 0; row < seatLayout.Count ;row++)
            {
                for (int col = 0; col < seatLayout[row].Count; col++)
                {
                    if (seatLayout[row][col] == '.') { continue; }

                    int numOfOccupiedAdjacent = 0;
                    int seatTreshold = 0;

                    if (part == 1)
                    {
                        seatTreshold = 4;

                        foreach (int[] offset in adjacentCellOffsets)
                        {
                            if (seatIsOccupied(row + offset[0], col + offset[1]))
                            {
                                numOfOccupiedAdjacent++;
                            }
                        }
                    }

                    else if (part == 2)
                    {
                        seatTreshold = 5;

                        foreach (int[] direction in adjacentCellOffsets)
                        {
                            if (firstSeatInDirectionIsOccupied(row, col, direction))
                            {
                                numOfOccupiedAdjacent++;
                            }
                        }
                    }

                    if (seatIsOccupied(row, col))
                    {
                        if (numOfOccupiedAdjacent >= seatTreshold)
                        {
                            seatLayout[row][col] = '%';
                        }
                    }
                    else
                    {
                        if (numOfOccupiedAdjacent == 0)
                        {
                            seatLayout[row][col] = 'l';
                        }
                    }
                }
            }

            // Replace the temp variables with actual ones.
            for (int row = 0; row < seatLayout.Count; row++)
            {
                for (int col = 0; col < seatLayout[row].Count; col++)
                {
                    if (seatLayout[row][col] == '%')
                    {
                        seatLayout[row][col] = 'L';
                        atLeastOneSeatChanged = true;
                    }

                    else if (seatLayout[row][col] == 'l')
                    {
                        seatLayout[row][col] = '#';
                        atLeastOneSeatChanged = true;
                    }
                }
            }

            return atLeastOneSeatChanged;
        }

        private List<List<char>> readSeatLayout()
        {
            System.IO.StreamReader file = getStreamReaderFromFile();
            List<List<char>> seatLayout = new List<List<char>>();

            string rowString;
            while ((rowString = file.ReadLine()) != null)
            {
                List<char> rowOfSeats = new List<char>();

                foreach (char c in rowString) {
                    rowOfSeats.Add(c);
                }

                seatLayout.Add(rowOfSeats);
            }

            return seatLayout;
        }
    }
}
