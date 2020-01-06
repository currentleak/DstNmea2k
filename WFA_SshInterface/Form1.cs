using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Renci.SshNet;

namespace WindowsFormsAppKcoTestSsh
{
    public partial class Form1 : Form
    {
        private const string address = "192.168.7.2";
        private const string user = "debian";
        private const string password = "temppwd";

        private static SshClient MonLienSecure = null;
        private static DstN2k MonDeviceDST = null;
        private static GraphCompass GCompass = null;
        private static GraphAngle GAngle = null;

        public Form1()
        {
            InitializeComponent();
            GCompass = new GraphCompass(pictureBoxMagneto);
            GAngle = new GraphAngle(pictureBoxAngle);
            MonLienSecure = new SshClient(address, user, password);
            MonDeviceDST = new DstN2k();
            MonDeviceDST.DstData.value = 0.0;
            MonDeviceDST.DstData.parameter = String.Empty;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                MonLienSecure.Connect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection error: \n" + ex.Message);
                Console.WriteLine("Connection error: \n" + ex.Message);
            }
            Console.WriteLine("SSH connection done");
            if (MonLienSecure.IsConnected)
            {  
                // **** config CAN bus and start it    ************************** //
                if(MonDeviceDST.CanBusId == "can0")
                {
                    new Task(() => CanDump(MonLienSecure, "candump can0")).Start();
                }
                else if (MonDeviceDST.CanBusId == "vcan0")
                {
                    new Task(() => CanDump(MonLienSecure, "candump vcan0")).Start();
                }
                else
                {
                    Console.WriteLine("CAN dump error.");
                    return;
                }
                Console.WriteLine("CAN dump running.");
            }
            Cursor.Current = Cursors.Default;

            //this.DoubleBuffered = true;
            //SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            //GCompass.DessineLeFond();
            //GAngle.DessineLeFond();
            //pictureBoxMagneto.Refresh();
        }

        private void CanDump(SshClient LienSsh, String commande)
        {
            var MaCommande = LienSsh.CreateCommand(commande);
            var Resultat = MaCommande.BeginExecute();

            using (var Lecteur = new StreamReader(MaCommande.OutputStream, Encoding.UTF8, true, 1024, true))
            {
                while (!Resultat.IsCompleted || !Lecteur.EndOfStream)
                {
                    var Reponse = Lecteur.ReadLine();
                    if (Reponse != null)
                    {
                        string str = MonDeviceDST.ParseNmea2kMessage(Reponse);
                        if (MonDeviceDST.DstData.parameter == "WaterTemperature") 
                        { 
                            labelTemp.Invoke((MethodInvoker)(() => labelTemp.Text = String.Format("{0:#00.0}", MonDeviceDST.DstData.value)));
                        }
                        else if (MonDeviceDST.DstData.parameter == "Depth")
                        {
                            labelDepth.Invoke((MethodInvoker)(() => labelDepth.Text = String.Format("{0:#00.00}", MonDeviceDST.DstData.value)));
                        }
                        else if (MonDeviceDST.DstData.parameter == "Speed")
                        {
                            labelSpeed.Invoke((MethodInvoker)(() => labelSpeed.Text = String.Format("{0:#00.00}", MonDeviceDST.DstData.value)));
                        }
                        else if (MonDeviceDST.DstData.parameter == "Heading")
                        {
                            labelHeading.Invoke((MethodInvoker)(() => labelHeading.Text = String.Format("{0:#000.0}", MonDeviceDST.DstData.value) + "°"));
                            GCompass.DessineLeFond(MonDeviceDST.DstData.value);
                        }
                        else if (MonDeviceDST.DstData.parameter == "AttitudeRoll")
                        {
                            labelRoll.Invoke((MethodInvoker)(() => labelRoll.Text = String.Format("{0:#000.0}", MonDeviceDST.DstData.value) + "°"));
                            GAngle.DessineLeFond(MonDeviceDST.DstData.value);
                        }
                        else if (MonDeviceDST.DstData.parameter == "Pressure")
                        {
                            labelPressure.Invoke((MethodInvoker)(() => labelPressure.Text = String.Format("{0:#000.0}", MonDeviceDST.DstData.value) + "hPa"));
                        }
                        Console.Write(MonDeviceDST.DstData.parameter + "= ");
                        Console.Write(String.Format("{0:#.00}", MonDeviceDST.DstData.value));
                        Console.WriteLine(" " + str);
                    }
                }
            }
            MaCommande.EndExecute(Resultat);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //pictureBoxMagneto.Refresh();
            //GAngle.DessineLeFond();
            //GCompass.DessineLeFond();
            //GCompass.DessineLeFond((float)trackBar1.Value);
        }

        private void pictureBoxMagneto_Paint(object sender, PaintEventArgs e)
        {
            //GCompass.DessineLeFond((float)trackBar1.Value);
            //GCompass.DessineLeFond();
        }

        private void pictureBoxAngle_Paint(object sender, PaintEventArgs e)
        {
            //GAngle.DessineLeFond();
        }
    }

}
