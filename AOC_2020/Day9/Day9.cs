using System;
using System.Collections.Generic;

namespace AOC_2020
{
    public class Day9 : Day
    {
        public Day9() : base(9) { }

        ulong bad_index = 0;

        public void solve()
        {
            string[] lines = readAllLinesFromInputFile();

            int preamble = 25;

            part1(lines, preamble);
            part2(lines);
            outputResult();
        }

        private void part2(string[] lines)
        {
            ulong window_size = bad_index - 1;
            ulong target = (ulong) Int64.Parse(lines[bad_index]);

            while (window_size > 1)
            {
                ulong begin = 0;
                ulong end = begin + window_size;

                while (end < bad_index)
                {
                    ulong sum = sumNums(begin, end, lines);

                    if (sum == target)
                    {
                        part2_ = findPart2Solution(begin, end, lines).ToString();
                        return;
                    }

                    begin++;
                    end++;
                }

                window_size--;
            }
        }

        private ulong findPart2Solution(ulong begin, ulong end, string[] lines) {

            ulong min = (ulong) Int64.MaxValue;
            ulong max = 0;

            for (ulong i=begin; i<=end ;i++) {
                max = Math.Max(max, (ulong) Int64.Parse(lines[i]));
                min = Math.Min(min, (ulong) Int64.Parse(lines[i]));
            }

            return min + max;
        }

        private ulong sumNums(ulong begin, ulong end, string[] lines)
        {
            ulong sum = 0;

            for (ulong i=begin; i<=end ;i++)
            {
                sum += (ulong) Int64.Parse(lines[i]);
            }

            return sum;
        }

        private void part1(string[] lines, int preamble)
        {

            Queue<ulong> window = new Queue<ulong>();

            for (ulong i = 0; (int) i < lines.Length; i++)
            {
                ulong newNum = (ulong) Int64.Parse(lines[i]);

                if ((int) i < preamble)
                {
                    window.Enqueue(newNum);
                    continue;
                }

                if (!TwoSum(window, newNum))
                {
                    bad_index = i;
                    part1_ = newNum.ToString();
                    return;
                }

                window.Dequeue();
                window.Enqueue(newNum);
            }
        }

        private bool TwoSum(Queue<ulong> window, ulong target)
        {
            ulong[] nums = window.ToArray();

            // Map number to index.
            Dictionary<ulong, ulong> myDictionary = new Dictionary<ulong, ulong>();

            for (ulong index = 0; (int) index < nums.Length ;index++)
            {
                ulong currentNum = nums[index];

                ulong valueIndex = 0;
                if (myDictionary.TryGetValue(target - currentNum, out valueIndex))
                {
                    return true;
                }
                else
                {
                    myDictionary.Add(currentNum, index);
                }
            }
            return false;
        }
    }
}
