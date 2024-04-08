using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Administracion_Base_de_datos_Proyecto_Final.Pages.Client.Create
{
    public class CreatePagoModel : PageModel
    {
        public Pago Pago = new Pago();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            // Obtener los valores de los campos del formulario
            Pago.IDReserva = Convert.ToInt32(Request.Form["IDReserva"]);
            Pago.Monto = Convert.ToDecimal(Request.Form["monto"]);
            Pago.FechaDePago = Convert.ToDateTime(Request.Form["fechaDePago"]);
            Pago.MetodoPago = Request.Form["metodoPago"];

            if (Pago.IDReserva <= 0 || Pago.Monto <= 0 || Pago.FechaDePago == DateTime.MinValue || string.IsNullOrEmpty(Pago.MetodoPago))
            {
                errorMessage = "Todos los campos son requeridos y deben ser válidos";
                return;
            }

            // Guardar el nuevo pago en la base de datos
            try
            {
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Pago (IDReserva, Monto, FechaDePago, MetodoPago) " +
                                 "VALUES (@IDReserva, @monto, @fechaDePago, @metodoPago)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@IDReserva", Pago.IDReserva);
                        command.Parameters.AddWithValue("@monto", Pago.Monto);
                        command.Parameters.AddWithValue("@fechaDePago", Pago.FechaDePago);
                        command.Parameters.AddWithValue("@metodoPago", Pago.MetodoPago);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            // Limpiar los campos después de agregar el pago
            Pago.IDReserva = 0;
            Pago.Monto = 0;
            Pago.FechaDePago = DateTime.MinValue;
            Pago.MetodoPago = "";

            successMessage = "Nuevo pago agregado";

            Response.Redirect("/Client/Pago");
        }
    }
}