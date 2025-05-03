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
        private readonly string connectionString = "Data Source=LAB411-025\\SQLEXPRESS; Initial Catalog=facturaDB; User ID=Jhersin; Password=jhersin123; TrustServerCertificate=True;";

        public List<Product> ObtenerProductos()
        {
            var lista = new List<Product>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("USP_ListarProductos", connection);
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                Console.WriteLine("Conexión exitosa.");

                while (reader.Read())
                {
                    lista.Add(new Product
                    {
                        ProductId = Convert.ToInt32(reader["Product_id"]),
                        Name = reader["Name"].ToString() ?? "",
                        Price = Convert.ToDecimal(reader["Price"]),
                        Stock = Convert.ToInt32(reader["Stock"]),
                        Active = Convert.ToBoolean(reader["Active"])
                    });
                }

                reader.Close();
            }

            return lista;
        }

        public void RegistrarProducto(Product product)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("USP_InsertarProducto", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Stock", product.Stock);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

    }
}
