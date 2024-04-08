using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Administracion_Base_de_datos_Proyecto_Final.Pages.Client;


namespace Administracion_Base_de_datos_Proyecto_Final.Pages.Client.Create
{
    public class CreateHuesepdModel : PageModel
    {
        public Huesped Huesped = new Huesped();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            // Obtener los valores de los campos del formulario
            Huesped.Nombre = Request.Form["nombre"];
            Huesped.Apellido = Request.Form["apellido"];
            Huesped.FechaDeNacimiento = Convert.ToDateTime(Request.Form["fechaDeNacimiento"]);
            Huesped.Direccion = Request.Form["direccion"];
            Huesped.Telefono = Request.Form["telefono"];
            Huesped.CorreoElectronico = Request.Form["correoElectronico"];

            if (string.IsNullOrEmpty(Huesped.Nombre) || string.IsNullOrEmpty(Huesped.Apellido) ||
                Huesped.FechaDeNacimiento == DateTime.MinValue || string.IsNullOrEmpty(Huesped.Direccion) ||
                string.IsNullOrEmpty(Huesped.Telefono) || string.IsNullOrEmpty(Huesped.CorreoElectronico))
            {
                errorMessage = "Todos los campos son requeridos";
                return;
            }

            // Guardar el nuevo huésped en la base de datos
            try
            {
                String connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Huesped (Nombre, Apellido, FechaDeNacimiento, Direccion, Telefono, CorreoElectronico) " +
                                 "VALUES (@nombre, @apellido, @fechaNacimiento, @direccion, @telefono, @correoElectronico)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nombre", Huesped.Nombre);
                        command.Parameters.AddWithValue("@apellido", Huesped.Apellido);
                        command.Parameters.AddWithValue("@fechaNacimiento", Huesped.FechaDeNacimiento);
                        command.Parameters.AddWithValue("@direccion", Huesped.Direccion);
                        command.Parameters.AddWithValue("@telefono", Huesped.Telefono);
                        command.Parameters.AddWithValue("@correoElectronico", Huesped.CorreoElectronico);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            // Limpiar los campos después de agregar el huésped
            Huesped.Nombre = "";
            Huesped.Apellido = "";
            Huesped.FechaDeNacimiento = DateTime.MinValue;
            Huesped.Direccion = "";
            Huesped.Telefono = "";
            Huesped.CorreoElectronico = "";

            successMessage = "Nuevo Huésped agregado";

            Response.Redirect("/Client/Huesped");
        }
    }
}