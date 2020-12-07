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
        Rectangle dragBoxFromMouseDown;
        Label selectedLabel;
        



        public PuzzleForm()
        {
            InitializeComponent();
            createCells();
            emptyCell = groupCells.Controls[8] as Label;

            emptyCell.DragEnter += new DragEventHandler(this.emptyCell_DragEnter);
            emptyCell.DragLeave += new System.EventHandler(this.emptyCell_DragLeave);

            //groupCells.DragDrop += new  DragEventHandler(this.groupCells_DragDrop);




            Shuffle();
        }

       


        private void emptyCell_DragEnter(object sender, DragEventArgs e)
        {
            Console.WriteLine("Enter");
        }
        private void emptyCell_DragLeave(object sender, System.EventArgs e)
        {
            Console.WriteLine("leave");

        }

        private void label_MouseDown(object sender , MouseEventArgs e)
        {
            selectedLabel = sender as Label;
            selectedLabel.MouseMove += new MouseEventHandler(this.label_MouseMove);
            

            if (selectedLabel != findDown() && selectedLabel != findUp() && selectedLabel != findLeft() && selectedLabel != findRight())
            {
                dragBoxFromMouseDown = Rectangle.Empty;
            }
            else
            {
                Size dragSize = SystemInformation.DragSize;

                // Create a rectangle using the DragSize, with the mouse position being
                // at the center of the rectangle.
                dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
                                          e.Y - (dragSize.Height / 2)), dragSize);

                
            }
        }

        private void groupCells_MouseUp(object sender, MouseEventArgs e)
        {
            
            //if (groupCells.GetChildAtPoint(e.Location) != emptyCell)
            //{
            //    Console.WriteLine("yey");
            //    dragBoxFromMouseDown = Rectangle.Empty;
            //}
        }

        private void label_MouseMove(object sender, MouseEventArgs e)
        {

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {

                // If the mouse moves outside the rectangle, start the drag.
                if (dragBoxFromMouseDown != Rectangle.Empty &&
                    !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    DragDropEffects dropEffect = groupCells.DoDragDrop(selectedLabel, DragDropEffects.All | DragDropEffects.Link);
                    swapEmpty(selectedLabel);
                }
            }
        }

        //private void groupCells_DragDrop(object sender, DragEventArgs e)
        //{
            

        //        Object item = (object)e.Data;
        //        Console.WriteLine(item);

        //        // Perform drag-and-drop, depending upon the effect.
        //        if (e.Effect == DragDropEffects.Copy ||
        //            e.Effect == DragDropEffects.Move)
        //        {
        //            swapEmpty(selectedLabel);
        //        }

          
        //}


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
                    cell.MouseDown += new MouseEventHandler(this.label_MouseDown);
                    //cell.DragDrop += new DragEventHandler(this.groupCells_DragDrop);
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
            return groupCells.GetChildAtPoint(new Point(emptyCell.Location.X + cellSize.Width, emptyCell.Location.Y)) as Label;  
        }

        void moveLeft()
        {
            if (emptyCell.Location.X == startPointLocation.X + cellSize.Width * (NUMBERROW -1))
                return;

            swapEmpty(findLeft());
        }

        Label findRight()
        {
            return groupCells.GetChildAtPoint(new Point(emptyCell.Location.X - cellSize.Width, emptyCell.Location.Y)) as Label;   
        }

        void moveRight()
        {
            if (emptyCell.Location.X == startPointLocation.X )
                return;

            swapEmpty(findRight());

        }

        Label findUp()
        {
            return groupCells.GetChildAtPoint(new Point(emptyCell.Location.X, emptyCell.Location.Y + cellSize.Height)) as Label;
        }

        void moveUp()
        {
            if (emptyCell.Location.Y == startPointLocation.Y + cellSize.Height * (NUMBERCOLUMN - 1))
                return;

            swapEmpty(findUp());

        }

        Label findDown()
        {     
            return groupCells.GetChildAtPoint(new Point(emptyCell.Location.X, emptyCell.Location.Y - cellSize.Height)) as Label; 
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
