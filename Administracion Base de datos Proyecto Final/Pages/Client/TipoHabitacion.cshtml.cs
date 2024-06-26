using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Administracion_Base_de_datos_Proyecto_Final.Pages.Client
{
    public class TipoHabitaciónModel : PageModel
    {
        public List<TipoHabitacion> listTipoHabitaciones = new List<TipoHabitacion>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM TipoHabitacion ";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TipoHabitacion tipoHabitación = new TipoHabitacion();
                                tipoHabitación.TipoID = reader.GetInt32(0);
                                tipoHabitación.Nombre = reader.GetString(1);
                                tipoHabitación.Descripcion = reader.GetString(2);
                                tipoHabitación.PrecioPorNoche = reader.GetDecimal(3);
                                tipoHabitación.Capacidad = reader.GetInt32(4);
                                listTipoHabitaciones.Add(tipoHabitación);
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


    public class TipoHabitacion
    {
        public int TipoID;
        public string Nombre;
        public string Descripcion;
        public decimal PrecioPorNoche;
        public int Capacidad;
    }
}
