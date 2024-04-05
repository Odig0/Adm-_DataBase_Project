using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Administracion_Base_de_datos_Proyecto_Final.Pages.Client
{

    public class HabitacionModel : PageModel
    {
        public List<Habitacion> listHabitaciones = new List<Habitacion>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Habitacion";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Habitacion habitacion = new Habitacion();
                                habitacion.NumeroHabitacion = reader.GetInt32(0);
                                habitacion.HotelID = reader.GetInt32(1);
                                habitacion.TipoID = reader.GetInt32(2);
                                habitacion.Estado = reader.GetString(3);
                                listHabitaciones.Add(habitacion);
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

    public class Habitacion
    {
        public int NumeroHabitacion;
        public int HotelID;
        public int TipoID;
        public string Estado;
    }
}
