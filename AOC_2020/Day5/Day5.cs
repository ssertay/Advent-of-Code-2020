using System;
using System.Collections.Generic;

namespace AOC_2020
{
    public class Day5 : Day
    {
        public Day5() : base(5) { }

        public void solve()
        {
            string[] boardPasses = readAllLinesFromInputFile();

            int max_id = Int32.MinValue;
            Dictionary<int, HashSet<int>> seatChart = new Dictionary<int, HashSet<int>>();

            foreach (string boardPass in boardPasses)
            {
                int rowNum, colNum;
                max_id = Math.Max(max_id, calculatePassId(boardPass, out rowNum, out colNum));

                if (!seatChart.ContainsKey(rowNum))
                {
                    seatChart.Add(rowNum, new HashSet<int>());
                }

                HashSet<int> seatRow;
                seatChart.TryGetValue(rowNum, out seatRow);
                seatRow.Add(colNum);

                if (seatRow.Count == 8)
                {
                    seatChart.Remove(rowNum);
                }
            }

            

            part1_ = max_id.ToString();
            part2_ = findMySeatID(seatChart).ToString();
            outputResult();
        }

        private int findMySeatID(Dictionary<int, HashSet<int>> seatChart) {

            foreach (KeyValuePair<int, HashSet<int>> entry in seatChart)
            {
                if (entry.Value.Count == 7)
                {
                    for (int i=0; i<7 ;i++) {
                        if (!entry.Value.Contains(i)) {
                            return (entry.Key * 8) + i;
                        }
                    }
                }
            }

            return 0;
        }

        private int calculatePassId(string boardPass, out int row_, out int col_)
        {
            int upper = 127;
            int lower = 0;

            for (int i = 0; i < 7; i++)
            {
                int diff = upper - lower;
                if (boardPass[i] == 'F')
                {
                    upper = upper - ((diff / 2) + (diff % 2 == 0 ? 0 : 1));
                }
                else if (boardPass[i] == 'B')
                {
                    lower = lower + ((diff / 2) + (diff % 2 == 0 ? 0 : 1));
                }
            }

            int row = lower;

            lower = 0;
            upper = 7;

            for (int i = 7; i < 10; i++)
            {
                int diff = upper - lower;
                if (boardPass[i] == 'L')
                {
                    upper = upper - ((diff / 2) + (diff % 2 == 0 ? 0 : 1));
                }
                else if (boardPass[i] == 'R')
                {
                    lower = lower + ((diff / 2) + (diff % 2 == 0 ? 0 : 1));
                }
            }

            int col = lower;

            row_ = row;
            col_ = col;
            return (row * 8) + col;
        }
    }
}
