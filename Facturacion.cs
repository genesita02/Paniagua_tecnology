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
    public partial class Facturacion : Form
    {
        private readonly string connectionString = @"Data Source=CHERRYY-E5440;Initial Catalog=Gestion_ventas;Integrated Security=True;TrustServerCertificate=True";
        public Facturacion()
        {
            InitializeComponent();
            Datos();
        }
        private void Datos()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Factura;";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable table = new DataTable();

                try
                {
                    adapter.Fill(table);
                    dgvFacturas.DataSource = table;
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
                    int id_cliente = 1;
                    string nombrecliente = txt_nom_cliente.Text;
                    string numeroidentidad = txt_num_identidad.Text;
                    string producto = txt_producto.Text;
                    string codigo = txt_codigo.Text;
                    int cantidad = int.Parse(txt_cantidad.Text);
                    string descripcion = txt_descrip.Text;
                    string RNC = txt_rnc.Text;
                    decimal precio = decimal.Parse(txt_precio.Text);

                    // Calcular el ITBIS y el total
                    decimal itbis = precio * cantidad * 0.18m;
                    decimal total = (precio * cantidad) + itbis;

                    // Cadena SQL usando parámetros
                    string cadena = "INSERT INTO factura (codigo_producto, producto, cantidad, descripcion, total, itbis, precio, nombre_cliente, num_identidad, RNC, id_cliente)" +
                                    "VALUES (@codigo_producto, @producto, @cantidad, @descripcion, @total, @itbis, @precio, @nombre_cliente, @num_identidad, @RNC, @id_cliente);";

                    // Creación del comando con parámetros
                    using (SqlCommand comando = new SqlCommand(cadena, conexion))
                    {
                        comando.Parameters.AddWithValue("@codigo_producto", codigo);
                        comando.Parameters.AddWithValue("@producto", producto);
                        comando.Parameters.AddWithValue("@cantidad", cantidad);
                        comando.Parameters.AddWithValue("@descripcion", descripcion);
                        comando.Parameters.AddWithValue("@total", total);
                        comando.Parameters.AddWithValue("@itbis", itbis);
                        comando.Parameters.AddWithValue("@precio", precio);
                        comando.Parameters.AddWithValue("@nombre_cliente", nombrecliente);
                        comando.Parameters.AddWithValue("@num_identidad", numeroidentidad);
                        comando.Parameters.AddWithValue("@RNC", RNC);
                        comando.Parameters.AddWithValue("@id_cliente", id_cliente);

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
                    string query = "DELETE FROM factura WHERE id_factura = @id_factura;";

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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btn_atua_Click(object sender, EventArgs e)
        {
            int id_cliente = 1;
            string nombrecliente = txt_nom_cliente.Text;
            string numeroidentidad = txt_num_identidad.Text;
            string producto = txt_producto.Text;
            string codigo = txt_codigo.Text;
            int cantidad = int.Parse(txt_cantidad.Text);
            string descripcion = txt_descrip.Text;
            string RNC = txt_rnc.Text;
            decimal precio = decimal.Parse(txt_precio.Text);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE factura SET cantidad = @cantidad, descripcion = @descripcion,total = @total, itbis = @itbis,precio = @precio, codigo_producto = @codigo_producto,nombre_cliente = @nombre_cliente,num_identidad = @num_identidad,RNC = @RNC  WHERE id_factura = @id_factura;";

                SqlCommand comando = new SqlCommand(query, connection);
                comando.Parameters.AddWithValue("@codigo_producto", codigo);
                comando.Parameters.AddWithValue("@producto", producto);
                comando.Parameters.AddWithValue("@cantidad", cantidad);
                comando.Parameters.AddWithValue("@descripcion", descripcion);
                comando.Parameters.AddWithValue("@precio", precio);
                comando.Parameters.AddWithValue("@nombre_cliente", nombrecliente);
                comando.Parameters.AddWithValue("@num_identidad", numeroidentidad);
                comando.Parameters.AddWithValue("@RNC", RNC);
                comando.Parameters.AddWithValue("@id_cliente", id_cliente);

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
