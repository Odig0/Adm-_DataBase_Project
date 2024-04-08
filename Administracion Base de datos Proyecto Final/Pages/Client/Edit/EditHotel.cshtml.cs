using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Net.Security;
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
            string hotelID = Request.Query["HotelID"];
            try
            {
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT Nombre, Direccion, Telefono, CorreoElectronico, Estrellas, HoraDeEntrada, HoraDeSalida FROM Hotel WHERE HotelID = @HotelID";
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
            hotel.Direccion = Request.Form["Direccion"];
            hotel.Telefono = Request.Form["Telefono"];
            hotel.CorreoElectronico = Request.Form["CorreoElectronico"];
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
                                 "SET Nombre = @Nombre, Direccion = @Direccion, Telefono = @Telefono, CorreoElectronico = @CorreoElectronico, Estrellas = @Estrellas, HoraDeEntrada = @HoraDeEntrada, HoraDeSalida = @HoraDeSalida " +
                                 "WHERE HotelID = @HotelID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@HotelID", hotel.HotelID);
                        command.Parameters.AddWithValue("@Nombre", hotel.Nombre);
                        command.Parameters.AddWithValue("@Direccion", hotel.Direccion);
                        command.Parameters.AddWithValue("@Telefono", hotel.Telefono);
                        command.Parameters.AddWithValue("@CorreoElectronico", hotel.CorreoElectronico);
                        command.Parameters.AddWithValue("@Estrellas", hotel.Estrellas);
                        command.Parameters.AddWithValue("@HoraDeEntrada", hotel.HoraDeEntrada);
                        command.Parameters.AddWithValue("@HoraDeSalida", hotel.HoraDeSalida);

                        command.ExecuteNonQuery();
                    }
                }
                successMessage = "Hotel actualizado correctamente";
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            Response.Redirect("/Client/Index");
        }
    }
}
