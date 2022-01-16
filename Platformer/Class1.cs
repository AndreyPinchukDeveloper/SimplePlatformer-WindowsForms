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
    public partial class Class1:PictureBox 
    {
        public void turn(PictureBox e)
        {
            e.Graphics.TranslateTransform(Width / 2.0f, Height / 2.0f + 400);
        }
    }
}
