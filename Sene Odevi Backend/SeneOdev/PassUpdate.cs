using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace SeneOdev
{
    public class PassUpdate
    {
        public string Username { get; set; }
        public string NewPass { get; set; }
        public string NewPassRepeat { get; set; }

        private readonly string connstring =
            "Data Source=EMREE\\SQLEXPRESS;Initial Catalog=Sene_Odevi;Integrated Security=True;Encrypt=False";

        public bool Update()
        {
            try
            {
                // 🔐 Şifreler eşleşiyor mu
                if (NewPass != NewPassRepeat)
                    return false;

                using var baglanti = new SqlConnection(connstring);
                baglanti.Open();

                string islem = "UPDATE Kullanici SET [PasswordHash] = @PasswordHash WHERE Username = @Username";

                using var cmd = new SqlCommand(islem, baglanti);

                cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 250).Value = HashPassword(NewPass);
                cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 100).Value = Username;

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("HATA: " + ex.Message);
                return false;
            }
        }

        // 🔐 Basit hash (şimdilik yeterli)
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}