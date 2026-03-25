const form = document.getElementById("passForm");

form.addEventListener("submit", async function (e) {
    e.preventDefault(); // sayfa refresh engelle

    const data = {
        Username: document.getElementById("username").value.trim(),
        Phone: document.getElementById("phone").value.trim(),
        Email: document.getElementById("email").value.trim(),
        Pass: document.getElementById("pass").value.trim(),
        PassRepeat: document.getElementById("passRepeat").value.trim()
    };

    // ✅ VALIDATION
    if (!data.Pass || !data.PassRepeat) {
        alert("Şifre alanları boş olamaz!");
        return;
    }

    if (data.Pass !== data.PassRepeat) {
        alert("Şifreler uyuşmuyor!");
        return;
    }

    if (!data.Username && !data.Email && !data.Phone) {
        alert("En az bir kullanıcı bilgisi gir!");
        return;
    }

    try {
        const response = await fetch("https://localhost:7074/PassUpdate", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(data)
        });

        let result = {};
        try {
            result = await response.json();
        } catch {
            result.message = "Sunucudan beklenmeyen cevap geldi";
        }

        if (response.ok) {
            alert(result.message || "Şifre başarıyla güncellendi");

            // inputları temizle
            form.reset();

        } else {
            alert(result.message || "İşlem başarısız!");
        }

    } catch (error) {
        console.error("Fetch hatası:", error);
        alert("Sunucuya bağlanırken hata oluştu!");
    }
});