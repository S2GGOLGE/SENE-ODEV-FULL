document.getElementById("passForm").addEventListener("submit", function (e) {
    e.preventDefault();

    const username = document.getElementById("username").value.trim();
    const newPass = document.getElementById("newPass").value.trim();
    const newPassRepeat = document.getElementById("newPassRepeat").value.trim();

    fetch("http://localhost:7074/updatepass", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
            Username: username,
            NewPass: newPass,
            NewPassRepeat: newPassRepeat
        })
    })
        .then(async res => {
            const data = await res.json();
            console.log("Response status:", res.status);
            console.log("Response data:", data);

            document.getElementById("result").innerText = data.message || "İşlem tamamlandı";

            if (res.ok && data.success) {
                console.log("Şifre güncellendi ✅");
                setTimeout(() => window.location.href = "giriş.html", 1000);
            } else {
                console.log("Şifre güncelleme başarısız ❌");
            }
        })
        .catch(err => {
            console.error("Fetch hatası:", err);
            document.getElementById("result").innerText = "Sunucuya bağlanılamadı.";
        });
});
