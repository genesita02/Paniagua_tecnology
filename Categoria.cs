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
    public partial class Categoria : Form
    {
        private readonly string connectionString = @"Data Source=CHERRYY-E5440;Initial Catalog=Gestion_ventas;Integrated Security=True;TrustServerCertificate=True";
        public Categoria()
        {
            InitializeComponent();
            Datos();
        }

        private void Datos()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Categoria;";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable table = new DataTable();

                try
                {
                    adapter.Fill(table);
                    dgv_categoria.DataSource = table;
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
                    string Descripcion = txt_producto.Text;
                    string Nombre = cbx_nombre.Text;
                    int id_categoria = 1;


                    // Cadena SQL usando parámetros
                    string cadena = "INSERT INTO Categoria (id_categoria, nombre_categoria, descripcion_categoria) " +
                                    "VALUES (@id_categoria, @nombre_categoria, @descripcion_categoria)";

                    // Creación del comando con parámetros
                    using (SqlCommand comando = new SqlCommand(cadena, conexion))
                    {
                        comando.Parameters.AddWithValue("@descripcion_categoria", Descripcion);
                        comando.Parameters.AddWithValue("@nombre_categoria", Nombre);
                        comando.Parameters.AddWithValue("@id_categoria",id_categoria);


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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btn_eliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Estás seguro de que deseas eliminar este producto?", "Confirmación", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Categoria WHERE id_categoria = @id_categoria";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id_categoria", txt_id.Text);

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

        private void btn_nuevo_Click(object sender, EventArgs e)
        {
            string Descripcion = txt_producto.Text;
            string Nombre = cbx_nombre.Text;
            int id_categoria = 1;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Categoria SET nombre_categoria = @nombre_categoria,descripcion_categoria = @descripcion_categoria\r\n";

                SqlCommand comando = new SqlCommand(query, connection);
                comando.Parameters.AddWithValue("@descripcion_categoria", Descripcion);
                comando.Parameters.AddWithValue("@nombre_categoria", Nombre);
                comando.Parameters.AddWithValue("@id_categoria", id_categoria); ;

                try
                {
                    connection.Open();
                    int rowsAffected = comando.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Datos actualizados correctamente");
                        Datos();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar : " + ex.Message);
                }
            }
        }

        private void btn_modificar_Click(object sender, EventArgs e)
        {

        }
    }
}
