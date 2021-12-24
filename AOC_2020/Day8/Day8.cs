using System;
using System.Collections.Generic;

namespace AOC_2020
{
    public class Day8 : Day
    {
        public Day8() : base(8) { }

        public void solve() {
            part1();
            part2();
            outputResult();
        }

        // Return true if terminates, false if infinite loop detected.
        private bool tryExecuteAndTerminate(string[] instructions, HashSet<int> modifiedIndexes) {
            HashSet<int> seenIndexes = new HashSet<int>();

            int accumulator = 0;
            int instruction_counter = 0;

            int modifiedIndex = -1;
            string cachedInstruction = "";

            while (true)
            {
                // Terminate
                if (instruction_counter == instructions.Length)
                {
                    part2_ = accumulator.ToString();
                    return true;
                }

                string[] instruction = instructions[instruction_counter].Split(' ');

                // Check before executing the instruction.
                if (seenIndexes.Contains(instruction_counter))
                {
                    if (modifiedIndex != -1) {
                        instructions[modifiedIndex] = cachedInstruction;
                    }
                    return false;
                }

                seenIndexes.Add(instruction_counter);

                string operation = instruction[0];
                char sign = instruction[1][0];
                int value = Int32.Parse(instruction[1].Substring(1));

                // Execute.
                switch (operation)
                {
                    case "nop":
                        if (modifiedIndex < 0 && !modifiedIndexes.Contains(instruction_counter)) {
                            // Modify and execute jmp instead.
                            cachedInstruction = instructions[instruction_counter];
                            modifiedIndex = instruction_counter;
                            modifiedIndexes.Add(modifiedIndex);
                            instructions[modifiedIndex] = String.Concat("jmp ", instruction[1]);
                            instruction_counter += value * (sign == '+' ? 1 : -1);
                            break;
                        }
                        instruction_counter++;
                        break;

                    case "acc":
                        accumulator += value * (sign == '+' ? 1 : -1);
                        instruction_counter++;
                        break;

                    case "jmp":
                        if (modifiedIndex < 0 && !modifiedIndexes.Contains(instruction_counter))
                        {
                            // Modify and execute nop instead.
                            cachedInstruction = instructions[instruction_counter];
                            modifiedIndex = instruction_counter;
                            modifiedIndexes.Add(modifiedIndex);
                            instructions[modifiedIndex] = String.Concat("nop ", instruction[1]);
                            instruction_counter++;
                            break;
                        }
                        instruction_counter += value * (sign == '+' ? 1 : -1);
                        break;
                }
            }
        }

        private void part2()
        {
            string[] instructions = readAllLinesFromInputFile();
            
            HashSet<int> modifiedIndexes = new HashSet<int>();

            while (part2_ == null) {
                tryExecuteAndTerminate(instructions, modifiedIndexes);
            }
        }

        private void part1()
        {
            string[] instructions = readAllLinesFromInputFile();

            HashSet<int> seenIndexes = new HashSet<int>();

            int accumulator = 0;
            int instruction_counter = 0;

            while (true) {
                string[] instruction = instructions[instruction_counter].Split(' ');

                // Check before executing the instruction.
                if (seenIndexes.Contains(instruction_counter)) {
                    part1_ = accumulator.ToString();
                    return;
                }

                seenIndexes.Add(instruction_counter);

                string operation = instruction[0];
                char sign = instruction[1][0];
                int value = Int32.Parse(instruction[1].Substring(1));

                switch (operation) {

                    case "nop":
                        instruction_counter++;
                        break;

                    case "acc":
                        accumulator += value * (sign == '+' ? 1 : -1);
                        instruction_counter++;
                        break;

                    case "jmp":
                        instruction_counter += value * (sign == '+' ? 1 : -1);
                        break;
                }
            }
        }

    }
}
