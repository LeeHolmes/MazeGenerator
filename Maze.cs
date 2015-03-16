using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Maze
{
	/// <summary>
	/// Summary description for Maze.
	/// </summary>
	public class Maze : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>

		private System.ComponentModel.Container components = null;
        MazeGrid mazeGrid;
        Position currentPosition;
        double gridWidth, gridHeight;
        int difficulty;
        
        const int lineWidth = 3;
        const int xOffset = 5, yOffset = lineWidth;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem OptionsDifficulty;
        private System.Windows.Forms.MenuItem FileExit;

		public Maze()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            difficulty = 18;
            Reset();

            this.Paint += new PaintEventHandler(Maze_Paint);
            this.KeyUp += new KeyEventHandler(Maze_KeyUp);
            this.Resize += new EventHandler(Maze_Resize);
		}

        private void Reset()
        {
            mazeGrid = new MazeGrid(difficulty, difficulty);
            
            currentPosition = new Position(0, 0);
            mazeGrid[currentPosition.X, currentPosition.Y].Visited = true;

            this.Invalidate();
            this.Update();
        }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Maze));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.FileExit = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.OptionsDifficulty = new System.Windows.Forms.MenuItem();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                      this.menuItem1,
                                                                                      this.menuItem3});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                      this.FileExit});
            this.menuItem1.Text = "File";
            // 
            // FileExit
            // 
            this.FileExit.Index = 0;
            this.FileExit.Text = "Exit";
            this.FileExit.Click += new System.EventHandler(this.FileExit_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 1;
            this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                      this.OptionsDifficulty});
            this.menuItem3.Text = "Options";
            // 
            // OptionsDifficulty
            // 
            this.OptionsDifficulty.Index = 0;
            this.OptionsDifficulty.Text = "Difficulty";
            this.OptionsDifficulty.Click += new System.EventHandler(this.OptionsDifficulty_Click);
            // 
            // Maze
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Menu = this.mainMenu1;
            this.Name = "Maze";
            this.Text = "Maze Runner";

        }
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Maze());
        }

        private void Maze_Paint(object sender, PaintEventArgs e)
        {
            int screenWidth = this.Width - 15;
            int mazeWidth = mazeGrid.Width;
            int screenHeight = this.Height - 60;
            int mazeHeight = mazeGrid.Length;

            gridWidth = (screenWidth - (mazeWidth*lineWidth)) / mazeWidth;
            gridWidth += lineWidth;

            gridHeight = (screenHeight - (mazeHeight*lineWidth)) / mazeHeight;
            gridHeight += lineWidth;

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            Pen drawPen = new System.Drawing.Pen(Color.Black, lineWidth);
            Brush visitedBrush = new SolidBrush(Color.FromArgb(128, 200, 0, 200));
            Brush currentBrush = new SolidBrush(Color.FromArgb(128, 0, 200, 0));
            Brush drawBrush;

            for(int xCounter = 0; xCounter < mazeWidth; xCounter++)
            {
                for(int yCounter = 0; yCounter < mazeHeight; yCounter++)
                {
                    MazeElement currentElement = mazeGrid[xCounter, yCounter];
                    if(currentElement.Visited)
                    {
                        if((xCounter == currentPosition.X) && (yCounter == currentPosition.Y))
                            drawBrush = currentBrush;
                        else
                            drawBrush = visitedBrush;

                        g.FillRectangle(drawBrush, (int) (xCounter*gridWidth)+xOffset+2,
                            (int) (yCounter*gridHeight)+yOffset+2,
                            (int) gridWidth-lineWidth,
                            (int) gridHeight-lineWidth);
                    }
                                               

                    if(currentElement[MazeElement.Directions.Top] !=
                        MazeElement.Status.Open)
                        g.DrawLine(drawPen, (int) (xCounter*gridWidth)+xOffset,
                                            (int) (yCounter*gridHeight)+yOffset,
                                            (int) ((xCounter+1)*gridWidth)+xOffset,
                                            (int) (yCounter*gridHeight)+yOffset);

                    if(currentElement[MazeElement.Directions.Left] !=
                        MazeElement.Status.Open)
                        g.DrawLine(drawPen, (int) (xCounter*gridWidth)+xOffset,
                                            (int) (yCounter*gridHeight)+yOffset,
                                            (int) (xCounter*gridWidth)+xOffset,
                                            (int) ((yCounter+1)*gridHeight)+yOffset);
                    
                    if(currentElement[MazeElement.Directions.Right] !=
                        MazeElement.Status.Open)
                        g.DrawLine(drawPen, (int) ((xCounter+1)*gridWidth)+xOffset,
                                            (int) (yCounter*gridHeight)+yOffset,
                                            (int) ((xCounter+1)*gridWidth)+xOffset,
                                            (int) ((yCounter+1)*gridHeight)+yOffset);
                    
                    if(currentElement[MazeElement.Directions.Bottom] !=
                        MazeElement.Status.Open)
                        g.DrawLine(drawPen, (int) (xCounter*gridWidth)+xOffset,
                                            (int) ((yCounter+1)*gridHeight)+yOffset,
                                            (int) ((xCounter+1)*gridWidth)+xOffset,
                                            (int) ((yCounter+1)*gridHeight)+yOffset);

                }
            }
        }

        private void Maze_KeyUp(object sender, KeyEventArgs e)
        {
            Rectangle rc;

            if(e.KeyCode.Equals(Keys.Space))
                Reset();

            if(e.KeyCode.Equals(Keys.Down) &&
                mazeGrid[currentPosition.X, currentPosition.Y][MazeElement.Directions.Bottom]
                == MazeElement.Status.Open)
                currentPosition.Y = currentPosition.Y + 1;
            if(e.KeyCode.Equals(Keys.Up) &&
                mazeGrid[currentPosition.X, currentPosition.Y][MazeElement.Directions.Top]
                == MazeElement.Status.Open)
                currentPosition.Y = currentPosition.Y - 1;
            if(e.KeyCode.Equals(Keys.Right) &&
                mazeGrid[currentPosition.X, currentPosition.Y][MazeElement.Directions.Right]
                == MazeElement.Status.Open)
                currentPosition.X = currentPosition.X + 1;
            if(e.KeyCode.Equals(Keys.Left) &&
                mazeGrid[currentPosition.X, currentPosition.Y][MazeElement.Directions.Left]
                == MazeElement.Status.Open)
                currentPosition.X = currentPosition.X - 1;

            rc = new Rectangle( (int) ((currentPosition.X-1)*gridWidth)+xOffset+2, 
                                (int) ((currentPosition.Y-1)*gridHeight)+yOffset+2,
                                (int) (3*gridWidth)-lineWidth,
                                (int) (3*gridHeight)-lineWidth);

            if(currentPosition.Y < 0)
                currentPosition.Y = 0;

            if(currentPosition.Y == mazeGrid.Length)
            {
                MessageBox.Show("You won!");
                Reset();
            }
            else
            {
                mazeGrid[currentPosition.X, currentPosition.Y].Visited = true;
                this.Invalidate(rc);
                this.Update();
            }
        }

        private void FileExit_Click(object sender, System.EventArgs e)
        {
            this.Close();        
        }

        private void OptionsDifficulty_Click(object sender, System.EventArgs e)
        {
            SelectDifficulty difficultyDlg = new SelectDifficulty();
            difficultyDlg.Difficulty = difficulty;

            difficultyDlg.ShowDialog();
            difficulty = difficultyDlg.Difficulty;
            Reset();
        }

        private void Maze_Resize(Object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
