using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Administracion_Base_de_datos_Proyecto_Final.Pages.Client
{
    public class InventarioModel : PageModel
    {
        public List<Inventario> listInventario = new List<Inventario>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Inventario";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Inventario inventario = new Inventario();
                                inventario.IDInventario = reader.GetInt32(0);
                                inventario.NumeroHabitacion = reader.GetInt32(1);
                                inventario.NombreItem = reader.GetString(2);
                                inventario.Cantidad = reader.GetInt32(3);
                                listInventario.Add(inventario);
                            }
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }

    public class Inventario
    {
        public int IDInventario { get; set; }
        public int NumeroHabitacion { get; set; }
        public string NombreItem { get; set; }
        public int Cantidad { get; set; }
    }
}