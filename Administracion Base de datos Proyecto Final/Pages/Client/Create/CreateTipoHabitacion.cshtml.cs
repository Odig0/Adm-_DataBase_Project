using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Administracion_Base_de_datos_Proyecto_Final.Pages.Client.Create
{
    public class CreateTipoHabitacionModel : PageModel
    {
        public TipoHabitacion TipoHabitación = new TipoHabitacion();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            // Obtener los valores de los campos del formulario
            TipoHabitación.Nombre = Request.Form["nombre"];
            TipoHabitación.Descripcion = Request.Form["descripcion"];
            TipoHabitación.PrecioPorNoche = Convert.ToDecimal(Request.Form["precioPorNoche"]);
            TipoHabitación.Capacidad = Convert.ToInt32(Request.Form["capacidad"]);

            if (string.IsNullOrEmpty(TipoHabitación.Nombre) || string.IsNullOrEmpty(TipoHabitación.Descripcion) || TipoHabitación.PrecioPorNoche <= 0 || TipoHabitación.Capacidad <= 0)
            {
                errorMessage = "Todos los campos son requeridos y deben ser válidos";
                return;
            }

            // Guardar el nuevo tipo de habitación en la base de datos
            try
            {
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO TipoHabitacion (Nombre, Descripcion, PrecioPorNoche, Capacidad) " +
                                 "VALUES (@nombre, @descripcion, @precioPorNoche, @capacidad)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nombre", TipoHabitación.Nombre);
                        command.Parameters.AddWithValue("@descripcion", TipoHabitación.Descripcion);
                        command.Parameters.AddWithValue("@precioPorNoche", TipoHabitación.PrecioPorNoche);
                        command.Parameters.AddWithValue("@capacidad", TipoHabitación.Capacidad);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            // Limpiar los campos después de agregar el tipo de habitación
            TipoHabitación.Nombre = "";
            TipoHabitación.Descripcion = "";
            TipoHabitación.PrecioPorNoche = 0;
            TipoHabitación.Capacidad = 0;

            successMessage = "Nuevo tipo de habitación agregado";

            Response.Redirect("/Client/TipoHabitacion");
        }
    }
}