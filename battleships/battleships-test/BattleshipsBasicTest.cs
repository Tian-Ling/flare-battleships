using System;
using System.Collections.Generic;
using System.Linq;
using battleships.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace battleships_test
{
    [TestClass]
    public class BattleshipsBasicTest
    {
        [TestMethod]
        public void TestCoordinateEquality()
        {
            Coordinate equalCoord1 = new Coordinate(3, 4);
            Coordinate equalCoord2 = new Coordinate(3, 4);
            Assert.IsTrue(equalCoord1.Equals(equalCoord2));
        }

        [TestMethod]
        public void TestBattleshipCreation()
        {
            List<Coordinate> validXBattleshipCoords = new List<Coordinate> { new Coordinate(0, 0), new Coordinate(0, 1), new Coordinate(0, 2) };
            Battleship battleship = new Battleship(validXBattleshipCoords);
            Assert.IsTrue(battleship.Coordinates.All(c => validXBattleshipCoords.Contains(c)));

            List<Coordinate> validYBattleshipCoords = new List<Coordinate> { new Coordinate(2, 1), new Coordinate(2, 3), new Coordinate(2, 2) };
            Battleship battleship1 = new Battleship(validYBattleshipCoords);

            List<Coordinate> invalidBattleshipCoords = new List<Coordinate> { new Coordinate(0, 0), new Coordinate(0, 2), new Coordinate(0, 3) };
            Assert.ThrowsException<ArgumentException>(() => new Battleship(invalidBattleshipCoords));
        }

        [TestMethod]
        public void TestBattleshipHit()
        {
            List<Coordinate> battleshipCoords = new List<Coordinate> { new Coordinate(0, 0), new Coordinate(0, 1), new Coordinate(0, 2) };
            Battleship battleship = new Battleship(battleshipCoords);

            // Test hitting battleship with invalid coordinates
            Assert.ThrowsException<ArgumentException>(() => battleship.Hit(new Coordinate(-1, -1)));
            Assert.ThrowsException<ArgumentException>(() => battleship.Hit(new Coordinate(0, 3)));
            Assert.ThrowsException<ArgumentException>(() => battleship.Hit(new Coordinate(1, 0)));
            battleship.Hit(new Coordinate(0, 0));
            battleship.Hit(new Coordinate(0, 1));
            battleship.Hit(new Coordinate(0, 2));

            Assert.IsTrue(battleship.IsSunk());
        }

        [TestMethod]
        public void TestBattleshipBoardCreation()
        {
            List<Coordinate> battleship1Coords = new List<Coordinate> { new Coordinate(0, 0), new Coordinate(0, 1), new Coordinate(0, 2) };
            Battleship battleship1 = new Battleship(battleship1Coords);

            List<Coordinate> battleship2Coords = new List<Coordinate> { new Coordinate(2, 1), new Coordinate(3, 1), new Coordinate(4, 1) };
            Battleship battleship2 = new Battleship(battleship2Coords);

            BattleshipBoard board = new BattleshipBoard(new List<Battleship> { battleship1, battleship2 });

            Assert.IsTrue(board.RemainingBattleships == 2);

            // Test overlapping ship
            List<Coordinate> overlappingBattleshipCoords = new List<Coordinate> { new Coordinate(3, 1), new Coordinate(3, 2), new Coordinate(3, 3) };
            Assert.ThrowsException<ArgumentException>(() => board.AddBattleship(new Battleship(overlappingBattleshipCoords)));

            // Test overlapping ship with illegal coordinates
            List<Coordinate> overlappingIllegalBattleshipCoords = new List<Coordinate> { new Coordinate(-1, 0), new Coordinate(0, 0), new Coordinate(1, 0) };
            Assert.ThrowsException<ArgumentException>(() => board.AddBattleship(new Battleship(overlappingIllegalBattleshipCoords)));
        }

        [TestMethod]
        public void TestBattleshipBoardAttack()
        {
            List<Coordinate> battleship1Coords = new List<Coordinate> { new Coordinate(0, 0), new Coordinate(0, 1), new Coordinate(0, 2) };
            Battleship battleship1 = new Battleship(battleship1Coords);

            List<Coordinate> battleship2Coords = new List<Coordinate> { new Coordinate(2, 1), new Coordinate(3, 1), new Coordinate(4, 1) };
            Battleship battleship2 = new Battleship(battleship2Coords);

            BattleshipBoard board = new BattleshipBoard(new List<Battleship> { battleship1, battleship2 });

            // Destroy Battleship1
            board.Attack(new Coordinate(0, 0));
            board.Attack(new Coordinate(0, 1));
            board.Attack(new Coordinate(0, 2));

            Assert.IsTrue(!board.IsLost());
            Assert.IsTrue(board.RemainingBattleships == 1);

            // Destroy Battleship 2
            board.Attack(new Coordinate(2, 1));
            board.Attack(new Coordinate(3, 1));
            board.Attack(new Coordinate(4, 1));


            Assert.IsTrue(board.IsLost());
            Assert.IsTrue(board.RemainingBattleships == 0);

            // Redestroy Battleship 2
            board.Attack(new Coordinate(0, 0));
            board.Attack(new Coordinate(0, 1));
            board.Attack(new Coordinate(0, 2));

            Assert.IsTrue(board.IsLost());
            Assert.IsTrue(board.RemainingBattleships == 0);
        }

    }
}
