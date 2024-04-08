using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Administracion_Base_de_datos_Proyecto_Final.Pages.Client.Edit
{
    public class EditPersonalModel : PageModel
    {
        public Personal personal = new Personal();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            try
            {
                string idPersonal = Request.Query["IDPersonal"];
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT IDPersonal, HotelID, Nombre, Apellido, Posicion, Salario, FechaDeNacimiento, Telefono, FechaDeContratacion FROM Personal WHERE IDPersonal = @IDPersonal";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@IDPersonal", idPersonal);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                personal.IDPersonal = reader.GetInt32(0);
                                personal.HotelID = reader.GetInt32(1);
                                personal.Nombre = reader.GetString(2);
                                personal.Apellido = reader.GetString(3);
                                personal.Posicion = reader.GetString(4);
                                personal.Salario = reader.GetDecimal(5);
                                personal.FechaDeNacimiento = reader.GetDateTime(6);
                                personal.Telefono = reader.GetString(7);
                                personal.FechaDeContratacion = reader.GetDateTime(8);
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
            personal.IDPersonal = Convert.ToInt32(Request.Form["IDPersonal"]);
            personal.HotelID = Convert.ToInt32(Request.Form["HotelID"]);
            personal.Nombre = Request.Form["Nombre"];
            personal.Apellido = Request.Form["Apellido"];
            personal.Posicion = Request.Form["Posicion"];
            personal.Salario = Convert.ToDecimal(Request.Form["Salario"]);
            personal.FechaDeNacimiento = DateTime.Parse(Request.Form["FechaDeNacimiento"]);
            personal.Telefono = Request.Form["Telefono"];
            personal.FechaDeContratacion = DateTime.Parse(Request.Form["FechaDeContratacion"]);

            try
            {
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE Personal " +
                                 "SET HotelID = @HotelID, Nombre = @Nombre, Apellido = @Apellido, Posicion = @Posicion, Salario = @Salario, FechaDeNacimiento = @FechaDeNacimiento, Telefono = @Telefono, FechaDeContratacion = @FechaDeContratacion " +
                                 "WHERE IDPersonal = @IDPersonal";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@IDPersonal", personal.IDPersonal);
                        command.Parameters.AddWithValue("@HotelID", personal.HotelID);
                        command.Parameters.AddWithValue("@Nombre", personal.Nombre);
                        command.Parameters.AddWithValue("@Apellido", personal.Apellido);
                        command.Parameters.AddWithValue("@Posicion", personal.Posicion);
                        command.Parameters.AddWithValue("@Salario", personal.Salario);
                        command.Parameters.AddWithValue("@FechaDeNacimiento", personal.FechaDeNacimiento);
                        command.Parameters.AddWithValue("@Telefono", personal.Telefono);
                        command.Parameters.AddWithValue("@FechaDeContratacion", personal.FechaDeContratacion);

                        command.ExecuteNonQuery();
                    }
                }

                successMessage = "Información de personal actualizada correctamente";
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            Response.Redirect("/Client/Personal");
        }
    }
}