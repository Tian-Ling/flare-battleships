using System;
using battleships.Models;

namespace battleships.Interfaces
{
    public interface IBoard
    {
        bool IsLost();

        bool Attack(Coordinate coord);

        void AddBattleship(IBattleship battleship);
    }
}
