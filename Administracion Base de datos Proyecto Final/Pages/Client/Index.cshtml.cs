using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Administracion_Base_de_datos_Proyecto_Final.Pages.Client
{
    public class IndexModel : PageModel
    {
        public List<Hotel> listHoteles = new List<Hotel>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Hotel";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Hotel hotel = new Hotel();
                                hotel.HotelID = reader.GetInt32(0);
                                hotel.Nombre = reader.GetString(1);
                                hotel.Direccion = reader.GetString(2);
                                hotel.Telefono = reader.GetString(3);
                                hotel.CorreoElectronico = reader.GetString(4);
                                hotel.Estrellas = reader.GetInt32(5);
                                hotel.HoraDeEntrada = reader.GetTimeSpan(6);
                                hotel.HoraDeSalida = reader.GetTimeSpan(7);
                                listHoteles.Add(hotel);
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


    public class Hotel
    {
        public int HotelID;
        public string Nombre;
        public string Direccion;
        public string Telefono;
        public string CorreoElectronico;
        public int Estrellas;
        public TimeSpan HoraDeEntrada;
        public TimeSpan HoraDeSalida;
    }
}
