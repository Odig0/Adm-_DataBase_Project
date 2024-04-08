using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Administracion_Base_de_datos_Proyecto_Final.Pages.Client.Edit
{
    public class EditHuespedModel : PageModel
    {
        public Huesped huesped = new Huesped();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            try
            {
                string idHuesped = Request.Query["IDHuesped"];
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT IDHuesped, Nombre, Apellido, FechaDeNacimiento, Direccion, Telefono, CorreoElectronico FROM Huesped WHERE IDHuesped = @IDHuesped";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@IDHuesped", idHuesped);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                huesped.IDHuesped = reader.GetInt32(0);
                                huesped.Nombre = reader.GetString(1);
                                huesped.Apellido = reader.GetString(2);
                                huesped.FechaDeNacimiento = reader.GetDateTime(3);
                                huesped.Direccion = reader.GetString(4);
                                huesped.Telefono = reader.GetString(5);
                                huesped.CorreoElectronico = reader.GetString(6);
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
            huesped.IDHuesped = Convert.ToInt32(Request.Form["IDHuesped"]);
            huesped.Nombre = Request.Form["Nombre"];
            huesped.Apellido = Request.Form["Apellido"];
            huesped.FechaDeNacimiento = Convert.ToDateTime(Request.Form["FechaDeNacimiento"]);
            huesped.Direccion = Request.Form["Direccion"];
            huesped.Telefono = Request.Form["Telefono"];
            huesped.CorreoElectronico = Request.Form["CorreoElectronico"];

            try
            {
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE Huesped " +
                                 "SET Nombre = @Nombre, Apellido = @Apellido, FechaDeNacimiento = @FechaDeNacimiento, Direccion = @Direccion, Telefono = @Telefono, CorreoElectronico = @CorreoElectronico " +
                                 "WHERE IDHuesped = @IDHuesped";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@IDHuesped", huesped.IDHuesped);
                        command.Parameters.AddWithValue("@Nombre", huesped.Nombre);
                        command.Parameters.AddWithValue("@Apellido", huesped.Apellido);
                        command.Parameters.AddWithValue("@FechaDeNacimiento", huesped.FechaDeNacimiento);
                        command.Parameters.AddWithValue("@Direccion", huesped.Direccion);
                        command.Parameters.AddWithValue("@Telefono", huesped.Telefono);
                        command.Parameters.AddWithValue("@CorreoElectronico", huesped.CorreoElectronico);

                        command.ExecuteNonQuery();
                    }
                }

                successMessage = "Huesped actualizado correctamente";
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            Response.Redirect("/Client/Huesped");
        }
    }
}
