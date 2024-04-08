using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Administracion_Base_de_datos_Proyecto_Final.Pages.Client.Create
{
    public class CreateTipoHabitacionModel : PageModel
    {
        public TipoHabitacion TipoHabitaci�n = new TipoHabitacion();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            // Obtener los valores de los campos del formulario
            TipoHabitaci�n.Nombre = Request.Form["nombre"];
            TipoHabitaci�n.Descripcion = Request.Form["descripcion"];
            TipoHabitaci�n.PrecioPorNoche = Convert.ToDecimal(Request.Form["precioPorNoche"]);
            TipoHabitaci�n.Capacidad = Convert.ToInt32(Request.Form["capacidad"]);

            if (string.IsNullOrEmpty(TipoHabitaci�n.Nombre) || string.IsNullOrEmpty(TipoHabitaci�n.Descripcion) || TipoHabitaci�n.PrecioPorNoche <= 0 || TipoHabitaci�n.Capacidad <= 0)
            {
                errorMessage = "Todos los campos son requeridos y deben ser v�lidos";
                return;
            }

            // Guardar el nuevo tipo de habitaci�n en la base de datos
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
                        command.Parameters.AddWithValue("@nombre", TipoHabitaci�n.Nombre);
                        command.Parameters.AddWithValue("@descripcion", TipoHabitaci�n.Descripcion);
                        command.Parameters.AddWithValue("@precioPorNoche", TipoHabitaci�n.PrecioPorNoche);
                        command.Parameters.AddWithValue("@capacidad", TipoHabitaci�n.Capacidad);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            // Limpiar los campos despu�s de agregar el tipo de habitaci�n
            TipoHabitaci�n.Nombre = "";
            TipoHabitaci�n.Descripcion = "";
            TipoHabitaci�n.PrecioPorNoche = 0;
            TipoHabitaci�n.Capacidad = 0;

            successMessage = "Nuevo tipo de habitaci�n agregado";

            Response.Redirect("/Client/TipoHabitacion");
        }
    }
}