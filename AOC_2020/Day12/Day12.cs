using System;
using System.Collections.Generic;

namespace AOC_2020
{
    public class Day12 : Day
    {
        public Day12() : base(12) { }

        public void solve()
        {
            part1();
            part2();
            outputResult();
        }

        char[] dirs = new char[] { 'N', 'E', 'S', 'W' };

        readonly Dictionary<char, int[]> directions = new Dictionary<char, int[]>() {
            { 'N', new int[] { 0, 1 } },    // north
            { 'E', new int[] { -1, 0 } },    // east
            { 'S', new int[] { 0, -1 } },   // south
            { 'W', new int[] { 1, 0 } },   // west
        };

        private void moveWp(char dir, ref int wpXPos, ref int wpYPos, ref int value)
        {
            wpXPos += directions[dir][0] * value;
            wpYPos += directions[dir][1] * value;
        }

        private void rotateWaypoint(char dir, ref int wpX, ref int wpY, ref int shipXPos, ref int shipYPos, ref int degree)
        {
            int value = degree / 90;

            for (int i = 0; i < value; i++)
            {
                int tmp = wpX;
                wpX = wpY;
                wpY = tmp;

                if (dir == 'R') { wpX *= -1; }
                else if (dir == 'L') { wpY *= -1; }
            }
        }

        private void moveShipTowardsWaypoint(ref int wpX, ref int wpY, ref int shipXPos, ref int shipYPos, ref int value)
        {
            shipXPos += wpX * value;
            shipYPos += wpY * value;
        }

        private void takeSingleActionPart2(string nav_dir, ref int wpX, ref int wpY, ref int shipXPos, ref int shipYPos)
        {

            char action = nav_dir[0];
            int value = Math.Abs(Int32.Parse(nav_dir.Substring(1)));

            switch (action)
            {
                case 'N':
                    moveWp(action, ref wpX, ref wpY, ref value);
                    break;
                case 'S':
                    moveWp(action, ref wpX, ref wpY, ref value);
                    break;
                case 'E':
                    moveWp(action, ref wpX, ref wpY, ref value);
                    break;
                case 'W':
                    moveWp(action, ref wpX, ref wpY, ref value);
                    break;
                case 'L':
                    rotateWaypoint(action, ref wpX, ref wpY, ref shipXPos, ref shipYPos, ref value);
                    break;
                case 'R':
                    rotateWaypoint(action, ref wpX, ref wpY, ref shipXPos, ref shipYPos, ref value);
                    break;
                case 'F':
                    moveShipTowardsWaypoint(ref wpX, ref wpY, ref shipXPos, ref shipYPos, ref value);
                    break;
            }
        }

        private void part2()
        {
            string[] navigation_directions = readAllLinesFromInputFile();

            int wpX = -10, wpY = 1;
            int shipXPos = 0, shipYPos = 0;

            foreach (string nav_dir in navigation_directions)
            {
                takeSingleActionPart2(nav_dir, ref wpX, ref wpY, ref shipXPos, ref shipYPos);
            }

            part2_ = calculateManhattanDistance(shipXPos, shipYPos).ToString();
        }

        private void move(char dir, ref int shipXPos, ref int shipYPos, ref int value)
        {
            shipXPos += directions[dir][0] * value;
            shipYPos += directions[dir][1] * value;
        }

        private void turn(char dir, ref int shipDirection, ref int degree)
        {
            int value = degree / 90;
            if (dir == 'L') { shipDirection = (shipDirection - value + 4) % 4; }
            else if (dir == 'R') { shipDirection = (shipDirection + value + 4) % 4; }
        }

        private void forward(ref int shipDirection, ref int shipXPos, ref int shipYPos, ref int value)
        {
            shipXPos += directions[dirs[shipDirection]][0] * value;
            shipYPos += directions[dirs[shipDirection]][1] * value;
        }

        private void takeSingleAction(string nav_dir, ref int shipDirection, ref int shipXPos, ref int shipYPos)
        {

            char action = nav_dir[0];
            int value = Math.Abs(Int32.Parse(nav_dir.Substring(1)));

            switch (action)
            {
                case 'N':
                    move(action, ref shipXPos, ref shipYPos, ref value);
                    break;
                case 'S':
                    move(action, ref shipXPos, ref shipYPos, ref value);
                    break;
                case 'E':
                    move(action, ref shipXPos, ref shipYPos, ref value);
                    break;
                case 'W':
                    move(action, ref shipXPos, ref shipYPos, ref value);
                    break;
                case 'L':
                    turn(action, ref shipDirection, ref value);
                    break;
                case 'R':
                    turn(action, ref shipDirection, ref value);
                    break;
                case 'F':
                    forward(ref shipDirection, ref shipXPos, ref shipYPos, ref value);
                    break;
            }
        }

        private int calculateManhattanDistance(int shipXPos, int shipYPos)
        {
            return Math.Abs(shipXPos) + Math.Abs(shipYPos);
        }

        private void part1()
        {
            string[] navigation_directions = readAllLinesFromInputFile();

            int shipDirection = 1; // start facing east
            int shipXPos = 0, shipYPos = 0;

            foreach (string nav_dir in navigation_directions)
            {
                takeSingleAction(nav_dir, ref shipDirection, ref shipXPos, ref shipYPos);
            }

            part1_ = calculateManhattanDistance(shipXPos, shipYPos).ToString();
        }
    }
}
