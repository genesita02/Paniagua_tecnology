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
    public partial class Inventario : Form
    {
        private readonly string connectionString = @"Data Source=CHERRYY-E5440;Initial Catalog=Gestion_ventas;Integrated Security=True;TrustServerCertificate=True";
        public Inventario()
        {
            InitializeComponent();
            Datos();
        }
        private void Datos()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Inventario;";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable table = new DataTable();

                try
                {
                    adapter.Fill(table);
                    dgv_inventario.DataSource = table;
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
                    string Descripcion = txt_descrip.Text;
                    string Nombre = cbx_inve.Text;
                    int id_inve = 1;
                    string precio = txt_precio.Text;
                    string cantidadTexto = txt_cantidad.Text;
                    decimal cantidad;

                    if (decimal.TryParse(cantidadTexto, out cantidad))
                    {
                        // La conversión fue exitosa, puedes usar cantidad como un decimal
                    }
                    else
                    {
                        // La conversión falló, manejar el error
                        MessageBox.Show("El valor ingresado no es un número decimal válido.");
                    }




                    // Cadena SQL usando parámetros
                    string cadena = "INSERT INTO inventario (codigo, descripcion, cantidad, precio)" +
                                    "VALUES (@codigo, @descripcion, @cantidad, @precio);";

                    // Creación del comando con parámetros
                    using (SqlCommand comando = new SqlCommand(cadena, conexion))
                    {
                        comando.Parameters.AddWithValue("@descripcion", Descripcion);
                        comando.Parameters.AddWithValue("@nombre_categoria", Nombre);
                        comando.Parameters.AddWithValue("@codigo", id_inve);
                        comando.Parameters.AddWithValue("@cantidad", cadena);
                        comando.Parameters.AddWithValue("@precio", precio);
                        comando.Parameters.AddWithValue("@cantidad", cantidad);


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
                    string query = "DELETE FROM inventario WHERE id_inventario = @id_inventario;";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id_inventario", txt_codigo.Text);

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
            string Descripcion = txt_descrip.Text;
            string Nombre = cbx_inve.Text;
            int id_inve = 1;
            string precio = txt_precio.Text;
            string cantidadTexto = txt_cantidad.Text;
            decimal cantidad;

            if (decimal.TryParse(cantidadTexto, out cantidad))
            {
                // La conversión fue exitosa, puedes usar cantidad como un decimal
            }
            else
            {
                // La conversión falló, manejar el error


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Categoria SET nombre_categoria = @nombre_categoria,descripcion_categoria = @descripcion_categoria\r\n";

                    SqlCommand comando = new SqlCommand(query, connection);
                    comando.Parameters.AddWithValue("@descripcion", Descripcion);
                    comando.Parameters.AddWithValue("@nombre_categoria", Nombre);
                    comando.Parameters.AddWithValue("@codigo", id_inve);
                    comando.Parameters.AddWithValue("@cantidad", cantidad);
                    comando.Parameters.AddWithValue("@precio", precio);

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
        }
    }
}
