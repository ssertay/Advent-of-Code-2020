using System;
using System.Collections.Generic;

namespace AOC_2020
{
    public class Day10 : Day
    {
        public Day10() : base(10) { }

        public void solve()
        {
            part1();
            part2();
            outputResult();
        }

        private void part2()
        {
            List<int> adapters = readAdaptersAndSort();
            adapters.Add(adapters[adapters.Count - 1] + 3);

            Dictionary<int, ulong> table = new Dictionary<int, ulong>();

            table.Add(0, 1);

            ulong getCombinationCount(int adapter)
            {
                if (table.ContainsKey(adapter)) { return table[adapter]; }
                else { return 0; }
            }

            foreach (int adapter in adapters)
            {
                ulong count = 0;
                count += getCombinationCount(adapter - 1);
                count += getCombinationCount(adapter - 2);
                count += getCombinationCount(adapter - 3);
                table.Add(adapter, count);
            }

            part2_ = table[adapters[adapters.Count - 1]].ToString();
        }

        private void part1() {

            List<int> adapters = readAdaptersAndSort();

            int totalDiff = 0;
            int oneDiff = 0;
            int threeDiff = 0;

            for (int i = 0; i <= adapters.Count; i++)
            {
                int diff;

                if (i == 0)
                {
                    diff = adapters[i];
                }
                else if (i == adapters.Count)
                {
                    diff = 3;
                }
                else
                {
                    diff = adapters[i] - adapters[i - 1];
                }

                totalDiff += diff;

                if (diff == 1) { oneDiff++; }
                if (diff == 3) { threeDiff++; }
            }

            part1_ = (oneDiff * threeDiff).ToString();
        }

        private List<int> readAdaptersAndSort()
        {

            System.IO.StreamReader file = getStreamReaderFromFile();
            List<int> adapters = new List<int>();
            string line;

            while ((line = file.ReadLine()) != null)
            {
                adapters.Add(Int32.Parse(line));
            }

            adapters.Sort();

            return adapters;
        }
    }
}
