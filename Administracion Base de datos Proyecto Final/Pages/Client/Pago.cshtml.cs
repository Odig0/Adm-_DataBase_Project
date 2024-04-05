using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Administracion_Base_de_datos_Proyecto_Final.Pages.Client
{
    public class PagoModel : PageModel
    {
        public List<Pago> listPagos = new List<Pago>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Pago";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Pago pago = new Pago();
                                pago.IDPago = reader.GetInt32(0);
                                pago.IDReserva = reader.GetInt32(1);
                                pago.Monto = reader.GetDecimal(2);
                                pago.FechaDePago = reader.GetDateTime(3);
                                pago.MetodoPago = reader.GetString(4);
                                listPagos.Add(pago);
                            }
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }

    public class Pago
    {
        public int IDPago { get; set; }
        public int IDReserva { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaDePago { get; set; }
        public string MetodoPago { get; set; }
    }
}