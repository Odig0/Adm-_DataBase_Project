using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;

namespace Administracion_Base_de_datos_Proyecto_Final.Pages.Client.Edit
{
    public class EditHabitacionModel : PageModel
    {
        public Habitacion habitacion = new Habitacion();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            try
            {
                string numeroHabitacion = Request.Query["NumeroHabitacion"];
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT NumeroHabitacion, HotelID, TipoID, Estado FROM Habitacion WHERE NumeroHabitacion = @NumeroHabitacion";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@NumeroHabitacion", numeroHabitacion);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                habitacion.NumeroHabitacion = reader.GetInt32(0);
                                habitacion.HotelID = reader.GetInt32(1);
                                habitacion.TipoID = reader.GetInt32(2);
                                habitacion.Estado = reader.GetString(3);
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
            if (!int.TryParse(Request.Form["HotelID"], out int hotelId))
            {
                errorMessage = "El ID del hotel proporcionado no es válido.";
                return;
            }
            habitacion.NumeroHabitacion = Convert.ToInt32(Request.Form["NumeroHabitacion"]);
            habitacion.HotelID = Convert.ToInt32(Request.Form["HotelID"]);
            habitacion.TipoID = Convert.ToInt32(Request.Form["TipoID"]);
            habitacion.Estado = Request.Form["Estado"];

            try
            {
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE Habitacion " +
                                 "SET HotelID = @HotelID, TipoID = @TipoID, Estado = @Estado " +
                                 "WHERE NumeroHabitacion = @NumeroHabitacion";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@NumeroHabitacion", habitacion.NumeroHabitacion);
                        command.Parameters.AddWithValue("@HotelID", habitacion.HotelID);
                        command.Parameters.AddWithValue("@TipoID", habitacion.TipoID);
                        command.Parameters.AddWithValue("@Estado", habitacion.Estado);

                        command.ExecuteNonQuery();
                    }
                }

                successMessage = "Habitación actualizada correctamente";
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            Response.Redirect("/Client/Habitacion");
        }
    }
}
