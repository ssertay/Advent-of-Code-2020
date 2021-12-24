namespace AOC_2020
{
    public class Day
    {
        protected int day_;
        protected string input_filename_;
        protected static string file_dir_ = @"/Users/sertaysener/Projects/AOC_2020/AOC_2020/";
        protected string part1_;
        protected string part2_;

        protected Day(int day)
        {
            day_ = day;
            input_filename_ = "day_" + day + "_input.txt";
        }

        protected void outputResult()
        {
            System.Console.WriteLine("\nAOC 2020 - Day " + day_);
            System.Console.WriteLine("================");
            System.Console.WriteLine("Part 1: " + part1_);
            System.Console.WriteLine("Part 2: " + part2_);
            System.Console.WriteLine("================\n");
        }

        protected string[] readAllLinesFromInputFile()
        {
            return System.IO.File.ReadAllLines(file_dir_ + "Day" + day_.ToString() + "/" + input_filename_);
        }

        protected System.IO.StreamReader getStreamReaderFromFile() {
            return new System.IO.StreamReader(@"/Users/sertaysener/Projects/AOC_2020/AOC_2020/Day" + day_.ToString() + "/" + input_filename_);
        }
    }

}