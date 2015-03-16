using System;
using NUnit.Framework;

namespace Maze
{
	/// <summary>
	/// Test the MazeGrid class.
	/// </summary>
    [TestFixture]
    public class TestMazeGrid
    {
        [Test]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void InvalidArgs()
        {
            MazeGrid mg = new MazeGrid(-1, -1);
        }

        [Test]
        public void SmallestMazeLength()
        {
            MazeGrid mg = new MazeGrid(1, 1);
            Assertion.AssertEquals(1, mg.Length);
        }

        [Test]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SmallestMazeAccess()
        {
            MazeGrid mg = new MazeGrid(1, 1);
            MazeElement me = mg[2,0];
        }

        [Test]
        public void SmallestMazeElement()
        {
            MazeGrid mg = new MazeGrid(1, 1);
            MazeElement me = mg[0,0];

            Assertion.Assert(me[MazeElement.Directions.Top] ==
                    MazeElement.Status.Open);
            Assertion.Assert(me[MazeElement.Directions.Bottom] ==
                MazeElement.Status.Open);
        }

        [Test]
        public void Coordinates()
        {
            MazeGrid mg = new MazeGrid(5, 2);
            
            MazeElement me = mg[0,0];
            Assertion.Assert(me[MazeElement.Directions.Top] ==
                MazeElement.Status.Open);

            me = mg[1,4];
            Assertion.Assert(me[MazeElement.Directions.Bottom] ==
                MazeElement.Status.Open);
        }

        [Test]
        public void SimpleMaze()
        {
            MazeGrid mg = new MazeGrid(3, 1);
            
            MazeElement me = mg[0,0];
            Assertion.Assert(me[MazeElement.Directions.Top] ==
                MazeElement.Status.Open);
            Assertion.Assert(me[MazeElement.Directions.Bottom] ==
                MazeElement.Status.Open);

            me = mg[0,1];
            Assertion.Assert(me[MazeElement.Directions.Top] ==
                MazeElement.Status.Open);
            Assertion.Assert(me[MazeElement.Directions.Bottom] ==
                MazeElement.Status.Open);

            me = mg[0,2];
            Assertion.Assert(me[MazeElement.Directions.Top] ==
                MazeElement.Status.Open);
            Assertion.Assert(me[MazeElement.Directions.Bottom] ==
                MazeElement.Status.Open);
        }
    }
}
