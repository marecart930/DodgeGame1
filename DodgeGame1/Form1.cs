using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DodgeGame1
{
    public partial class Form1 : Form
    {
        //Hero variables
        Rectangle hero = new Rectangle(280, 50, 60, 60);
        int heroSpeed = 10;

        //Ball variables
        int ballSpeed = 8;
        int ballSize = 10;

        //List of balls
        List<Rectangle> balllist = new List<Rectangle>();

        int score = 0;
        int time = 500;

        bool leftDown = false;
        bool rightDown = false;

        SolidBrush whiteBrush = new SolidBrush(Color.White);

        Random randGen = new Random();
        int randValue = 0;

        int groundHeight = 50;
        public Form1()
        {
            InitializeComponent();
            Rectangle ball = new Rectangle(20, 0, ballSize, ballSize);
            balllist.Add(ball);
        }

        private void Form1_Paint_1(object sender, PaintEventArgs e)
        {
            //update labels
            timeLabel.Text = $"Time Left: {time}";
            scoreLabel.Text = $"Score: {score}";

            //draw ground
            e.Graphics.FillRectangle(whiteBrush, 0, this.Height - groundHeight, this.Width, groundHeight);

            //draw hero
            e.Graphics.FillRectangle(whiteBrush, hero);

            //draw balls
            for (int i = 0; i < balllist.Count; i++)
            {
                e.Graphics.FillEllipse(whiteBrush, balllist[i]);
            }
        }

        private void Form1_KeyUp_1(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftDown = false;
                    break;
                case Keys.Right:
                    rightDown = false;
                    break;
            }
        }

        private void Form1_KeyDown_1(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftDown = true;
                    break;
                case Keys.Right:
                    rightDown = true;
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {

            if (rightDown == true && hero.X < this.Width - hero.Width)
            {
                hero.X += heroSpeed;
            }
            if (leftDown == true && hero.X > 0)
            {
                hero.X -= heroSpeed;
            }
            //move the ball rectangles
            for (int i = 0; i < balllist.Count; i++)
            {
                int y = balllist[i].Y + ballSpeed;
                balllist[i] = new Rectangle(balllist[i].X, y, ballSize, ballSize);
            }

            //generate new ball object

            randValue = randGen.Next(0, 100);

            if (randValue <= 10)
            {
                randValue = randGen.Next(0, this.Width - ballSize - 10);

                Rectangle ball = new Rectangle(randValue, 0, ballSize, ballSize);
                balllist.Add(ball);
            }

            //remove the ball if it hits ground

            for (int i = 0; i < balllist.Count; i++)
            {
                if (balllist[i].Y >= this.Height)
                {
                    balllist.RemoveAt(i);
                }
            }

            //check if ball has hit paddle
            for (int i = 0; i < balllist.Count; i++)
            {
                if (balllist[i].IntersectsWith(hero))
                {
                    balllist.RemoveAt(i);
                    score += 5;
                }
            }

            //check the time left
            time--;
            if (time == 0)
            {
                gameTimer.Stop();
            }


            //redraw the screen
            Refresh();

        }
    }

}

