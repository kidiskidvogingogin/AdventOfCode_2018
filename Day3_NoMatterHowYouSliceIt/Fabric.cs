using System;
using System.Collections.Generic;
using System.Text;

namespace Day3_NoMatterHowYouSliceIt
{
    class Fabric
    {
        public string ID { get; set; }
        public int LeftOffset { get; set; }
        public int TopOffset { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int RightOffset { get { return LeftOffset + Width; } }
        public int BottomOffset { get { return TopOffset + Height; } }

        private Coordinate GetMinOverlap(Fabric fabric1, Fabric fabric2)
        {
            int minX;
            int minY;

            if(fabric1.LeftOffset <= fabric2.LeftOffset)
                minX = fabric2.LeftOffset;
            else
                minX = fabric1.LeftOffset;

            if (fabric1.TopOffset <= fabric2.TopOffset)
                minY = fabric2.TopOffset;
            else
                minY = fabric1.TopOffset;

            return new Coordinate { X = minX, Y = minY };
        }

        private Coordinate GetMaxOverlap(Fabric fabric1, Fabric fabric2)
        {
            int maxX;
            int maxY;

            if (fabric1.RightOffset <= fabric2.RightOffset)
                maxX = fabric1.RightOffset;
            else
                maxX = fabric2.RightOffset;

            if (fabric1.BottomOffset <= fabric2.BottomOffset)
                maxY = fabric1.BottomOffset;
            else
                maxY = fabric2.BottomOffset;

            return new Coordinate { X = maxX, Y = maxY };
        }

        public bool CheckForOverlap(Fabric fabric2)
        {
            bool widthsOverlap =
                (this.LeftOffset <= fabric2.LeftOffset && this.RightOffset > fabric2.LeftOffset)
                || (fabric2.LeftOffset <= this.LeftOffset && fabric2.RightOffset > this.LeftOffset);
            bool heightsOverlap =
                (this.TopOffset <= fabric2.TopOffset && this.BottomOffset > fabric2.TopOffset)
                || (fabric2.TopOffset <= this.TopOffset && fabric2.BottomOffset > this.TopOffset);

            return widthsOverlap && heightsOverlap;
        }

        public ISet<Coordinate> CheckClaimOverlaps(Fabric fabric2, ISet<Coordinate> usedCoordinates)
        {
            if(CheckForOverlap(fabric2))
            {
                Coordinate min = GetMinOverlap(this, fabric2);
                Coordinate max = GetMaxOverlap(this, fabric2);

                for(int x = min.X; x < max.X; x++)
                    for(int y = min.Y; y < max.Y; y++)
                    {
                        Coordinate overlapPoint = new Coordinate { X = x, Y = y };
                        if(!usedCoordinates.Contains(overlapPoint))
                        {
                            usedCoordinates.Add(overlapPoint);
                        }
                    }
            }

            return usedCoordinates;
        }

        public static Fabric Factory(string fabricData, string fabricID = null)
        {
            int left = Convert.ToInt32(fabricData.Substring(0, fabricData.IndexOf(",")));
            fabricData = fabricData.Substring(fabricData.IndexOf(",") + 1);
            int top = Convert.ToInt32(fabricData.Substring(0, fabricData.IndexOf(":")));
            fabricData = fabricData.Substring(fabricData.IndexOf(":") + 2);
            int width = Convert.ToInt32(fabricData.Substring(0, fabricData.IndexOf("x")));
            fabricData = fabricData.Substring(fabricData.IndexOf("x") + 1);
            int height = Convert.ToInt32(fabricData);

            return new Fabric
            {
                ID = fabricID ?? "#-1",
                LeftOffset = left,
                TopOffset = top,
                Height = height,
                Width = width
            };
        }
    }
}
