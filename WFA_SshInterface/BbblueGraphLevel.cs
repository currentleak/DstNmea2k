using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsAppKcoTestSsh
{
    class BbblueGraphLevel
    {
        private Graphics monDessin = null;
        private Pen monCrayonLigne = new Pen(Color.Black);
        private Pen monCrayonBulle = new Pen(Color.Red, 10);
        private Size grosseurDeLaBulle = new Size(10, 10);
        private Point centre, maxRectSize;
        private Color CouleurDeFond = Color.White;
        private Point positionDeLaBulle;

        public BbblueGraphLevel(PictureBox picBox)
        {
            this.monDessin = picBox.CreateGraphics();
            this.CouleurDeFond = picBox.BackColor;

        }

        public void DessineLeFond()
        {
            
            
            //monDessin.Clear(CouleurDeFond);
            //DessineLesCercles();
        }

        private void DessineLesCercles()
        {
            monDessin.DrawLine(monCrayonLigne, 0, 0, 500, 500);

            


            //Rectangle rect = new Rectangle(0, 300, 0, 300);
            //monDessin.DrawEllipse(monCrayonLigne, rect);
            //rect = new Rectangle(50, 200, 50, 200);
            //monDessin.DrawEllipse(monCrayonLigne, rect);
        }

    }
}
