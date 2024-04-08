using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Administracion_Base_de_datos_Proyecto_Final.Pages.Client.Edit
{
    public class EditReservaModel : PageModel
    {
        public Reserva reserva = new Reserva();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            try
            {
                string idReserva = Request.Query["IDReserva"];
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT IDReserva, IDHuesped, NumeroHabitacion, FechaDeEntrada, FechaDeSalida, PrecioTotal FROM Reserva WHERE IDReserva = @IDReserva";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@IDReserva", idReserva);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                reserva.IDReserva = reader.GetInt32(0);
                                reserva.IDHuesped = reader.GetInt32(1);
                                reserva.NumeroHabitacion = reader.GetInt32(2);
                                reserva.FechaDeEntrada = reader.GetDateTime(3);
                                reserva.FechaDeSalida = reader.GetDateTime(4);
                                reserva.PrecioTotal = reader.GetDecimal(5);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            reserva.IDReserva = Convert.ToInt32(Request.Form["IDReserva"]);
            reserva.IDHuesped = Convert.ToInt32(Request.Form["IDHuesped"]);
            reserva.NumeroHabitacion = Convert.ToInt32(Request.Form["NumeroHabitacion"]);
            reserva.FechaDeEntrada = DateTime.Parse(Request.Form["FechaDeEntrada"]);
            reserva.FechaDeSalida = DateTime.Parse(Request.Form["FechaDeSalida"]);
            reserva.PrecioTotal = Convert.ToDecimal(Request.Form["PrecioTotal"]);

            try
            {
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE Reserva " +
                                 "SET IDHuesped = @IDHuesped, NumeroHabitacion = @NumeroHabitacion, FechaDeEntrada = @FechaDeEntrada, FechaDeSalida = @FechaDeSalida, PrecioTotal = @PrecioTotal " +
                                 "WHERE IDReserva = @IDReserva";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@IDReserva", reserva.IDReserva);
                        command.Parameters.AddWithValue("@IDHuesped", reserva.IDHuesped);
                        command.Parameters.AddWithValue("@NumeroHabitacion", reserva.NumeroHabitacion);
                        command.Parameters.AddWithValue("@FechaDeEntrada", reserva.FechaDeEntrada);
                        command.Parameters.AddWithValue("@FechaDeSalida", reserva.FechaDeSalida);
                        command.Parameters.AddWithValue("@PrecioTotal", reserva.PrecioTotal);

                        command.ExecuteNonQuery();
                    }
                }

                successMessage = "Reserva actualizada correctamente";
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            Response.Redirect("/Client/Reserva");
        }
    }
}
