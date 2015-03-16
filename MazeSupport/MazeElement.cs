using System;

namespace Maze
{
	/// <summary>
	/// One grid square in a maze.
	/// </summary>
    public class MazeElement
    {
        private Status[] wallStatus;
        private bool inMap, inAvailable, visited;

        public MazeElement()
        {
            wallStatus = new Status[4];
            Type t = typeof(Directions);
            foreach(Directions d in Enum.GetValues(t))
                this[d] = Status.Blocked;

            inMap = false;
            inAvailable = false;
            visited = false;
        }

        public enum Directions { Top = 0, Left = 1, Right = 2, Bottom = 3 };
        public enum Status { Blocked, Open };

        public bool InMap
        {
            get { return inMap; }
            set { inMap = value; }
        }

        public bool InAvailable
        {
            get { return inAvailable; }
            set { inAvailable = value; }
        }

        public bool Visited
        {
            get { return visited; }
            set { visited = value; }
        }

        public Status this[Directions whichDirection]
        {
            get
            {
                return wallStatus[(int) whichDirection];
            }

            set
            {
                wallStatus[(int) whichDirection] = value;
            }
        }
    }
}
