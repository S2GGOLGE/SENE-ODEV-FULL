using Microsoft.Data.SqlClient;
using System.Data;

namespace SeneOdev
{
    public class PassUpdate
    {
        public string Username { get; set; }
        public string NewPass { get; set; }
        public string NewPassRepeat { get; set; }

        private readonly string connstring =
            "Data Source=EMREE\\SQLEXPRESS;Initial Catalog=Sene_Odevi;Integrated Security=True;Encrypt=False";

        public (bool success, string message) Update()
        {
            try
            {
                if (NewPass != NewPassRepeat)
                    return (false, "Şifreler eşleşmiyor.");

                using var baglanti = new SqlConnection(connstring);
                baglanti.Open();

                string islem = "UPDATE Kullanici SET [PasswordHash] = @Password WHERE Username = @Username";

                using var cmd = new SqlCommand(islem, baglanti);
                cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 250).Value = NewPass;
                cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 100).Value = Username;

                bool updated = cmd.ExecuteNonQuery() > 0;
                return updated
                    ? (true, "Şifre başarıyla güncellendi.")
                    : (false, "Kullanıcı bulunamadı.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("HATA: " + ex.Message);
                return (false, "Sunucu hatası oluştu.");
            }
        }
    }
}