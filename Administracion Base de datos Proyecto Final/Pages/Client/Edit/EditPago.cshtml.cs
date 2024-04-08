using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Administracion_Base_de_datos_Proyecto_Final.Pages.Client.Edit
{
    public class EditPagoModel : PageModel
    {
        public Pago pago = new Pago();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            try
            {
                string idPago = Request.Query["IDPago"];
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT IDPago, IDReserva, Monto, FechaDePago, MetodoPago FROM Pago WHERE IDPago = @IDPago";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@IDPago", idPago);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                pago.IDPago = reader.GetInt32(0);
                                pago.IDReserva = reader.GetInt32(1);
                                pago.Monto = reader.GetDecimal(2);
                                pago.FechaDePago = reader.GetDateTime(3);
                                pago.MetodoPago = reader.GetString(4);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            pago.IDPago = Convert.ToInt32(Request.Form["IDPago"]);
            pago.IDReserva = Convert.ToInt32(Request.Form["IDReserva"]);
            pago.Monto = Convert.ToDecimal(Request.Form["Monto"]);
            pago.FechaDePago = DateTime.Parse(Request.Form["FechaDePago"]);
            pago.MetodoPago = Request.Form["MetodoPago"];

            try
            {
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE Pago " +
                                 "SET IDReserva = @IDReserva, Monto = @Monto, FechaDePago = @FechaDePago, MetodoPago = @MetodoPago " +
                                 "WHERE IDPago = @IDPago";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@IDPago", pago.IDPago);
                        command.Parameters.AddWithValue("@IDReserva", pago.IDReserva);
                        command.Parameters.AddWithValue("@Monto", pago.Monto);
                        command.Parameters.AddWithValue("@FechaDePago", pago.FechaDePago);
                        command.Parameters.AddWithValue("@MetodoPago", pago.MetodoPago);

                        command.ExecuteNonQuery();
                    }
                }

                successMessage = "Pago actualizado correctamente";
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            Response.Redirect("/Client/Pago");
        }
    }
}
