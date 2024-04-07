using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Administracion_Base_de_datos_Proyecto_Final.Pages.Client.Create
{
    public class CreateReservaServiciosModel : PageModel
    {
        public ReservaServicio ReservaServicio = new ReservaServicio();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            // Obtener los valores de los campos del formulario
            ReservaServicio.IDServicio = Convert.ToInt32(Request.Form["idServicio"]);
            ReservaServicio.IDReserva = Convert.ToInt32(Request.Form["idReserva"]);

            if (ReservaServicio.IDServicio <= 0 || ReservaServicio.IDReserva <= 0)
            {
                errorMessage = "Todos los campos son requeridos y deben ser válidos";
                return;
            }

            // Guardar la nueva reserva de servicio en la base de datos
            try
            {
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO ReservaServicio (IDServicio, IDReserva) " +
                                 "VALUES (@idServicio, @idReserva)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@idServicio", ReservaServicio.IDServicio);
                        command.Parameters.AddWithValue("@idReserva", ReservaServicio.IDReserva);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            // Limpiar los campos después de agregar la reserva de servicio
            ReservaServicio.IDServicio = 0;
            ReservaServicio.IDReserva = 0;

            successMessage = "Nueva reserva de servicio agregada";

            Response.Redirect("/Client/Index");
        }
    }
}
