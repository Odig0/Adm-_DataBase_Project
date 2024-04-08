using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Administracion_Base_de_datos_Proyecto_Final.Pages.Client.Edit
{
    public class EditHotelModel : PageModel
    {
        public Hotel hotel = new Hotel();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            try
            {
                string hotelID = Request.Query["HotelID"];
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT  Nombre, Dirección, Teléfono, CorreoElectrónico, Estrellas, HoraDeEntrada, HoraDeSalida FROM Hotel WHERE HotelID = @HotelID";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@HotelID", hotelID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                hotel.HotelID = reader.GetInt32(0);
                                hotel.Nombre = reader.GetString(1);
                                hotel.Direccion = reader.GetString(2);
                                hotel.Telefono = reader.GetString(3);
                                hotel.CorreoElectronico = reader.GetString(4);
                                hotel.Estrellas = reader.GetInt32(5);
                                hotel.HoraDeEntrada = reader.GetTimeSpan(6);
                                hotel.HoraDeSalida = reader.GetTimeSpan(7);
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
            hotel.HotelID = Convert.ToInt32(Request.Form["HotelID"]);
            hotel.Nombre = Request.Form["Nombre"];
            hotel.Direccion = Request.Form["Dirección"];
            hotel.Telefono = Request.Form["Teléfono"];
            hotel.CorreoElectronico = Request.Form["CorreoElectrónico"];
            hotel.Estrellas = Convert.ToInt32(Request.Form["Estrellas"]);
            hotel.HoraDeEntrada = TimeSpan.Parse(Request.Form["HoraDeEntrada"]);
            hotel.HoraDeSalida = TimeSpan.Parse(Request.Form["HoraDeSalida"]);

            try
            {
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE Hotel " +
                                 "SET Nombre = @Nombre, Dirección = @Dirección, Telefono = @Telefono, CorreoElectrónico = @CorreoElectrónico, Estrellas = @Estrellas, HoraDeEntrada = @HoraDeEntrada, HoraDeSalida = @HoraDeSalida " +
                                 "WHERE HotelID = @HotelID";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@HotelID", hotel.HotelID);
                        command.Parameters.AddWithValue("@Nombre", hotel.Nombre);
                        command.Parameters.AddWithValue("@Dirección", hotel.Direccion);
                        command.Parameters.AddWithValue("@Telefono", hotel.Telefono);
                        command.Parameters.AddWithValue("@CorreoElectrónico", hotel.CorreoElectronico);
                        command.Parameters.AddWithValue("@Estrellas", hotel.Estrellas);
                        command.Parameters.AddWithValue("@HoraDeEntrada", hotel.HoraDeEntrada);
                        command.Parameters.AddWithValue("@HoraDeSalida", hotel.HoraDeSalida);

                        command.ExecuteNonQuery();
                        var Telefono = " ";
                    }
                }

                successMessage = "Hotel actualizado correctamente";
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
    }
}