using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Administracion_Base_de_datos_Proyecto_Final.Pages.Client
{
    public class ReservaModel : PageModel
    {
        public List<Reserva> listReservas = new List<Reserva>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Reserva";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Reserva reserva = new Reserva();
                                reserva.IDReserva = reader.GetInt32(0);
                                reserva.IDHuesped = reader.GetInt32(1);
                                reserva.NumeroHabitacion = reader.GetInt32(2);
                                reserva.FechaDeEntrada = reader.GetDateTime(3);
                                reserva.FechaDeSalida = reader.GetDateTime(4);
                                reserva.PrecioTotal = reader.GetDecimal(5);
                                listReservas.Add(reserva);
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

    public class Reserva
    {
        public int IDReserva { get; set; }
        public int IDHuesped { get; set; }
        public int NumeroHabitacion { get; set; }
        public DateTime FechaDeEntrada { get; set; }
        public DateTime FechaDeSalida { get; set; }
        public decimal PrecioTotal { get; set; }
    }
}