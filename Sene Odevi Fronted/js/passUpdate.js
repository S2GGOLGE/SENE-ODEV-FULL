function Update() {
    // Input id'lerinin HTML'de doğru olduğundan emin ol
    let username = document.getElementById("username")?.value; // Kullanıcıyı tanımak için şart
    let pass = document.getElementById("newpass").value;
    let passRepeat = document.getElementById("newpassRepeat").value;

    if (pass !== passRepeat) {
        alert("Şifreler uyuşmuyor!");
        return;
    }

    fetch("http://localhost:7074/PassUpdate", { // Port numaranın doğruluğunu kontrol et (7074 mü 5193 mü?)
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
            Username: username, // Backend'deki DTO ile birebir aynı isim
            Pass: pass,         // Backend'deki request.Pass ile eşleşir
            PassRepeat: passRepeat
        })
    })
    .then(res => res.json())
    .then(data => {
        if (data.success) {
            alert(data.message);
            window.location.href = "giriş.html";
        } else {
            alert("Hata: " + data.message);
        }
    })
    .catch(err => {
        console.error("Fetch hatası:", err);
        alert("Sunucuya bağlanılamadı!");
    });
}