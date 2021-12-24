using System.Collections.Generic;

namespace AOC_2020
{
    public class Day6 : Day
    {
        public Day6() : base(6) { }

        public void solve()
        {
            System.IO.StreamReader file = getStreamReaderFromFile();

            string line;
            string commonQuestionsPart1 = "";
            HashSet<char> commonQuestionsPart2 = null;
            int part1Result = 0;
            int part2Result = 0;

            while ((line = file.ReadLine()) != null)
            {
                if (line.Length == 0 || file.EndOfStream)
                {
                    if (file.EndOfStream) {
                        commonQuestionsPart1 = string.Concat(commonQuestionsPart1, line);

                        HashSet<char> currentQuestionsSet = parseQuestionsToSet(line);
                        if (commonQuestionsPart2 == null)
                        {
                            commonQuestionsPart2 = new HashSet<char>();
                            commonQuestionsPart2.UnionWith(currentQuestionsSet);
                        }
                        else
                        {
                            commonQuestionsPart2.IntersectWith(currentQuestionsSet);
                        }
                    }

                    part1Result += getNumberOfUniqueAnswers(commonQuestionsPart1);
                    commonQuestionsPart1 = "";

                    part2Result += commonQuestionsPart2.Count;
                    commonQuestionsPart2 = null;
                }
                else
                {
                    commonQuestionsPart1 = string.Concat(commonQuestionsPart1, line);

                    HashSet<char> currentQuestionsSet = parseQuestionsToSet(line);
                    if (commonQuestionsPart2 == null)
                    {
                        commonQuestionsPart2 = new HashSet<char>();
                        commonQuestionsPart2.UnionWith(currentQuestionsSet);
                    }
                    else
                    {
                        commonQuestionsPart2.IntersectWith(currentQuestionsSet);
                    }
                }
            }

            part1_ = part1Result.ToString();
            part2_ = part2Result.ToString();
            outputResult();
        }

        private HashSet<char> parseQuestionsToSet(string questions)
        {
            HashSet<char> questionSet = new HashSet<char>();

            foreach (char question in questions)
            {
                if (!questionSet.Contains(question)) {
                    questionSet.Add(question);
                }
            }

            return questionSet;
        }

        private int getNumberOfUniqueAnswers(string groupQuestions) {
            HashSet<char> seenQuestions = new HashSet<char>();

            foreach (char question in groupQuestions) {
                if (!seenQuestions.Contains(question)) {
                    seenQuestions.Add(question);
                }
            }

            return seenQuestions.Count;
        }
    }
}
