using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static Administracion_Base_de_datos_Proyecto_Final.Pages.Client.Hotel;

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
            Hotel.HotelID = Convert.ToInt32(Request.Form["hotelID"]);
            Hotel.Nombre = Request.Form["nombre"];
            Hotel.CorreoElectr�nico = Request.Form["email"];
            Hotel.Tel�fono = Request.Form["telefono"];
            Hotel.Direcci�n = Request.Form["direccion"];

            if (string.IsNullOrEmpty(Hotel.Nombre) || string.IsNullOrEmpty(Hotel.CorreoElectr�nico) ||
                string.IsNullOrEmpty(Hotel.Tel�fono) || string.IsNullOrEmpty(Hotel.Direcci�n))
            {
                errorMessage = "Todos los campos son requeridos";
                return;
            }

            // Guardar el nuevo cliente en la base de datos
            try
            {
                string connectionString = "Data Source=Localhost;Initial Catalog=Proyecto;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO clientes " +
                                 "(nombre,email,telefono,direccion) VALUES " +
                                 "(@nombre,@email,@telefono,@direccion)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nombre", Hotel.Nombre);
                        command.Parameters.AddWithValue("@email", Hotel.CorreoElectr�nico);
                        command.Parameters.AddWithValue("@telefono", Hotel.Tel�fono);
                        command.Parameters.AddWithValue("@direccion", Hotel.Direcci�n);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            // Limpiar los campos despu�s de agregar el cliente
            Hotel.Nombre = "";
            Hotel.CorreoElectr�nico = "";
            Hotel.Tel�fono = "";
            Hotel.Direcci�n = "";

            successMessage = "Nuevo Cliente agregado";

            Response.Redirect("/Clientes/Index");
        }
    }
}