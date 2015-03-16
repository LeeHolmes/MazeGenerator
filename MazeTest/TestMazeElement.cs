using System;
using NUnit.Framework;

namespace Maze
{
	/// <summary>
	/// Test the MazeElement class.
	/// </summary>
	[TestFixture]
	public class TestMazeElement
	{
        [Test]
		public void Created()
		{
            MazeElement me = new MazeElement();

            Assertion.Assert(me[MazeElement.Directions.Top] == MazeElement.Status.Blocked);
            Assertion.Assert(me[MazeElement.Directions.Left] == MazeElement.Status.Blocked);
            Assertion.Assert(me[MazeElement.Directions.Right] == MazeElement.Status.Blocked);
            Assertion.Assert(me[MazeElement.Directions.Bottom] == MazeElement.Status.Blocked);
		}
	}
}
