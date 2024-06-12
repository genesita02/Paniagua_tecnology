using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;

namespace Paniagua_tecnology
{
    public partial class Menu : Form
    {
        private readonly string connectionString = @"Data Source=CHERRYY-E5440;Initial Catalog=Gestion_ventas;Integrated Security=True;TrustServerCertificate=True";
        public Menu()
        {
            InitializeComponent();

        }

        private void btnslide_Click(object sender, EventArgs e)
        {
            if (barravertical.Width == 250)
            {
                barravertical.Width = 70;
            }
            else
            {
                barravertical.Width = 250;
            }
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        

        private void AbrirFormInPanel(object Formhijo)
        {
            if (this.panelContenedor2.Controls.Count > 0)
                this.panelContenedor2.Controls.RemoveAt(0);
            Form fh = Formhijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panelContenedor2.Controls.Add(fh);
            this.panelContenedor2.Tag = fh;
            fh.Show();

        }

        private void btnmaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnmaximizar.Enabled = false;
           btnrestaurar.Enabled = true;
        }

        private void btnrestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState= FormWindowState.Normal;
            btnmaximizar.Enabled = true;
            btnrestaurar.Enabled= false;
        }

        private void btnminisar_Click(object sender, EventArgs e)
        {
            this.WindowState =FormWindowState.Minimized;
        }

        private void barravertical_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void btncliente_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel(new Cliente());
        }

        private void btnfacturacion_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel(new Facturacion());
        }

        private void btnCategoria_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel(new Categoria());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel(new Inventario());
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("oh,tan rapido te vas?😔", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string nombre = System.DateTime.Today.Day.ToString() + "-" + System.DateTime.Today.Month.ToString() + "-" + System.DateTime.Today.Year.ToString() + "-" + System.DateTime.Now.Hour.ToString() + "-" + System.DateTime.Now.Minute.ToString() + "-" + System.DateTime.Now.Second.ToString() + "-";
            string consulta = "BACKUP DATABASE [Gestion_ventas] TO  DISK = N'C:\\Program Files\\Microsoft SQL Server\\MSSQL15.MSSQLSERVER\\MSSQL\\Backup\\Backup\\Gestion ventas"+nombre+".back' WITH NOFORMAT, NOINIT,  NAME = N'Gestion_ventas-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(consulta, conn);
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("El backup fue realizado exitosamente");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
        }
    }
}
