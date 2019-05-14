using System;
using System.Collections.Generic;
using System.Linq;
using battleships.Interfaces;

namespace battleships.Models
{
    public class Battleship : IBattleship
    {
        private Dictionary<Coordinate, bool> hits;
        private int hitsRemaining;
        public IEnumerable<Coordinate> Coordinates { get; private set; }


        public Battleship(IEnumerable<Coordinate> coords)
        {
            if (!CheckValidCoords(coords))
            {
                throw new ArgumentException("Invalid coords given");
            }

            hits = new Dictionary<Coordinate, bool>();
            Coordinates = coords;

            foreach (var coord in Coordinates)
            {
                this.hits[coord] = false;
            }

            this.hitsRemaining = coords.Count();
        }

        public void Hit(Coordinate coord)
        {
            if (!hits.ContainsKey(coord))
            {
                throw new ArgumentException("Battleship does not contain coordinates " + coord.ToString());
            }

            if (!hits[coord]) {
                hits[coord] = true;
                hitsRemaining--;
            }
        }

        public bool IsSunk()
        {
            return hitsRemaining == 0;
        }

        private bool CheckValidCoords(IEnumerable<Coordinate> coords)
        {
            if (coords.Count() == 1)
            {
                return true;
            }
 
            bool differX = coords.GroupBy(c => c.X).Count() > 1;
            bool differY = coords.GroupBy(c => c.Y).Count() > 1;

            if (differX && differY)
            {
                return false;
            } else
            {
                IEnumerable<int> sortedCoords = differX ? coords.OrderBy(c => c.X).Select(c => c.X) : coords.OrderBy(c => c.Y).Select(c => c.Y);
                var prevCoord = sortedCoords.First();

                foreach (var coord in sortedCoords.Skip(1))
                {
                    if (!(coord == prevCoord + 1))
                    {
                        return false;
                    }

                    prevCoord = coord;
                }

                return true;
            }
        }
    }
}
