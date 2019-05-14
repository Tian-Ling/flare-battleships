using System;
using System.Collections.Generic;
using battleships.Models;

namespace battleships.Interfaces
{
    public interface IBattleship
    {
        bool IsSunk();

        bool Hit(Coordinate coord);

        IEnumerable<Coordinate> Coordinates { get; }
    }
}
