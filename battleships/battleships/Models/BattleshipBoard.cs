using System;
using System.Collections.Generic;
using System.Linq;
using battleships.Interfaces;

namespace battleships.Models
{
    public class BattleshipBoard : IBoard
    {
        private const int BOARD_SIZE = 10;
        private int remainingBattleships;
        public IBattleship[,] Battleships { get; private set; }

        public BattleshipBoard(IEnumerable<IBattleship> battleships)
        {
            this.Battleships = new IBattleship[BOARD_SIZE, BOARD_SIZE];

            foreach (var battleship in battleships)
            {
                if (!ValidBattleship(battleship))
                {
                    throw new ArgumentException("Battleships cannot overlap with other battleships");
                }

                foreach (var coord in battleship.Coordinates)
                {
                    this.Battleships[coord.X, coord.Y] = battleship;
                }
            }

            this.remainingBattleships = battleships.Count();
        }

        public void AddBattleship(IBattleship battleship)
        {
            if (!ValidBattleship(battleship))
            {
                throw new ArgumentException("Battleships cannot overlap with other battleships");
            }

            foreach (var coord in battleship.Coordinates)
            {
                this.Battleships[coord.X, coord.Y] = battleship;
            }
        }

        public bool Attack(Coordinate attackCoord)
        {
            if (!ValidCoordinate(attackCoord))
            {
                throw new ArgumentException("Attacks must be within the bounds of the board");
            }

            if (Battleships[attackCoord.X, attackCoord.Y] != null)
            {
                IBattleship battleship = Battleships[attackCoord.X, attackCoord.Y];
                if (battleship.Hit(attackCoord) && battleship.IsSunk())
                {
                    remainingBattleships--;
                }

                return true;
            }

            return false;
        }

        public bool IsLost()
        {
            return remainingBattleships == 0;
        }

        private bool ValidCoordinate(Coordinate coord)
        {
            if ((coord.X < 0 || coord.X >= BOARD_SIZE) || (coord.Y < 0 || coord.Y >= BOARD_SIZE))
            {
                return false;
            } else
            {
                return true;
            }
        }

        private bool ValidBattleship(IBattleship battleship)
        {
            foreach (Coordinate battleshipCoord in battleship.Coordinates)
            {
                if (Battleships[battleshipCoord.X, battleshipCoord.Y] != null)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
