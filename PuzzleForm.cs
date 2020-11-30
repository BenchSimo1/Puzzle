using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzle
{
    public partial class PuzzleForm : Form
    {
        Label emptyCell ;
        Stopwatch timer = new Stopwatch();

        const int NUMBERCOLUMN = 3;
        const int NUMBERROW = 3;

        Size cellSize = new Size(78 , 78);
        
        Point startPointLocation = new Point(30, 26);
        

        public PuzzleForm()
        {
            InitializeComponent();
            createCells();
            emptyCell = groupCells.Controls[8] as Label;


            Shuffle();


        }

        public void Shuffle()
        {
            Random rnd = new Random();
            int n = groupCells.Controls.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                var value = groupCells.Controls[k].Location;
                groupCells.Controls[k].Location = groupCells.Controls[n].Location;
                groupCells.Controls[n].Location = value;
            }


        }

        public bool checkWin()
        {
            for (int i = 0; i < NUMBERCOLUMN; ++i)
            {
                for (int j = 0; j < NUMBERROW; ++j)
                {
                    var currentCell = groupCells.Controls.Find($"Cell({i} , {j})", false)[0];
                    if ( !currentCell.Location.Equals(new Point(startPointLocation.X + cellSize.Width * j, startPointLocation.Y + cellSize.Height * i))) {
                        return false;
                    }
                }
            }

            return true;

        }

        void createCells()
        {
            
            for (int i = 0; i < NUMBERCOLUMN; ++i)
            {
                for (int j = 0; j < NUMBERROW; ++j)
                {
                    Label cell = new Label();
                    cell.AutoSize = false;
                    cell.BorderStyle = BorderStyle.Fixed3D;
                    cell.Font = new Font("Microsoft Sans Serif", 22.2F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                    cell.Size = cellSize;
                    cell.Location = new Point(startPointLocation.X + cellSize.Width * j, startPointLocation.Y + cellSize.Height * i);
                    cell.Name = $"Cell({i} , {j})";
                    cell.TextAlign = ContentAlignment.MiddleCenter;
                    cell.TabIndex = i == 0 ? j + 1 : i == 1 ? j + 4 : j + 7;

                    if (i == NUMBERROW - 1 && j == NUMBERCOLUMN - 1)
                    {
                        cell.Text = "";
                    }
                    else
                    {
                        cell.Text = cell.TabIndex.ToString();
                    }
                    groupCells.Controls.Add(cell);
                }

            }

        }
        
        void swapEmpty(Label cell)
        {
            var value = emptyCell.Location;
            emptyCell.Location = cell.Location;
            cell.Location = value;

        }

        Label findLeft()
        {
            foreach (Label cell in groupCells.Controls)
            {
                if (cell.Location.Y == emptyCell.Location.Y && cell.Location.X == emptyCell.Location.X + cellSize.Width )
                    return cell;
            }

            return null;
        }
        void moveLeft()
        {
            if (emptyCell.Location.X == startPointLocation.X + cellSize.Width * (NUMBERROW -1))
                return;

            swapEmpty(findLeft());
        }


        Label findRight()
        {
            foreach (Label cell in groupCells.Controls)
            {
                if (cell.Location.Y == emptyCell.Location.Y && cell.Location.X + cellSize.Width == emptyCell.Location.X)
                    return cell;
            }

            return null;
        }
        void moveRight()
        {
            if (emptyCell.Location.X == startPointLocation.X )
                return;

            swapEmpty(findRight());

        }


        Label findUp()
        {
            foreach (Label cell in groupCells.Controls)
            {
                if (cell.Location.X == emptyCell.Location.X && cell.Location.Y - cellSize.Height == emptyCell.Location.Y )
                    return cell;
            }

            return null;
        }
        void moveUp()
        {
            if (emptyCell.Location.Y == startPointLocation.Y + cellSize.Height * (NUMBERCOLUMN - 1))
                return;

            swapEmpty(findUp());

        }


        Label findDown()
        {
            foreach (Label cell in groupCells.Controls)
            {
                if (cell.Location.X == emptyCell.Location.X && cell.Location.Y + cellSize.Height == emptyCell.Location.Y)
                    return cell;
            }

            return null;
        }
        void moveDown()
        {
            if (emptyCell.Location.Y == startPointLocation.Y)
                return;

            swapEmpty(findDown());

        }

        private void PuzzleForm_KeyDown(object sender, KeyEventArgs e)
        {

            if (timerLabel.Text == "")
            {
                timer.Start();
            }
            switch (e.KeyCode)
            {
                case Keys.Left:
                    {
                        moveLeft();
                    }
                    break;

                case Keys.Right:
                    {
                        moveRight();
                       
                    }
                    break;

                case Keys.Up:
                    {
                        moveUp();
                    }
                    break;

                case Keys.Down:
                    {
                        moveDown();

                    }
                    break;

                default:
                    Console.Beep();
                    break;
            }

            if (checkWin())
            {
                timer.Stop();
                MessageBox.Show("WIIN !!");
                Shuffle();
                timer.Reset();
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (timer.Elapsed.ToString() != "00:00:00")
                timerLabel.Text = timer.Elapsed.ToString().Remove(8);
            

            if (timer.Elapsed.Minutes.ToString() == "2")
            {
                timer.Reset();
                timerLabel.Text = "";

                MessageBox.Show("Game over ! loser");
                Shuffle();
            }

        }
    }
}
