using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Administracion_Base_de_datos_Proyecto_Final.Pages.Client.Create
{
    public class CreateInventarioModel : PageModel
    {
        public Inventario Inventario = new Inventario();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            // Obtener los valores de los campos del formulario
            Inventario.NumeroHabitacion = Convert.ToInt32(Request.Form["númeroHabitación"]);
            Inventario.NombreItem = Request.Form["nombreItem"];
            Inventario.Cantidad = Convert.ToInt32(Request.Form["cantidad"]);

            if (Inventario.NumeroHabitacion <= 0 || string.IsNullOrEmpty(Inventario.NombreItem) || Inventario.Cantidad <= 0)
            {
                errorMessage = "Todos los campos son requeridos y deben ser válidos";
                return;
            }

            // Guardar el nuevo inventario en la base de datos
            try
            {
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Inventario (NúmeroHabitación, NombreItem, Cantidad) " +
                                 "VALUES (@númeroHabitación, @nombreItem, @cantidad)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@númeroHabitación", Inventario.NumeroHabitacion);
                        command.Parameters.AddWithValue("@nombreItem", Inventario.NombreItem);
                        command.Parameters.AddWithValue("@cantidad", Inventario.Cantidad);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            // Limpiar los campos después de agregar el inventario
            Inventario.NumeroHabitacion = 0;
            Inventario.NombreItem = "";
            Inventario.Cantidad = 0;

            successMessage = "Nuevo inventario agregado";

            Response.Redirect("/Client/Index");
        }
    }
}