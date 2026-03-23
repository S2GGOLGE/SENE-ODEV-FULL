using System;
using Microsoft.Data.SqlClient;
using System.Data;

namespace SeneOdev
{
    public class KayitOl
    {
        // Kullanıcı bilgileri
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }
        public string PasswordRepeat { get; set; }

        // SQL Server bağlantı dizesi
        private readonly string connstring =
            "Data Source=EMREE\\SQLEXPRESS;Initial Catalog=Sene_Odevi;Integrated Security=True;Encrypt=False";

        public bool Kayit()
        {
            try
            {
                // Bağlantı oluştur ve aç
                using var baglanti = new SqlConnection(connstring);
                baglanti.Open();

                // Şifre ve tekrar şifre eşleşmesini kontrol et
                if (Password != PasswordRepeat)
                    return false;

                // Kullanıcı adı veri tabanında var mı kontrol et
                string kontrol = "SELECT COUNT(*) FROM Kullanici WHERE Username=@Username";
                using var comnd = new SqlCommand(kontrol, baglanti);
                comnd.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = Username;
                int varmi = (int)comnd.ExecuteScalar();
                if (varmi > 0)
                    return false;

                // Kullanıcıyı veri tabanına ekle
                string query = @"INSERT INTO Kullanici
        (Ad,Soyad,Username,Email,Phone,Gender,[PasswordHash])
        VALUES (@Ad,@Soyad,@Username,@Email,@Phone,@Gender,@PasswordHash)";

                using var cmd = new SqlCommand(query, baglanti);

                // Parametreleri ayarla ve veri tiplerini belirt
                cmd.Parameters.Add("@Ad", SqlDbType.NVarChar, 50).Value = Name;
                cmd.Parameters.Add("@Soyad", SqlDbType.NVarChar, 50).Value = Surname;
                cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = Username;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = Email;
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = Phone;
                cmd.Parameters.Add("@Gender", SqlDbType.NVarChar, 10).Value = Gender;
                cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 256).Value = Password; // İleride şifreyi hash ile sakla

                // INSERT işlemi başarılı mı kontrol et
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
                return false;
            }
        }
    }
}