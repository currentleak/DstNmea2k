using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsAppKcoTestSsh
{
    class GraphLevel
    {
        private Graphics monDessin = null;
        private Pen monCrayonLigne = new Pen(Color.Black);
        private Pen monCrayonBulle = new Pen(Color.Red, 10);
        private Size grosseurDeLaBulle = new Size(10, 10);
        private Point centre, maxRectSize;
        private Color CouleurDeFond = Color.White;
        private Point positionDeLaBulle;

        public GraphLevel(PictureBox picBox)
        {
            monDessin = picBox.CreateGraphics();
            CouleurDeFond = picBox.BackColor;
            positionDeLaBulle = centre;
            ReSize(picBox);
            DessineLaBulle(positionDeLaBulle);
        }

        public void ReSize(PictureBox picBox)
        {
            monDessin = picBox.CreateGraphics();
            centre = new Point(picBox.Width / 2, (picBox.Height) / 2);
            maxRectSize = new Point(picBox.Width, picBox.Height);
            positionDeLaBulle = centre;
            DessineLeFond();
        }

        public void DessineLeFond()
        {
            monDessin.Clear(CouleurDeFond);
            DessineLesCercles();
        }

        public void BougeLaBulle(Point nouvellePosition)
        {
            this.positionDeLaBulle = nouvellePosition;
            DessineLeFond();
            DessineLaBulle();
        }

        public void BougeLaBulle(int xPos, int yPos)
        {
            Point bulle = new Point(xPos + centre.X, yPos + centre.Y);
            BougeLaBulle(bulle);
        }

        private void DessineLaBulle()
        {
            Rectangle rect = new Rectangle(positionDeLaBulle.X - grosseurDeLaBulle.Width / 2,
                positionDeLaBulle.Y - grosseurDeLaBulle.Height / 2,
                grosseurDeLaBulle.Width, grosseurDeLaBulle.Height);

            monDessin.DrawEllipse(monCrayonBulle, rect);
        }

        private void DessineLaBulle(Point nouvellePositionDeLaBulle)
        {
            positionDeLaBulle = nouvellePositionDeLaBulle;
            DessineLaBulle();
        }

        public int GetDimensionCercle()
        {
            int dimCercle;
            // on touve le plus grand carré faisable dans la zone graphique
            if (maxRectSize.X < maxRectSize.Y)
            {
                dimCercle = maxRectSize.X;
            }
            else
            {
                dimCercle = maxRectSize.Y;
            }
            return dimCercle;
        }
        private void DessineLesCercles()
        {
            int dimensionCercle = GetDimensionCercle();
            // dessine 3 cercles concentriques de diametre chaque fois 2 fois plus petit
            Rectangle rect = new Rectangle(centre.X - (dimensionCercle / 2), centre.Y - (dimensionCercle / 2), dimensionCercle, dimensionCercle);
            monDessin.DrawEllipse(monCrayonLigne, rect);
            rect = new Rectangle(centre.X - (dimensionCercle / 4), centre.Y - (dimensionCercle / 4), dimensionCercle / 2, dimensionCercle / 2);
            monDessin.DrawEllipse(monCrayonLigne, rect);
            rect = new Rectangle(centre.X - (dimensionCercle / 8), centre.Y - (dimensionCercle / 8), dimensionCercle / 4, dimensionCercle / 4);
            monDessin.DrawEllipse(monCrayonLigne, rect);
        }

    }
}
