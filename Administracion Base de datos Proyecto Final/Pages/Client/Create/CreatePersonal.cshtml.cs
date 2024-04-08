using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Administracion_Base_de_datos_Proyecto_Final.Pages.Client.Create
{
    public class CreatePersonalModel : PageModel
    {
        public Personal Personal = new Personal();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            // Obtener los valores de los campos del formulario
            Personal.HotelID = Convert.ToInt32(Request.Form["hotelID"]);
            Personal.Nombre = Request.Form["nombre"];
            Personal.Apellido = Request.Form["apellido"];
            Personal.Posicion = Request.Form["posicion"];
            Personal.Salario = Convert.ToDecimal(Request.Form["salario"]);
            Personal.FechaDeNacimiento = Convert.ToDateTime(Request.Form["fechaDeNacimiento"]);
            Personal.Telefono = Request.Form["telefono"];
            Personal.FechaDeContratacion = Convert.ToDateTime(Request.Form["fechaDeContratacion"]);

            if (Personal.HotelID <= 0 || string.IsNullOrEmpty(Personal.Nombre) || string.IsNullOrEmpty(Personal.Apellido) ||
                string.IsNullOrEmpty(Personal.Posicion) || Personal.Salario <= 0 || Personal.FechaDeNacimiento == DateTime.MinValue ||
                string.IsNullOrEmpty(Personal.Telefono) || Personal.FechaDeContratacion == DateTime.MinValue)
            {
                errorMessage = "Todos los campos son requeridos y deben ser válidos";
                return;
            }

            // Guardar el nuevo personal en la base de datos
            try
            {
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Personal (HotelID, Nombre, Apellido, Posicion, Salario, FechaDeNacimiento, Telefono, FechaDeContratacion) " +
                                 "VALUES (@hotelID, @nombre, @apellido, @posicion, @salario, @fechaDeNacimiento, @telefono, @fechaDeContratacion)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@hotelID", Personal.HotelID);
                        command.Parameters.AddWithValue("@nombre", Personal.Nombre);
                        command.Parameters.AddWithValue("@apellido", Personal.Apellido);
                        command.Parameters.AddWithValue("@posicion", Personal.Posicion);
                        command.Parameters.AddWithValue("@salario", Personal.Salario);
                        command.Parameters.AddWithValue("@fechaDeNacimiento", Personal.FechaDeNacimiento);
                        command.Parameters.AddWithValue("@telefono", Personal.Telefono);
                        command.Parameters.AddWithValue("@fechaDeContratacion", Personal.FechaDeContratacion);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            // Limpiar los campos después de agregar el personal
            Personal.HotelID = 0;
            Personal.Nombre = "";
            Personal.Apellido = "";
            Personal.Posicion = "";
            Personal.Salario = 0;
            Personal.FechaDeNacimiento = DateTime.MinValue;
            Personal.Telefono = "";
            Personal.FechaDeContratacion = DateTime.MinValue;

            successMessage = "Nuevo personal agregado";

            Response.Redirect("/Client/Personal");
        }
    }
}