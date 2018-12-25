using System;
using System.Collections.Generic;
using System.Text;

namespace Day10_TheStarsAlign
{
    class Star
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public int XVelocity { get; set; }
        public int YVelocity { get; set; }

        public Star(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void MoveStar()
        {
            X += XVelocity;
            Y += YVelocity;
        }

        public void RevertMove()
        {
            X -= XVelocity;
            Y -= YVelocity;
        }
    }
}
