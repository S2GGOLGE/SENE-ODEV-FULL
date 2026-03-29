using Microsoft.Data.SqlClient;
using System.Data;

namespace SeneOdev
{
    public class Pass_Update
    {
        private static string NewPass { get; set; }
        private static string NewPassRepeat { get; set; }
        private readonly string connstring =
           "Data Source=EMREE\\SQLEXPRESS;Initial Catalog=Sene_Odevi;Integrated Security=True;Encrypt=False";
          private bool passUpdate()
          {
            using var bağlantı=new SqlConnection(connstring);
            if (NewPass != NewPassRepeat)
                return false;
            try
            {
                string update = ("UPDATE Kullanici SET [PasswordHash] WHERE ");
            }
            catch (Exception ex)
            {

            }
          }

    }
}