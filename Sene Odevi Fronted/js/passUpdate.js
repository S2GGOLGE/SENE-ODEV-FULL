function Update() {
    let newpass = document.getElementById("newpass").value;
    let newpassRepeat = document.getElementById("newpassRepeat").value;

    fetch("http://localhost:7074/PassUpdate", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
            newpass: newpass,
            newpassRepeat: newpassRepeat
        })
    })
        .then(res => res.json())
        .then(data => {
            alert(data.message);
            if (data.success) {
                window.location.href = "giriş.html";
            }
        })
        .catch(err => {
            console.error("Fetch hatası:", err);
        });
}