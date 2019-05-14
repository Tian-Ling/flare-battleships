using System;
using System.Collections.Generic;
using battleships.Models;

namespace battleships.Interfaces
{
    public interface IBattleship
    {
        bool IsSunk();

        void Hit(Coordinate coord);

        IEnumerable<Coordinate> Coordinates { get; }
    }
}
