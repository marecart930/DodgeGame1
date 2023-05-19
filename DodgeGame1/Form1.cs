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
        Rectangle hero = new Rectangle(500, 550, 30, 30);
        Rectangle hero2 = new Rectangle(100, 550, 30, 30);
        int heroSpeed = 10;

        //Ball variables
        int ballSpeed = 8;
        int ballSize = 30;

        //List of balls
        List<Rectangle> balllist = new List<Rectangle>();

        //int score = 0;
        int time = 1000;
        int heroScore = 0;
        int hero2Score = 0;

        // bool leftDown = false;
        //bool rightDown = false;
        bool upDown = false;
        bool downDown = false;
        bool wDown = false;
        bool sDown = false;

        SolidBrush whiteBrush = new SolidBrush(Color.White);

        Random randGen = new Random();
        int randValue = 0;

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
            scoreLabel.Text = $"Score: {heroScore}";
            score2Label.Text = $"Score: {hero2Score}";


            //draw ground
            //e.Graphics.FillRectangle(whiteBrush, 0, this.Height - groundHeight, this.Width, groundHeight);

            //draw hero
            e.Graphics.FillRectangle(whiteBrush, hero);
            e.Graphics.FillRectangle(whiteBrush, hero2);

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
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
              
                case Keys.Up:
                    upDown = false;
                    break;
                case Keys.Down:
                    downDown = false;
                    break;
            }
        }

        private void Form1_KeyDown_1(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                
                case Keys.Up:
                    upDown = true;
                    break;
                case Keys.Down:
                    downDown = true;
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (upDown == true && hero.Y > 0)
            {
                hero.Y -= heroSpeed;
            }
            if (downDown == true && hero.Y > 0)
            {
                hero.Y += heroSpeed;
            }
            if (wDown == true && hero2.Y > 0)
            {
                hero2.Y -= heroSpeed;
            }
            if (sDown == true && hero2.Y > 0)
            {
                hero2.Y += heroSpeed;
            }
            //move the ball rectangles
            for (int i = 0; i < balllist.Count; i++)
            {
                int y = balllist[i].X + ballSpeed;
                balllist[i] = new Rectangle(y, balllist[i].Y,  ballSize, ballSize);
            }

            //generate new ball object

            randValue = randGen.Next(1, 100);

            if (randValue <= 30)
            {
                randValue = randGen.Next(0, 550);

                Rectangle ball = new Rectangle(0, randValue, ballSize, ballSize);
                balllist.Add(ball);
            }

            //remove the ball if it is off screen

            for (int i = 0; i < balllist.Count; i++)
            {
                if (balllist[i].X >= 600)
                {
                    balllist.RemoveAt(i);
                }
            }

            //check if ball has hit paddle
            for (int i = 0; i < balllist.Count; i++)
            {
                if (balllist[i].IntersectsWith(hero))
                {
                    hero.Y = 550;
                    balllist.RemoveAt(i);  
                }
                if (balllist[i].IntersectsWith(hero2))
                {
                    hero2.Y = 550;
                    balllist.RemoveAt(i);
                }
            }

            //check if hero has reached the top
            if (hero.Y < 5)
            {
                hero.Y = 550;
                heroScore += 1;
            }
            if (hero2.Y < 5)
            {
                hero2.Y = 550;
                hero2Score += 1;
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

}

