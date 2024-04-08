using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Administracion_Base_de_datos_Proyecto_Final.Pages.Client.Edit
{
    public class EditInventarioModel : PageModel
    {
        public Inventario inventario = new Inventario();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            try
            {
                string idInventario = Request.Query["IDInventario"];
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT IDInventario, NumeroHabitacion, NombreItem, Cantidad FROM Inventario WHERE IDInventario = @IDInventario";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@IDInventario", idInventario);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                inventario.IDInventario = reader.GetInt32(0);
                                inventario.NumeroHabitacion = reader.GetInt32(1);
                                inventario.NombreItem = reader.GetString(2);
                                inventario.Cantidad = reader.GetInt32(3);
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
            inventario.IDInventario = Convert.ToInt32(Request.Form["IDInventario"]);
            inventario.NumeroHabitacion = Convert.ToInt32(Request.Form["NumeroHabitacion"]);
            inventario.NombreItem = Request.Form["NombreItem"];
            inventario.Cantidad = Convert.ToInt32(Request.Form["Cantidad"]);

            try
            {
                string connectionString = "Data Source=DESKTOP-2J8LJOL;Initial Catalog=Hotel;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE Inventario " +
                                 "SET NumeroHabitacion = @NumeroHabitacion, NombreItem = @NombreItem, Cantidad = @Cantidad " +
                                 "WHERE IDInventario = @IDInventario";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@IDInventario", inventario.IDInventario);
                        command.Parameters.AddWithValue("@NumeroHabitacion", inventario.NumeroHabitacion);
                        command.Parameters.AddWithValue("@NombreItem", inventario.NombreItem);
                        command.Parameters.AddWithValue("@Cantidad", inventario.Cantidad);

                        command.ExecuteNonQuery();
                    }
                }

                successMessage = "Inventario actualizado correctamente";
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            Response.Redirect("/Client/Inventario");
        }
    }
}
