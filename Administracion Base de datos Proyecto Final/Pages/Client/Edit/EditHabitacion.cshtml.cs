using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
                string numeroHabitacion = Request.Query["NúmeroHabitacion"];
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT NúmeroHabitación, HotelID, TipoID, Estado FROM Habitación WHERE NúmeroHabitación = @NúmeroHabitación";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@NúmeroHabitación", numeroHabitacion);

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
            habitacion.NumeroHabitacion = Convert.ToInt32(Request.Form["NúmeroHabitación"]);
            habitacion.HotelID = Convert.ToInt32(Request.Form["HotelID"]);
            habitacion.TipoID = Convert.ToInt32(Request.Form["TipoID"]);
            habitacion.Estado = Request.Form["Estado"];

            try
            {
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE Habitación " +
                                 "SET HotelID = @HotelID, TipoID = @TipoID, Estado = @Estado " +
                                 "WHERE NúmeroHabitación = @NúmeroHabitación";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@NúmeroHabitación", habitacion.NumeroHabitacion);
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
        }
    }
}