using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using static InventorySystem.Pages.Siam.IndexSiamModel;

namespace inventorySystem.Pages.Siam
{
    public class EditSiamModel : PageModel
    {
        public StockInfo stockInfo = new StockInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String itemid = Request.Query["itemid"];

            try
            {
                string connectionString = "Server=tcp:inventory1212312.database.windows.net,1433;Initial Catalog=inventory;Persist Security Info=False;User ID=chayapol;Password=Hope1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM stocks WHERE itemid=@itemid";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@itemid", itemid);
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            if (reader.Read())
                            {
                                stockInfo.itemid = reader.GetInt32(0).ToString();
                                stockInfo.item = reader.GetString(1);
                                stockInfo.storeid = reader.GetString(2);
                                stockInfo.supplier = reader.GetString(3);
                                stockInfo.amount = reader.GetString(4);
                                stockInfo.create_at = reader.GetDateTime(5).ToString();
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
            stockInfo.itemid = Request.Form["itemid"];
            stockInfo.item = Request.Form["item"];
            stockInfo.storeid = Request.Form["storeid"];
            stockInfo.supplier = Request.Form["supplier"];
            stockInfo.amount = Request.Form["amount"];

            if (stockInfo.item.Length == 0 ||stockInfo.storeid.Length == 0 || 
                stockInfo.supplier.Length == 0 ||stockInfo.amount.Length == 0)

            {
                errorMessage = "All fields are required";
                return;
            }

            try
            {
                String connectionString = "Server=tcp:inventory1212312.database.windows.net,1433;Initial Catalog=inventory;Persist Security Info=False;User ID=chayapol;Password=Hope1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE stocks " +
                                 "SET item=@item, storeid=@storeid ,supplier=supplier, amount=@amount" +
                                 " WHERE itemid=@itemid;";

                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        command.Parameters.AddWithValue("@itemid", stockInfo.itemid);
                        command.Parameters.AddWithValue("@item", stockInfo.item);
                        command.Parameters.AddWithValue("@storeid", stockInfo.storeid);
                        command.Parameters.AddWithValue("@supplier", stockInfo.supplier);
                        command.Parameters.AddWithValue("@amount", stockInfo.amount);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)

            {
                errorMessage = ex.Message;
                return;

            }
            Response.Redirect("/Siam/indexSiam");

        }   



    }



}
