using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Administracion_Base_de_datos_Proyecto_Final.Pages.Client.Edit
{
    public class EditTipoHabitacionModel : PageModel
    {
        public TipoHabitacion tipoHabitacion = new TipoHabitacion();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            try
            {
                string tipoID = Request.Query["TipoID"];
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT TipoID, Nombre, Descripcion, PrecioPorNoche, Capacidad FROM TipoHabitacion WHERE TipoID = @TipoID";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@TipoID", tipoID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                tipoHabitacion.TipoID = reader.GetInt32(0);
                                tipoHabitacion.Nombre = reader.GetString(1);
                                tipoHabitacion.Descripcion = reader.GetString(2);
                                tipoHabitacion.PrecioPorNoche = reader.GetDecimal(3);
                                tipoHabitacion.Capacidad = reader.GetInt32(4);
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
            tipoHabitacion.TipoID = Convert.ToInt32(Request.Form["TipoID"]);
            tipoHabitacion.Nombre = Request.Form["Nombre"];
            tipoHabitacion.Descripcion = Request.Form["Descripcion"];
            tipoHabitacion.PrecioPorNoche = Convert.ToDecimal(Request.Form["PrecioPorNoche"]);
            tipoHabitacion.Capacidad = Convert.ToInt32(Request.Form["Capacidad"]);

            try
            {
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE TipoHabitacion " +
                                 "SET Nombre = @Nombre, Descripcion = @Descripcion, PrecioPorNoche = @PrecioPorNoche, Capacidad = @Capacidad " +
                                 "WHERE TipoID = @TipoID";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@TipoID", tipoHabitacion.TipoID);
                        command.Parameters.AddWithValue("@Nombre", tipoHabitacion.Nombre);
                        command.Parameters.AddWithValue("@Descripcion", tipoHabitacion.Descripcion);
                        command.Parameters.AddWithValue("@PrecioPorNoche", tipoHabitacion.PrecioPorNoche);
                        command.Parameters.AddWithValue("@Capacidad", tipoHabitacion.Capacidad);

                        command.ExecuteNonQuery();
                    }
                }

                successMessage = "Tipo de habitación actualizado correctamente";
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            Response.Redirect("/Client/TipoHabitacion");
        }
    }
}