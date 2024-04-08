using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Administracion_Base_de_datos_Proyecto_Final.Pages.Client.Edit
{
    public class EditServiciosModel : PageModel
    {
        public Servicio servicio = new Servicio();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            try
            {
                string idServicio = Request.Query["IDServicio"];
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT IDServicio, Nombre, Descripcion, Precio FROM Servicio WHERE IDServicio = @IDServicio";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@IDServicio", idServicio);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                servicio.IDServicio = reader.GetInt32(0);
                                servicio.Nombre = reader.GetString(1);
                                servicio.Descripcion = reader.GetString(2);
                                servicio.Precio = reader.GetDecimal(3);
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
            servicio.IDServicio = Convert.ToInt32(Request.Form["IDServicio"]);
            servicio.Nombre = Request.Form["Nombre"];
            servicio.Descripcion = Request.Form["Descripcion"];
            servicio.Precio = Convert.ToDecimal(Request.Form["Precio"]);

            try
            {
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE Servicio " +
                                 "SET Nombre = @Nombre, Descripcion = @Descripcion, Precio = @Precio " +
                                 "WHERE IDServicio = @IDServicio";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@IDServicio", servicio.IDServicio);
                        command.Parameters.AddWithValue("@Nombre", servicio.Nombre);
                        command.Parameters.AddWithValue("@Descripcion", servicio.Descripcion);
                        command.Parameters.AddWithValue("@Precio", servicio.Precio);

                        command.ExecuteNonQuery();
                    }
                }

                successMessage = "Servicio actualizado correctamente";
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            Response.Redirect("/Client/Servicios");
        }
    }
}