using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AOC_2020
{
    public class Day7 : Day
    {
        public Day7() : base(7) { }

        public void solve()
        {
            string[] rules = readAllLinesFromInputFile();

            Dictionary<string, HashSet<Tuple<int, string>>> bagRulesPart1 = new Dictionary<string, HashSet<Tuple<int, string>>>();
            Dictionary<string, Dictionary<string, int>> bagRulesPart2 = new Dictionary<string, Dictionary<string, int>>();

            // Build the dependencies.
            foreach (string rule in rules)
            {
                string[] words = rule.Split(' ', '.')
                    .Where(r => r.Length > 0)
                    .Where(r => !r.Equals("contain"))
                    .Where(r => !r.Contains("bag"))
                    .ToArray();

                string container = String.Concat(words[0], " ", words[1]);

                if (!bagRulesPart1.ContainsKey(container))
                {
                    bagRulesPart1.Add(container, new HashSet<Tuple<int, string>>());
                }

                if (!bagRulesPart2.ContainsKey(container))
                {
                    bagRulesPart2.Add(container, new Dictionary<string, int>());
                }

                int beginIndex = 2;
                if (!words[beginIndex].Equals("no"))
                {
                    while (beginIndex < words.Length)
                    {
                        int amount = Int32.Parse(words[beginIndex]);
                        string containee = String.Concat(words[beginIndex + 1], " ", words[beginIndex + 2]);

                        if (!bagRulesPart1.ContainsKey(containee))
                        {
                            bagRulesPart1.Add(containee, new HashSet<Tuple<int, string>>());
                        }

                        if (!bagRulesPart2[container].ContainsKey(containee))
                        {
                            bagRulesPart2[container].Add(containee, amount);
                        }

                        bagRulesPart1[containee].Add(new Tuple<int, string>(amount, container));
                        beginIndex += 3;
                    }
                }
            }

            Queue<string> queue = new Queue<string>();
            queue.Enqueue("shiny gold");
            int resultPart1 = 0;
            HashSet<string> seen = new HashSet<string>();

            while (queue.Count > 0)
            {
                HashSet<Tuple<int, string>> set = bagRulesPart1[queue.Dequeue()];

                foreach (Tuple<int, string> pair in set)
                {
                    int amount = pair.Item1;
                    string color = pair.Item2;

                    if (!seen.Contains(color))
                    {
                        seen.Add(color);
                        queue.Enqueue(color);
                        resultPart1++;
                    }
                }
            }

            part1_ = resultPart1.ToString();

            queue.Clear();
            queue.Enqueue("shiny gold");
            int resultPart2 = -1;

            while (queue.Count > 0)
            {
                Dictionary<string, int> dict = bagRulesPart2[queue.Dequeue()];
                resultPart2++;

                foreach (KeyValuePair<string, int> entry in dict)
                {
                    string color = entry.Key;
                    int amount = entry.Value;

                    for (int i = 0; i < amount; i++)
                    {
                        queue.Enqueue(color);
                    }
                }
            }

            part2_ = resultPart2.ToString();

            outputResult();
        }
    }
}
