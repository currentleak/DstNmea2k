using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppKcoTestSsh
{
    class GraphAngle
    {
        private Graphics MonDessin = null;
        private Color CouleurDeFond = Color.White;
        private Pen MonCrayon = new Pen(Color.Red, 10);

        private Size BulleSize = new Size(20, 20);
        private Point BullePos = new Point(0, 0);

        Image ImgRoll = WindowsFormsAppKcoTestSsh.Properties.Resources.niveau;
        Rectangle RectRoll;

        public GraphAngle(PictureBox picBox)
        {
            MonDessin = picBox.CreateGraphics();
            MonDessin.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            CouleurDeFond = picBox.BackColor;

            RectRoll = new Rectangle(0, 0, picBox.Size.Width, picBox.Size.Height);
            
            //MonDessin.Clip = new Region(RectClip);

        }

        public void DessineLeFond()
        {
            MonDessin.DrawImageUnscaled(ImgRoll, RectRoll);

            BullePos.X = RectRoll.Width / 2 - BulleSize.Width / 2;
            BullePos.Y = RectRoll.Height - BulleSize.Width;

            Rectangle r = new Rectangle(BullePos, BulleSize);
            MonDessin.DrawEllipse(MonCrayon, r);

        }
        public void DessineLeFond(double angle)
        {
            //MonDessin.Clear(CouleurDeFond);
            //MonDessin.TranslateTransform((float)ImgCompass.Width / 2, (float)ImgCompass.Height / 2);
            //MonDessin.RotateTransform((float)angle);
            //MonDessin.TranslateTransform(-(float)ImgCompass.Width / 2, -(float)ImgCompass.Height / 2);
            //MonDessin.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //MonDessin.DrawImage(ImgCompass, RectCompass);
            //MonDessin.DrawImageUnscaled(ImgCompass, RectCompass);
            //MonDessin.DrawImageUnscaledAndClipped(ImgCompass, RectCompass);
            //MonDessin.ResetTransform();
            MonDessin.DrawImage(ImgRoll, RectRoll);
        }



    }
}
