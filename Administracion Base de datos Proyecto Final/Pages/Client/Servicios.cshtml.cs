using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Administracion_Base_de_datos_Proyecto_Final.Pages.Client
{
    public class ServiciosModel : PageModel
    {
        public List<Servicio> listServicios { get; set; } = new List<Servicio>();

        // Método OnGet para obtener los servicios de la base de datos
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Servicio";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Servicio servicio = new Servicio();
                                servicio.IDServicio = reader.GetInt32(0);
                                servicio.Nombre = reader.GetString(1);
                                servicio.Descripcion = reader.GetString(2);
                                servicio.Precio = reader.GetDecimal(3);
                                listServicios.Add(servicio);
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

    // Definición de la clase Servicio
    public class Servicio
    {
        public int IDServicio { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
    }
}