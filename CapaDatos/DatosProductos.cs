using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class DatosProductos
    {
        private readonly string connectionString = "Data Source=LAB411-020\\SQLEXPRESS; Initial Catalog=facturaDB; User ID=jhersin; Password=123; TrustServerCertificate=True;";

        // Leer (Listar productos)
        public List<Product> ObtenerProductos(int? productId = null)
        {
            var lista = new List<Product>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("sp_products_CRUD", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@action", "READ");
                command.Parameters.AddWithValue("@product_id", productId.HasValue ? (object)productId.Value : DBNull.Value);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                Console.WriteLine("Conexión exitosa.");

                while (reader.Read())
                {
                    lista.Add(new Product
                    {
                        ProductId = Convert.ToInt32(reader["product_id"]),
                        Name = reader["name"].ToString() ?? "",
                        Price = Convert.ToDecimal(reader["price"]),
                        Stock = Convert.ToInt32(reader["stock"]),
                        Active = Convert.ToBoolean(reader["active"])
                    });
                }

                reader.Close();
            }

            return lista;
        }

        // Crear (Registrar producto)
        public void RegistrarProducto(Product product)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("sp_products_CRUD", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@action", "CREATE");
                command.Parameters.AddWithValue("@name", product.Name);
                command.Parameters.AddWithValue("@price", product.Price);
                command.Parameters.AddWithValue("@stock", product.Stock);
                command.Parameters.AddWithValue("@active", product.Active);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Actualizar producto
        public void ActualizarProducto(Product product)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("sp_products_CRUD", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@action", "UPDATE");
                command.Parameters.AddWithValue("@product_id", product.ProductId);
                command.Parameters.AddWithValue("@name", product.Name);
                command.Parameters.AddWithValue("@price", product.Price);
                command.Parameters.AddWithValue("@stock", product.Stock);
                command.Parameters.AddWithValue("@active", product.Active);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Eliminar producto (eliminación lógica)
        public void EliminarProducto(int productId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("sp_products_CRUD", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@action", "DELETE");
                command.Parameters.AddWithValue("@product_id", productId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
