using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Administracion_Base_de_datos_Proyecto_Final.Pages.Client.Create
{
    public class CreateReservaModel : PageModel
     {
        public Reserva Reserva = new Reserva();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            // Obtener los valores de los campos del formulario
            Reserva.IDHuesped = Convert.ToInt32(Request.Form["idHuesped"]);
            Reserva.NumeroHabitacion = Convert.ToInt32(Request.Form["numeroHabitacion"]);
            Reserva.FechaDeEntrada = Convert.ToDateTime(Request.Form["fechaDeEntrada"]);
            Reserva.FechaDeSalida = Convert.ToDateTime(Request.Form["fechaDeSalida"]);
            Reserva.PrecioTotal = Convert.ToDecimal(Request.Form["precioTotal"]);

            DateTime year2000 = new DateTime(2000, 1, 1);

            if (Reserva.IDHuesped <= 0 || Reserva.NumeroHabitacion <= 0 ||
                Reserva.FechaDeEntrada <= year2000 || Reserva.FechaDeSalida <= Reserva.FechaDeEntrada ||
                Reserva.PrecioTotal <= 0)
            {
                errorMessage = "Todos los campos son requeridos y deben ser válidos";
                return ;
            }

            // Guardar la nueva reserva de habitación en la base de datos
            try
            {
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO ReservaHabitacion (IDHuesped, NumeroHabitacion, FechaDeEntrada, FechaDeSalida, PrecioTotal) " +
                                 "VALUES (@idHuesped, @numeroHabitacion, @fechaDeEntrada, @fechaDeSalida, @precioTotal)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@idHuesped", Reserva.IDHuesped);
                        command.Parameters.AddWithValue("@numeroHabitacion", Reserva.NumeroHabitacion);
                        command.Parameters.AddWithValue("@fechaDeEntrada", Reserva.FechaDeEntrada);
                        command.Parameters.AddWithValue("@fechaDeSalida", Reserva.FechaDeSalida);
                        command.Parameters.AddWithValue("@precioTotal", Reserva.PrecioTotal);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            // Limpiar los campos después de agregar la reserva de habitación
            Reserva.IDHuesped = 0;
            Reserva.NumeroHabitacion = 0;
            Reserva.FechaDeEntrada = DateTime.MinValue;
            Reserva.FechaDeSalida = DateTime.MinValue;
            Reserva.PrecioTotal = 0;

            successMessage = "Nueva reserva de habitación agregada";

            Response.Redirect("/Client/Reserva");
        }
    }
}
