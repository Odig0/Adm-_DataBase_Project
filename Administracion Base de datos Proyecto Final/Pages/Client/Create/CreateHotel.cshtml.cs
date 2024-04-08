using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;
using Administracion_Base_de_datos_Proyecto_Final.Pages.Client;

namespace Administracion_Base_de_datos_Proyecto_Final.Pages.Client.Create
{
    public class CreateHotelModel : PageModel
    {
        public Hotel Hotel = new Hotel();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            // Obtener los valores de los campos del formulario
            Hotel.HotelID = Convert.ToInt32(Request.Form["HotelID"]);
            Hotel.Nombre = Request.Form["nombre"];
            Hotel.CorreoElectronico = Request.Form["email"];
            Hotel.Telefono = Request.Form["telefono"];
            Hotel.Direccion = Request.Form["direccion"];
            Hotel.Estrellas = Convert.ToInt32(Request.Form["estrellas"]);
            string horaDeEntradaForm = Request.Form["horaDeEntrada"];
            Hotel.HoraDeEntrada = !string.IsNullOrEmpty(horaDeEntradaForm) ? TimeSpan.Parse(horaDeEntradaForm) : TimeSpan.FromHours(8); // Por ejemplo, 8:00 AM

            // Obtener la hora de salida del formulario y asignar un valor predeterminado si est� vac�a
            string horaDeSalidaForm = Request.Form["horaDeSalida"];
            Hotel.HoraDeSalida = !string.IsNullOrEmpty(horaDeSalidaForm) ? TimeSpan.Parse(horaDeSalidaForm) : TimeSpan.FromHours(17); // Por ejemplo, 5:00 PM


            if (string.IsNullOrEmpty(Hotel.Nombre) || string.IsNullOrEmpty(Hotel.CorreoElectronico) ||
                string.IsNullOrEmpty(Hotel.Telefono) || string.IsNullOrEmpty(Hotel.Direccion))
            {
                errorMessage = "Todos los campos son requeridos";
                return;
            }

            // Guardar el nuevo hotel en la base de datos
            try
            {
                String connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Hotel (HotelID, Nombre, Direccion, Telefono, CorreoElectronico, Estrellas, HoraDeEntrada, HoraDeSalida) " +
                                 "VALUES (@HotelID, @nombre, @direccion, @telefono, @correoElectronico, @estrellas, @horaEntrada, @horaSalida)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        int hotelID = Convert.ToInt32(Request.Form["HotelID"]);
                        command.Parameters.AddWithValue("@HotelID", hotelID);
                        command.Parameters.AddWithValue("@nombre", Hotel.Nombre);
                        command.Parameters.AddWithValue("@direccion", Hotel.Direccion);
                        command.Parameters.AddWithValue("@telefono", Hotel.Telefono);
                        command.Parameters.AddWithValue("@correoElectronico", Hotel.CorreoElectronico);
                        command.Parameters.AddWithValue("@estrellas", Hotel.Estrellas);
                        command.Parameters.AddWithValue("@horaEntrada", Hotel.HoraDeEntrada);
                        command.Parameters.AddWithValue("@horaSalida", Hotel.HoraDeSalida);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            // Limpiar los campos despu�s de agregar el hotel
            Hotel.Nombre = "";
            Hotel.Direccion = "";
            Hotel.Telefono = "";
            Hotel.CorreoElectronico = "";
            Hotel.Estrellas = 0;
            Hotel.HoraDeEntrada = TimeSpan.Zero;
            Hotel.HoraDeSalida = TimeSpan.Zero;

            successMessage = "Nuevo Hotel agregado";

            Response.Redirect("/Client/Index");
        }
    }
}
