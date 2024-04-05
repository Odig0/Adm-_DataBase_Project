using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Administracion_Base_de_datos_Proyecto_Final.Pages.Client
{
    public class HuespedModel : PageModel
    {
        public List<Huesped> listHuespedes = new List<Huesped>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Huesped";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Huesped huesped = new Huesped();
                                huesped.IDHuesped = reader.GetInt32(0);
                                huesped.Nombre = reader.GetString(1);
                                huesped.Apellido = reader.GetString(2);
                                huesped.FechaDeNacimiento = reader.GetDateTime(3);
                                huesped.Direccion = reader.GetString(4);
                                huesped.Telefono = reader.GetString(5);
                                huesped.CorreoElectronico = reader.GetString(6);
                                listHuespedes.Add(huesped);
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

    public class Huesped
    {
        public int IDHuesped { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
    }
}