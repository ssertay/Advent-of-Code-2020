using System;
using System.Collections.Generic;

namespace AOC_2020
{
    public class Day1 : Day
    {
        public Day1() : base(1) { }

        public void solve()
        {
            string[] lines = readAllLinesFromInputFile();

            int target = 2020;

            int[] part1Numbers;
            TryTwoSum(lines, target, out part1Numbers);

            int[] part2Numbers = ThreeSum(lines, target);

            part1_ = (part1Numbers[0] * part1Numbers[1]).ToString();
            part2_ = (part2Numbers[0] * part2Numbers[1] * part2Numbers[2]).ToString();
            outputResult();
        }

        private int[] ThreeSum(string[] lines, int target)
        {
            // Map int to int pair.
            Dictionary<int, int[]> myDictionary = new Dictionary<int, int[]>();

            for (int index = 0; index < lines.Length; index++)
            {
                int currentNum = Int32.Parse(lines[index]);
                int remainingSum = target - currentNum;

                int[] intPair;
                if (!myDictionary.TryGetValue(remainingSum, out intPair))
                {
                    var arrayWithCurrentNumMissing = new List<string>(lines);
                    arrayWithCurrentNumMissing.RemoveAt(index);

                    int[] twoSumResult;
                    if (TryTwoSum(arrayWithCurrentNumMissing.ToArray(), target - currentNum, out twoSumResult))
                    {
                        myDictionary.Add(remainingSum, twoSumResult);
                    }
                }
                if (myDictionary.TryGetValue(remainingSum, out intPair))
                {
                    return new int[] { currentNum, intPair[0], intPair[1] };
                }
            }

            return new int[] { 0, 0, 0 };
        }

        private bool TryTwoSum(string[] lines, int target, out int[] resultPair)
        {
            // Map number to index.
            Dictionary<int, int> myDictionary = new Dictionary<int, int>();

            for (int index = 0; index < lines.Length; index++)
            {
                int currentNum = Int32.Parse(lines[index]);

                int valueIndex = 0;
                if (myDictionary.TryGetValue(target - currentNum, out valueIndex))
                {
                    resultPair = new int[] { Int32.Parse(lines[valueIndex]), currentNum };
                    return true;
                }
                else
                {
                    myDictionary.Add(currentNum, index);
                }
            }

            resultPair = null;
            return false;
        }
    }
}