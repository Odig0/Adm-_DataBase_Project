using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Administracion_Base_de_datos_Proyecto_Final.Pages.Client
{
    public class ReservaServiciosModel : PageModel
    {
        public List<ReservaServicio> listReservaServicios = new List<ReservaServicio>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM ReservaServicio";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ReservaServicio reservaServicio = new ReservaServicio();
                                reservaServicio.IDReservaServicio = reader.GetInt32(0);
                                reservaServicio.IDServicio = reader.GetInt32(1);
                                reservaServicio.IDReserva = reader.GetInt32(2);
                                listReservaServicios.Add(reservaServicio);
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

    public class ReservaServicio
    {
        public int IDReservaServicio { get; set; }
        public int IDServicio { get; set; }
        public int IDReserva { get; set; }
    }
}