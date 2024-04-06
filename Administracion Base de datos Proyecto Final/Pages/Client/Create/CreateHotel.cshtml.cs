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
            Hotel.Nombre = Request.Form["nombre"];
            Hotel.CorreoElectrónico = Request.Form["email"];
            Hotel.Teléfono = Request.Form["telefono"];
            Hotel.Dirección = Request.Form["direccion"];
            Hotel.Estrellas = Convert.ToInt32(Request.Form["estrellas"]);
            string horaDeEntradaForm = Request.Form["horaDeEntrada"];
            Hotel.HoraDeEntrada = !string.IsNullOrEmpty(horaDeEntradaForm) ? TimeSpan.Parse(horaDeEntradaForm) : TimeSpan.FromHours(8); // Por ejemplo, 8:00 AM

            // Obtener la hora de salida del formulario y asignar un valor predeterminado si está vacía
            string horaDeSalidaForm = Request.Form["horaDeSalida"];
            Hotel.HoraDeSalida = !string.IsNullOrEmpty(horaDeSalidaForm) ? TimeSpan.Parse(horaDeSalidaForm) : TimeSpan.FromHours(17); // Por ejemplo, 5:00 PM


            if (string.IsNullOrEmpty(Hotel.Nombre) || string.IsNullOrEmpty(Hotel.CorreoElectrónico) ||
                string.IsNullOrEmpty(Hotel.Teléfono) || string.IsNullOrEmpty(Hotel.Dirección))
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
                    string sql = "INSERT INTO Hotel (Nombre, Dirección, Teléfono, CorreoElectrónico, Estrellas, HoraDeEntrada, HoraDeSalida) " +
                                 "VALUES (@nombre, @direccion, @telefono, @correoElectronico, @estrellas, @horaEntrada, @horaSalida)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nombre", Hotel.Nombre);
                        command.Parameters.AddWithValue("@direccion", Hotel.Dirección);
                        command.Parameters.AddWithValue("@telefono", Hotel.Teléfono);
                        command.Parameters.AddWithValue("@correoElectronico", Hotel.CorreoElectrónico);
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

            // Limpiar los campos después de agregar el hotel
            Hotel.Nombre = "";
            Hotel.Dirección = "";
            Hotel.Teléfono = "";
            Hotel.CorreoElectrónico = "";
            Hotel.Estrellas = 0;
            Hotel.HoraDeEntrada = TimeSpan.Zero;
            Hotel.HoraDeSalida = TimeSpan.Zero;

            successMessage = "Nuevo Hotel agregado";

            Response.Redirect("/Clientes/Index");
        }
    }
}
