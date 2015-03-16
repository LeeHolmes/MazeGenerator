using System;
using System.Collections;

namespace Maze
{
	/// <summary>
	/// Summary description for MazeGrid.
	/// </summary>
	public class MazeGrid
	{
        private int length, width;
        private MazeElement[][] mazeGrid;
        Random generator;

		public MazeGrid(int length, int width)
		{
            generator = new Random();

            if(length <= 0)
                throw new IndexOutOfRangeException("Length must be > 0");

            if(width <= 0)
                throw new IndexOutOfRangeException("Width must be > 0");

            this.length = length;
            this.width = width;

            InitializeMaze();
            GenerateMaze();
		}

        public int Length { get { return length; } }
        public int Width { get { return width; } }

        public MazeElement this[int x, int y]
        {
            get
            {
                if(x >= width)
                    throw new IndexOutOfRangeException("x must be < " + width);
                if(y >= length)
                    throw new IndexOutOfRangeException("y must be < " + length);

                return mazeGrid[y][x];
            }
        }

        private void InitializeMaze()
        {
            mazeGrid = new MazeElement[Length][];
            for(int counter = 0; counter < Length; counter++)
            {
                mazeGrid[counter] = new MazeElement[Width];
                for(int counter2 = 0; counter2 < Width; counter2++)
                    mazeGrid[counter][counter2] = new MazeElement();
            }

            MazeElement topLeft = this[0,0];
            topLeft[MazeElement.Directions.Top] =
                MazeElement.Status.Open;

            MazeElement bottomRight = this[Width - 1, Length - 1];
            bottomRight[MazeElement.Directions.Bottom] =
                MazeElement.Status.Open;
        }

        private void GenerateMaze()
        {
            ArrayList availableSquares = new ArrayList();

            int xPosition = generator.Next(Width);
            int yPosition = generator.Next(Length);
            Position p = new Position(xPosition, yPosition);
            availableSquares.Add(p);

            while(availableSquares.Count > 0)
            {
                int availableCount = availableSquares.Count;
                int nextSquare = generator.Next(availableCount);

                Position currentElement = (Position) availableSquares[nextSquare];
                GenerateNeighbors(currentElement, availableSquares);
                ConnectToMap(currentElement);
            }
        }

        private void GenerateNeighbors(Position currentElement, ArrayList availableSquares)
        {
            if((currentElement.X > 0))
                AddAvailable(availableSquares, currentElement.X - 1, currentElement.Y); 
            if((currentElement.X != (Width - 1)))
                AddAvailable(availableSquares, currentElement.X + 1, currentElement.Y); 
            if((currentElement.Y > 0))
                AddAvailable(availableSquares, currentElement.X, currentElement.Y - 1); 
            if((currentElement.Y != (Length - 1)))
                AddAvailable(availableSquares, currentElement.X, currentElement.Y + 1); 

            availableSquares.Remove(currentElement);
        }

        private void AddAvailable(ArrayList availableSquares, int xPos, int yPos)
        {
            if((! this[xPos,yPos].InMap) && (! this[xPos, yPos].InAvailable))
            {
                this[xPos, yPos].InAvailable = true;
                availableSquares.Add(new Position(xPos, yPos));
            }
        }

        private void ConnectToMap(Position currentElement)
        {
            ArrayList mapNeighbors = new ArrayList();
            this[currentElement.X,currentElement.Y].InMap = true;

            if((currentElement.X > 0) && 
                (this[currentElement.X - 1,currentElement.Y].InMap))
                mapNeighbors.Add(new Position(currentElement.X - 1,currentElement.Y));
            if((currentElement.X != (Width - 1)) && 
                (this[currentElement.X + 1,currentElement.Y].InMap))
                mapNeighbors.Add(new Position(currentElement.X + 1,currentElement.Y));
            if((currentElement.Y > 0) && 
                (this[currentElement.X,currentElement.Y - 1].InMap))
                mapNeighbors.Add(new Position(currentElement.X,currentElement.Y - 1));
            if((currentElement.Y != (Length - 1)) && 
                (this[currentElement.X,currentElement.Y + 1].InMap))
                mapNeighbors.Add(new Position(currentElement.X,currentElement.Y + 1));

            int neighborCount = mapNeighbors.Count;
            
            if(neighborCount > 0)
            {
                int connectedNeighbor = generator.Next(neighborCount);
                ConnectNeighbors(currentElement, (Position) mapNeighbors[connectedNeighbor]);
            }
        }

        private void ConnectNeighbors(Position firstNeighbor, Position secondNeighbor)
        {
            if(firstNeighbor.X < secondNeighbor.X)
            {
                this[firstNeighbor.X,firstNeighbor.Y][MazeElement.Directions.Right] =
                    MazeElement.Status.Open;
                this[secondNeighbor.X,secondNeighbor.Y][MazeElement.Directions.Left] =
                    MazeElement.Status.Open;
            }
            else if (firstNeighbor.X > secondNeighbor.X)
            {
                this[firstNeighbor.X,firstNeighbor.Y][MazeElement.Directions.Left] =
                    MazeElement.Status.Open;
                this[secondNeighbor.X,secondNeighbor.Y][MazeElement.Directions.Right] =
                    MazeElement.Status.Open;
            }
            else if(firstNeighbor.Y < secondNeighbor.Y)
            {
                this[firstNeighbor.X,firstNeighbor.Y][MazeElement.Directions.Bottom] =
                    MazeElement.Status.Open;
                this[secondNeighbor.X,secondNeighbor.Y][MazeElement.Directions.Top] =
                    MazeElement.Status.Open;
            }
            else if (firstNeighbor.Y > secondNeighbor.Y)
            {
                this[firstNeighbor.X,firstNeighbor.Y][MazeElement.Directions.Top] =
                    MazeElement.Status.Open;
                this[secondNeighbor.X,secondNeighbor.Y][MazeElement.Directions.Bottom] =
                    MazeElement.Status.Open;
            }
        }
	}

    public class Position
    {
        public int X;
        public int Y;

        public Position(int xPosition, int yPosition)
        {
            X = xPosition;
            Y = yPosition;
        }
    }

}
