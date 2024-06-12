using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paniagua_tecnology
{
    public partial class Cliente : Form
    {
        private readonly string connectionString = @"Data Source=CHERRYY-E5440;Initial Catalog=Gestion_ventas;Integrated Security=True;TrustServerCertificate=True";
        public Cliente()
        {
            InitializeComponent();
            Datos();
        }
        private void Datos()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Cliente;";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable table = new DataTable();

                try
                {
                    adapter.Fill(table);
                    dgv_cliente.DataSource = table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los datos: " + ex.Message);
                }
            }
        }

        private void btn_guardar_Click(object sender, EventArgs e)
        {
            string connectionString = "server=CHERRYY-E5440;database=Gestion_ventas;integrated security=true";
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();

                    // Obtención de los datos desde los controles
                    int idcliente =1;
                    string Apellido = txt_apelldio.Text;
                    string Nombre = txt_nombre.Text;
                    string numeroIdentidadTexto = txt_num_identidad.Text;
                    decimal numeroIdentidad;


                    if (decimal.TryParse(numeroIdentidadTexto, out numeroIdentidad))
                    {
                        // La conversión fue exitosa, puedes usar numeroIdentidad como un decimal
                    }
                    else
                    {
                        // La conversión falló, manejar el error
                        MessageBox.Show("El valor ingresado no es un número decimal válido.");
                    }


                    // Cadena SQL usando parámetros
                    string cadena = "INSERT INTO Cliente (nombre, apellido, num_identidad)" +
                                    "VALUES (@nombre, @apellido, @num_identidad);";

                    // Creación del comando con parámetros
                    using (SqlCommand comando = new SqlCommand(cadena, conexion))
                    {
                        comando.Parameters.AddWithValue("@apellido", Apellido);
                        comando.Parameters.AddWithValue("@nombre", Nombre);
                        comando.Parameters.AddWithValue("", idcliente);
                        comando.Parameters.AddWithValue("@num_identidad", numeroIdentidadTexto);


                        // Ejecución del comando
                        comando.ExecuteNonQuery();
                    }

                    // Mensaje de confirmación
                    MessageBox.Show("Los datos se guardaron correctamente");
                }
                catch (Exception ex)
                {
                    // Manejo de errores
                    MessageBox.Show("Ocurrió un error: " + ex.Message);
                }
            }
        }

        private void btn_eliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Estás seguro de que deseas eliminar este producto?", "Confirmación", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Cliente WHERE id_cliente = @id_cliente;";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id_cliente", txt_id.Text);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Datos eliminado correctamente");
                            Datos();
                        }
                        else
                        {
                            MessageBox.Show("No se encontró ningún usuario");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al eliminar la categoria: " + ex.Message);
                    }
                }

            }
        }
    }
}
