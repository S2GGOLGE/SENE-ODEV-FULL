namespace SeneOdev;

using Microsoft.Data.SqlClient;
using System.Data;

public class Login
{
    public static string GirisYap(string username, string password)
    {
        // Kullanıcının boş alan bırakıp bırakmadığını kontrol et
        string sonuc = BosAlanKontrolu(username, password);
        if (sonuc != "OK")
            return sonuc;

        // SQL Server veri tabanına bağlantı dizesi
        string connstring = "Data Source=EMREE\\SQLEXPRESS;Initial Catalog=Sene_Odevi;Integrated Security=True;Encrypt=False";

        using var baglanti = new SqlConnection(connstring);
        baglanti.Open();

        // Kullanıcının şifresini veri tabanından al
        string komut = "SELECT PasswordHash FROM Kullanici WHERE Username = @username";

        using var islem = new SqlCommand(komut, baglanti);
        islem.Parameters.AddWithValue("@username", username);

        var result = islem.ExecuteScalar();

        // Kullanıcı bulunamadıysa mesaj dön
        if (result == null)
            return "Kullanıcı yok";

        string dbPassword = result.ToString();

        // Girilen şifre ile veri tabanındaki şifreyi karşılaştır eyer şifre uyuşmuyor ise mesaj dön 
        // Not: Eğer PasswordHash kullanılıyorsa burada hash kontrolü yapılmalı
        if (dbPassword != password)
            return "Şifre yanlış";

        return "OK";
    }

    // Boş alan girilmesini engellemek için kontrol
    public static string BosAlanKontrolu(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username))
            return "Lütfen kullanıcı adınızı girin.";

        if (string.IsNullOrWhiteSpace(password))
            return "Lütfen şifrenizi giriniz.";

        return "OK";
    }
}