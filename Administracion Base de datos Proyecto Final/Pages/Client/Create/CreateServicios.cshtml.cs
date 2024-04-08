using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Administracion_Base_de_datos_Proyecto_Final.Pages.Client.Create
{
    public class CreateServiciosModel : PageModel
    {
        public Servicio Servicio = new Servicio();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            // Obtener los valores de los campos del formulario
            Servicio.Nombre = Request.Form["nombre"];
            Servicio.Descripcion = Request.Form["descripcion"];
            Servicio.Precio = Convert.ToDecimal(Request.Form["precio"]);

            if (string.IsNullOrEmpty(Servicio.Nombre) || string.IsNullOrEmpty(Servicio.Descripcion) || Servicio.Precio <= 0)
            {
                errorMessage = "Todos los campos son requeridos y deben ser válidos";
                return;
            }

            // Guardar el nuevo servicio en la base de datos
            try
            {
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Servicio (Nombre, Descripcion, Precio) " +
                                 "VALUES (@nombre, @descripcion, @precio)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nombre", Servicio.Nombre);
                        command.Parameters.AddWithValue("@descripcion", Servicio.Descripcion);
                        command.Parameters.AddWithValue("@precio", Servicio.Precio);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            // Limpiar los campos después de agregar el servicio
            Servicio.Nombre = "";
            Servicio.Descripcion = "";
            Servicio.Precio = 0;

            successMessage = "Nuevo servicio agregado";

            Response.Redirect("/Client/Index");
        }
    }
}