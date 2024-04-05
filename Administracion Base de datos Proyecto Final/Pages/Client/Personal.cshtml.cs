using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Administracion_Base_de_datos_Proyecto_Final.Pages.Client
{
    public class PersonalModel : PageModel
    {
        public List<Personal> listPersonal = new List<Personal>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Personal";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Personal personal = new Personal();
                                personal.IDPersonal = reader.GetInt32(0);
                                personal.HotelID = reader.GetInt32(1);
                                personal.Nombre = reader.GetString(2);
                                personal.Apellido = reader.GetString(3);
                                personal.Posicion = reader.GetString(4);
                                personal.Salario = reader.GetDecimal(5);
                                personal.FechaDeNacimiento = reader.GetDateTime(6);
                                personal.Telefono = reader.GetString(7);
                                personal.FechaDeContratacion = reader.GetDateTime(8);
                                listPersonal.Add(personal);
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

    public class Personal
    {
        public int IDPersonal { get; set; }
        public int HotelID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Posicion { get; set; }
        public decimal Salario { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaDeContratacion { get; set; }
    }
}