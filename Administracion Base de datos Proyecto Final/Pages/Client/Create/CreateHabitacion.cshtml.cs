using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;
namespace Administracion_Base_de_datos_Proyecto_Final.Pages.Client.Create
{
    public class CreateHabitacionModel : PageModel
    {
        public Habitacion Habitacion = new Habitacion();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            // Obtener los valores de los campos del formulario
            Habitacion.HotelID = Convert.ToInt32(Request.Form["hotelId"]);
            Habitacion.TipoID = Convert.ToInt32(Request.Form["tipoId"]);
            Habitacion.Estado = Request.Form["estado"];

            if (Habitacion.HotelID <= 0 || Habitacion.TipoID <= 0 || string.IsNullOrEmpty(Habitacion.Estado))
            {
                errorMessage = "Todos los campos son requeridos";
                return;
            }

            // Guardar la nueva habitación en la base de datos
            try
            {
                String connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Habitacion (HotelID, TipoID, Estado) " +
                                 "VALUES (@hotelId, @tipoId, @estado)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@hotelId", Habitacion.HotelID);
                        command.Parameters.AddWithValue("@tipoId", Habitacion.TipoID);
                        command.Parameters.AddWithValue("@estado", Habitacion.Estado);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            // Limpiar los campos después de agregar la habitación
            Habitacion.HotelID = 0;
            Habitacion.TipoID = 0;
            Habitacion.Estado = "";

            successMessage = "Nueva Habitación agregada";

            Response.Redirect("/Client/Index");
        }
    }
}