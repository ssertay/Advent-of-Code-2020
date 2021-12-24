using System;

namespace AOC_2020
{
    public class Day2 : Day
    {
        public Day2() : base(2) { }

        public void solve()
        {
            string[] policies = readAllLinesFromInputFile();

            char[] separators = new char[] { ' ', '-', ':' };

            int resultPart1 = 0;
            int resultPart2 = 0;

            foreach (string policy in policies)
            {
                string[] subs = policy.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                int min = Int32.Parse(subs[0]);
                int max = Int32.Parse(subs[1]);
                char policyChar = subs[2][0];
                string password = subs[3];


                if (isPasswordValid(1, password, policyChar, min, max))
                {
                    resultPart1++;
                }

                int firstIndex = Int32.Parse(subs[0]);
                int secondIndex = Int32.Parse(subs[1]);

                if (isPasswordValid(2, password, policyChar, firstIndex, secondIndex))
                {
                    resultPart2++;
                }

            }

            part1_ = resultPart1.ToString();
            part2_ = resultPart2.ToString();
            outputResult();
        }

        private bool isPasswordValid(int part, string password, char policyChar, int firstNum, int secondNum)
        {

            if (part == 1)
            {
                int seenCount = 0;
                foreach (char c in password)
                {
                    if (c == policyChar)
                    {
                        seenCount++;
                    }
                }

                return seenCount <= secondNum && seenCount >= firstNum;

            }
            else if (part == 2)
            {
                int count = 0;
                if (password[firstNum - 1] == policyChar) { count++; }
                if (password[secondNum - 1] == policyChar) { count++; }

                return count == 1;
            }

            return false;
        }
    }
}