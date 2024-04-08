using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Administracion_Base_de_datos_Proyecto_Final.Pages.Client.Edit
{
    public class EditReservaServiciosModel : PageModel
    {
        public ReservaServicio reservaServicio = new ReservaServicio();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            try
            {
                string idReservaServicio = Request.Query["IDReservaServicio"];
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT IDReservaServicio, IDServicio, IDReserva FROM ReservaServicio WHERE IDReservaServicio = @IDReservaServicio";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@IDReservaServicio", idReservaServicio);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                reservaServicio.IDReservaServicio = reader.GetInt32(0);
                                reservaServicio.IDServicio = reader.GetInt32(1);
                                reservaServicio.IDReserva = reader.GetInt32(2);
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
            reservaServicio.IDReservaServicio = Convert.ToInt32(Request.Form["IDReservaServicio"]);
            reservaServicio.IDServicio = Convert.ToInt32(Request.Form["IDServicio"]);
            reservaServicio.IDReserva = Convert.ToInt32(Request.Form["IDReserva"]);

            try
            {
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE ReservaServicio " +
                                 "SET IDServicio = @IDServicio, IDReserva = @IDReserva " +
                                 "WHERE IDReservaServicio = @IDReservaServicio";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@IDReservaServicio", reservaServicio.IDReservaServicio);
                        command.Parameters.AddWithValue("@IDServicio", reservaServicio.IDServicio);
                        command.Parameters.AddWithValue("@IDReserva", reservaServicio.IDReserva);

                        command.ExecuteNonQuery();
                    }
                }

                successMessage = "Reserva de Servicio actualizada correctamente";
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            Response.Redirect("/Client/ReservaServicios");
        }
    }
}