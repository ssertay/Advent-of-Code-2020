using System;
using System.Collections.Generic;
using System.Text;

namespace AOC_2020
{
    public class Day4 : Day
    {
        public Day4() : base(4) { }

        public void solve()
        {
            System.IO.StreamReader file = getStreamReaderFromFile();

            string line;
            int validPassportCountPart1 = 0;
            int validPassportCountPart2 = 0;
            List<string> passport = new List<string>();
            bool validPart1 = false;

            while ((line = file.ReadLine()) != null)
            {
                if (line.Length > 0)
                {
                    string[] entries = line.Split(' ');
                    foreach (string entry in entries)
                    {
                        passport.Add(entry);
                    }
                }
                else
                {
                    validPart1 = false;
                    if (isValidPassport(passport, out validPart1))
                    {
                        validPassportCountPart2++;
                    }
                    if (validPart1) {
                        validPassportCountPart1++;
                    }
                    passport.Clear();
                }
            }

            validPart1 = false;
            if (isValidPassport(passport, out validPart1))
            {
                validPassportCountPart2++;
            }
            if (validPart1)
            {
                validPassportCountPart1++;
            }
            passport.Clear();

            part1_ = validPassportCountPart1.ToString();
            part2_ = validPassportCountPart2.ToString();
            outputResult();
        }

        private bool isValidPassport(List<string> passport, out bool validPart1)
        {
            bool validateYear(string val, int min, int max)
            {
                int parsed = Int32.Parse(val);
                return val.Length == 4 && parsed >= min && parsed <= max;
            }

            bool validateHeight(string val)
            {
                if (val.EndsWith("cm"))
                {
                    int parsed = Int32.Parse(val.Substring(0, val.Length - 2));
                    return parsed >= 150 && parsed <= 193;
                }
                else if (val.EndsWith("in"))
                {
                    int parsed = Int32.Parse(val.Substring(0, val.Length - 2));
                    return parsed >= 59 && parsed <= 76;
                }
                else
                {
                    return false;
                }
            }

            bool validateHairColor(string val)
            {
                if (val.StartsWith("#"))
                {
                    byte[] asciiBytes = Encoding.ASCII.GetBytes(val);
                    for (int i = 1; i < asciiBytes.Length; i++)
                    {
                        if (!((asciiBytes[i] <= 57 && asciiBytes[i] >= 48) || (asciiBytes[i] <= 102 && asciiBytes[i] >= 97)))
                        {
                            return false;
                        }
                    }
                    return true;
                }
                return false;
            }

            bool validateEyeColor(string val)
            {
                List<string> accepted = new List<string> {
                    "amb", "blu", "brn", "gry", "grn", "hzl", "oth"
                };
                return accepted.Contains(val);
            }

            HashSet<string> expectedFieldsPart1 = new HashSet<string>() {
                "byr"
                , "iyr"
                , "eyr"
                , "hgt"
                , "hcl"
                , "ecl"
                , "pid"
            };

            HashSet<string> expectedFields = new HashSet<string>() {
                "byr"
                , "iyr"
                , "eyr"
                , "hgt"
                , "hcl"
                , "ecl"
                , "pid"
            };

            foreach (string entry in passport)
            {
                string[] pair = entry.Split(':');
                string field = pair[0];
                string value = pair[1];

                switch (field)
                {
                    case "byr":
                        expectedFieldsPart1.Remove("byr");
                        if (validateYear(value, 1920, 2020))
                        {
                            expectedFields.Remove("byr");
                        }
                        break;
                    case "iyr":
                        expectedFieldsPart1.Remove("iyr");
                        if (validateYear(value, 2010, 2020))
                        {
                            expectedFields.Remove("iyr");
                        }
                        break;
                    case "eyr":
                        expectedFieldsPart1.Remove("eyr");
                        if (validateYear(value, 2020, 2030))
                        {
                            expectedFields.Remove("eyr");
                        }
                        break;
                    case "hgt":
                        expectedFieldsPart1.Remove("hgt");
                        if (validateHeight(value))
                        {
                            expectedFields.Remove("hgt");
                        }
                        break;
                    case "hcl":
                        expectedFieldsPart1.Remove("hcl");
                        if (validateHairColor(value))
                        {
                            expectedFields.Remove("hcl");
                        }
                        break;
                    case "ecl":
                        expectedFieldsPart1.Remove("ecl");
                        if (validateEyeColor(value))
                        {
                            expectedFields.Remove("ecl");
                        }
                        break;
                    case "pid":
                        expectedFieldsPart1.Remove("pid");
                        int i;
                        if (int.TryParse(value, out i) && value.Length == 9)
                        {
                            expectedFields.Remove("pid");
                        }
                        break;
                }
            }

            validPart1 = expectedFieldsPart1.Count == 0;
            return expectedFields.Count == 0;
        }
    }
}
