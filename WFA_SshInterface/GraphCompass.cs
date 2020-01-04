using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppKcoTestSsh
{
    class GraphCompass
    {
        private Graphics MonDessin = null;
        private Color CouleurDeFond = Color.White;

        Image ImgCompass = WindowsFormsAppKcoTestSsh.Properties.Resources.compass;
        Image ImgHeading = WindowsFormsAppKcoTestSsh.Properties.Resources.heading;

        Rectangle RectCompass, RectHeading, RectClip;

        public GraphCompass(PictureBox picBox)
        {
            MonDessin = picBox.CreateGraphics();
            MonDessin.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            CouleurDeFond = picBox.BackColor;

            RectCompass = new Rectangle(0, 0, picBox.Size.Width, picBox.Size.Height);
            RectHeading = new Rectangle(0, 100, picBox.Size.Width, picBox.Size.Height);
            RectClip = new Rectangle(0, 0, picBox.Size.Width, picBox.Size.Height/2 + 50);
            MonDessin.Clip = new Region(RectClip);
        }

        public void DessineLeFond()
        {
            MonDessin.Clear(CouleurDeFond);
            MonDessin.DrawImage(ImgCompass, RectCompass);
            MonDessin.DrawImage(ImgHeading, RectHeading);
        }
        public void DessineLeFond(double angle)
        {
            MonDessin.Clear(CouleurDeFond);

            MonDessin.TranslateTransform((float)ImgCompass.Width / 2, (float)ImgCompass.Height / 2);
            MonDessin.RotateTransform((float)angle);
            MonDessin.TranslateTransform(-(float)ImgCompass.Width / 2, -(float)ImgCompass.Height / 2);
            MonDessin.InterpolationMode = InterpolationMode.HighQualityBicubic;
            MonDessin.DrawImage(ImgCompass, RectCompass);
            //MonDessin.DrawImageUnscaled(ImgCompass, RectCompass);
            //MonDessin.DrawImageUnscaledAndClipped(ImgCompass, RectCompass);
            MonDessin.ResetTransform();
            MonDessin.DrawImage(ImgHeading, RectHeading);
        }


    }
}
