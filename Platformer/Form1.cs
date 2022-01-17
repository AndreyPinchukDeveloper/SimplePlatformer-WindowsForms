using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Platformer
{
    public partial class GhostAndOgr : Form
    {
        bool goLeft, goRight, jumping, isGameOver;

        int jumpSpeed;
        int force;
        int playerSpeed = 7;
        int score = 0;

        int horizontal1Speed = 5;
        int horizontal2Speed = 5;
        int vertical1Speed = 3;
        int vertical2Speed = 3;
        int vertical3Speed = 3;

        int ghostSpeed = 5;
        int ogrSpeed = 3;

        public GhostAndOgr()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + score;

            Player.Top += jumpSpeed;

            if (goLeft==true)
            {
                Player.Left -= playerSpeed;
            }
            if (goRight == true)
            {
                Player.Left += playerSpeed;
            }

            if (jumping==true&&force<0)
            {
                jumping = false;
            }
            if (jumping==true)
            {
                jumpSpeed = -32;
                force -= 1;
            }
            else
            {
                jumpSpeed = 10;
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {

                    if ((string)x.Tag =="platform")
                    {
                        if (Player.Bounds.IntersectsWith(x.Bounds))
                        {
                            force = 0;
                            Player.Top = x.Top - Player.Height;

                            if ((string)x.Name=="horizontal1"&&goLeft==false|| (string)x.Name == "horizontal1" && goRight == false)
                            {
                                Player.Left -= horizontal1Speed;//this for you can move with platform, in default version you fall
                            }
                            if ((string)x.Name=="horizontal2"&&goLeft==false|| (string)x.Name == "horizontal2" && goRight == false)
                            {
                                Player.Left -= horizontal2Speed;//this for you can move with platform, in default version you fall
                            }

                        }
                        x.BringToFront();
                    }
                    if ((string)x.Tag=="coin")
                    {
                        if (Player.Bounds.IntersectsWith(x.Bounds)&& x.Visible==true)
                        {
                            x.Visible = false;
                            score++;
                        }
                    }

                    if ((string)x.Tag=="enemy")
                    {
                        if (Player.Bounds.IntersectsWith(x.Bounds))
                        {
                            timer1.Stop();
                            isGameOver = true;
                            txtScore.Text = "Score: " + score + Environment.NewLine + "YOU DEAD";
                        }
                    }
                }
            }
            horizontal1.Left -= horizontal1Speed;
            if (horizontal1.Left<pictureBox8.Left||horizontal1.Right>=pictureBox8.Right)
            {
                horizontal1Speed = -horizontal1Speed;
            }
            
            horizontal2.Left -= horizontal2Speed;
            if (horizontal2.Left<0|| horizontal2.Left==pictureBox2.Right || horizontal2.Left + horizontal2.Width>this.ClientSize.Width)
            {
                horizontal2Speed = -horizontal2Speed;
            }
            
            vertical1.Top += vertical1Speed;
            if (vertical1.Top<330||vertical1.Top >448)
            {
                vertical1Speed = -vertical1Speed;
            }
            
            vertical2.Top += vertical2Speed;
            if (vertical2.Top==276||vertical2.Top == 561)
            {
                vertical2Speed = -vertical2Speed;
            }
            Ghost.Left += ghostSpeed;
            if (Ghost.Left<pictureBox8.Left||Ghost.Left+ Ghost.Width>pictureBox8.Left+pictureBox8.Width)
            {
                ghostSpeed = -ghostSpeed;
            }

            Ogr.Left += ogrSpeed;
            if (Ogr.Left < pictureBox3.Left ||Ogr.Left+ Ogr.Width > pictureBox3.Left + pictureBox3.Width)
            {
                ogrSpeed = -ogrSpeed;
            }

            if (Player.Top+Player.Height>this.ClientSize.Height+50)
            {
                timer1.Stop();
                isGameOver = true;
                txtScore.Text = "Score: " + score + Environment.NewLine + "YOU DEAD";
            }

            if (Player.Bounds.IntersectsWith(topDoor.Bounds)&& score==13)
            {
                timer1.Stop();
                isGameOver = true;
                txtScore.Text = "Score: " + score + Environment.NewLine + "Well done ! Good Luck !";
            }
            else if (Player.Bounds.IntersectsWith(topDoor.Bounds))
            {
                txtScore.Text = "Score: " + score + Environment.NewLine + "You do not have enough money !";
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
                
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Space && jumping==false)
            {
                jumping = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (jumping==true)
            {
                jumping = false;
            }

            if (e.KeyCode == Keys.Enter && isGameOver==true)
            {
                RestartGame();
            }
        }

        private void RestartGame()
        {
            jumping = false;
            goLeft = false;
            goRight = false;
            isGameOver = false;
            score = 0;

            txtScore.Text = "Score: " + score;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Visible ==false)
                {
                    x.Visible = true;
                }
            }

            Player.Left = 65;
            Player.Top = 580;

            Ghost.Left =301;

            Ogr.Left =269;

            vertical1.Top =449;
            vertical2.Top =276;
            vertical3.Top =197;
            horizontal1.Left =93;
            horizontal2.Left =223;

            timer1.Start();
        }
    }
}
