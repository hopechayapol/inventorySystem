using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Query;
using System.Data;
using static InventorySystem.Pages.Rangsit.IndexRangsitModel;

namespace InventorySystem.Pages.Rangsit
{
    public class IndexRangsitModel : PageModel
    {
        public List<StockInfo> listStocks = new List<StockInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Server=tcp:inventory1212312.database.windows.net,1433;Initial Catalog=inventory;Persist Security Info=False;User ID=chayapol;Password=Hope1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM stocks WHERE storeid=1";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            int i = 0;

                            while (reader.Read())
                            {
                                StockInfo stockInfo = new StockInfo();
                                stockInfo.itemid = reader.GetInt32(0).ToString();
                                stockInfo.item = reader.GetString(1);
                                stockInfo.storeid = reader.GetString(2);
                                stockInfo.supplier = reader.GetString(3);
                                stockInfo.amount = reader.GetString(4);
                                stockInfo.create_at = reader.GetDateTime(5).ToString();

                                listStocks.Add(stockInfo);
                                i++;
                            }

                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Exception:  " + ex.ToString());
            }

        }

        public class StockInfo
        {
            public String itemid;
            public String item;
            public String storeid;
            public String supplier;
            public String amount;
            public String create_at;
        }
    }
}
